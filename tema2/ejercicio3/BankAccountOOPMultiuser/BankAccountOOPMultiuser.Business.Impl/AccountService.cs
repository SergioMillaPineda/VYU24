using BankAccountOOPMultiuser.Business.Contracts;
using BankAccountOOPMultiuser.Business.Contracts.Dtos;
using BankAccountOOPMultiuser.Domain.Models.Account;
using BankAccountOOPMultiuser.Infrastructure.Contracts;
using BankAccountOOPMultiuser.Infrastructure.Contracts.Entities;
using BankAccountOOPMultiuser.XCutting.Enums;

namespace BankAccountOOPMultiuser.Business.Impl
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository? _accountRepository;
        private readonly IMovementsRepository? _movementsRepository;

        public AccountService(IAccountRepository accountRepository, IMovementsRepository? movementsRepository)
        {
            _accountRepository = accountRepository;
            _movementsRepository = movementsRepository;
        }

        public IncomeResultDto AddMoney(decimal income)
        {
            IncomeResultDto result = new()
            {
                ResultHasErrors = false,
                Error = null
            };

            AccountModel accountModel = new();
            if (accountModel.ValidIncome(income))
            {
                AccountEntity? accountEntity = _accountRepository?.GetAccountInfo();
                List<MovementEntity>? movementsEntityList = _movementsRepository?.GetMovements();
                if (accountEntity != null && movementsEntityList != null)
                {
                    // map entity to domain model...
                    accountModel.Money = accountEntity.money;
                    accountModel.Movements = movementsEntityList.Select(x => new MovementModel
                    {
                        Value = x.value,
                        Timestamp = x.timestamp,
                    }).ToList();

                    // ... in order to apply business logic in domain model
                    accountModel.AddIncome(income);

                    // map domain model result to entity...
                    accountEntity.money = accountModel.Money;
                    MovementEntity movementToAdd = new()
                    {
                        value = accountModel.Movements.Last().Value,
                        timestamp = accountModel.Movements.Last().Timestamp
                    };

                    // ... in order to save entity with the last changes done
                    _accountRepository?.UpdateAccount(accountEntity);
                    _movementsRepository?.AddMovement(movementToAdd);

                    // map domain model to Dto in order to add needed information to presentation layer
                    result.totalMoney = accountModel.Money;
                }
            }
            else
            {
                result.ResultHasErrors = true;

                if (accountModel.incomeNegative)
                {
                    result.Error = IncomeErrorEnum.Negative;
                }

                if (accountModel.incomeOverMaxValue)
                {
                    result.Error = IncomeErrorEnum.OverMaxValue;
                    result.maxIncomeAllowed = AccountModel.maxIncome;
                }
            }

            return result;
        }

        public OutcomeResultDto RetireMoney(decimal outcome)
        {
            OutcomeResultDto result = new()
            {
                ResultHasErrors = false,
                Error = null
            };

            AccountModel accountModel = new();
            if (accountModel.ValidOutcome(outcome))
            {
                AccountEntity? accountEntity = _accountRepository?.GetAccountInfo();
                List<MovementEntity>? movementsEntityList = _movementsRepository?.GetMovements();
                if (accountEntity != null && movementsEntityList != null)
                {
                    accountModel.Money = accountEntity.money;
                    accountModel.Movements = movementsEntityList.Select(x => new MovementModel
                    {
                        Value = x.value,
                        Timestamp = x.timestamp,
                    }).ToList();

                    accountModel.AddOutcome(outcome);

                    accountEntity.money = accountModel.Money;
                    MovementEntity movementToAdd = new()
                    {
                        value = accountModel.Movements.Last().Value,
                        timestamp = accountModel.Movements.Last().Timestamp
                    };

                    _accountRepository?.UpdateAccount(accountEntity);
                    _movementsRepository?.AddMovement(movementToAdd);

                    result.totalMoney = accountModel.Money;
                }
            }
            else
            {
                result.ResultHasErrors = true;

                if (accountModel.outcomeNegative)
                {
                    result.Error = OutcomeErrorEnum.Negative;
                }

                if (accountModel.outcomeOverMaxValue)
                {
                    result.Error = OutcomeErrorEnum.OverMaxValue;
                    result.maxOutcomeAllowed = AccountModel.maxOutcome;
                }

                if (accountModel.outcomeLeavesAccountOverMaxAllowedDebt)
                {
                    result.Error = OutcomeErrorEnum.MaxAllowedDebtSurpassed;
                    result.maxDebtAllowed = AccountModel.maxDebtAllowed;
                }
            }

            return result;
        }

        public MovementListDto GetMovements()
        {
            List<MovementEntity> movementsEntityList = _movementsRepository?.GetMovements()!;

            return new()
            {
                movements = movementsEntityList.Select(x => new MovementDto
                {
                    value = x.value,
                    timestamp = x.timestamp,
                }).ToList(),
                totalMoney = movementsEntityList.Sum(x => x.value)
            };
        }

        public MovementListDto GetIncomes()
        {
            List<MovementEntity> incomesEntityList = _movementsRepository!.GetMovements().Where(x => x.value > 0).ToList();

            return new()
            {
                movements = incomesEntityList.Select(x => new MovementDto
                {
                    value = x.value,
                    timestamp = x.timestamp,
                }).ToList(),
                totalMoney = incomesEntityList.Sum(x => x.value)
            };
        }

        public MovementListDto GetOutcomes()
        {
            List<MovementEntity> outcomesEntityList = _movementsRepository!.GetMovements().Where(x => x.value < 0).ToList();

            return new()
            {
                movements = outcomesEntityList.Select(x => new MovementDto
                {
                    value = x.value,
                    timestamp = x.timestamp,
                }).ToList(),
                totalMoney = outcomesEntityList.Sum(x => x.value)
            };
        }

        public decimal? GetMoney()
        {
            AccountEntity? entity = _accountRepository?.GetAccountInfo();
            if (entity == null)
            {
                throw new Exception();
            }
            AccountModel accountModel = new()
            {
                Number = entity.number,
                Money = entity.money
            };

            return accountModel?.Money;
        }
    }
}
