using BankAccountOOPMultiuser.Business.Contracts.Dtos;
using BankAccountOOPMultiuser.Domain.Models.Account;

namespace BankAccountOOPMultiuser.Business.Contracts
{
    public interface IAccountService
    {
        decimal? GetMoney();
        IncomeResultDto AddMoney(decimal income);
        OutcomeResultDto RetireMoney(decimal outcome);
        MovementListDto GetMovements();
        MovementListDto GetIncomes();
        MovementListDto GetOutcomes();
    }
}
