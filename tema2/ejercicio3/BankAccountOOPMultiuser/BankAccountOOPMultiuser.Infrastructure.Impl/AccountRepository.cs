using BankAccountOOPMultiuser.ExternalDataSimulation.DataBase;
using BankAccountOOPMultiuser.Infrastructure.Contracts;
using BankAccountOOPMultiuser.Infrastructure.Contracts.Entities;

namespace BankAccountOOPMultiuser.Infrastructure.Impl
{
    public class AccountRepository : IAccountRepository
    {
        private List<AccountEntity>? _accounts;

        private List<AccountEntity> GetLastDatabaseValues()
        {
            _accounts = new List<AccountEntity>();
            foreach (AccountTableRow row in AccountTable.Rows)
            {
                _accounts.Add(new AccountEntity
                {
                    id = row.Id,
                    money = row.Money,
                    number = row.Number.ToString(),
                    pin = row.Pin.ToString()
                });
            }

            return _accounts;
        }

        public AccountEntity? FindByNumberAndPin(int number, int pin)
        {
            return GetLastDatabaseValues().FirstOrDefault(x => x.number == number.ToString() && x.pin == pin.ToString());
        }

        public AccountEntity? GetAccountInfo()
        {
            return GetLastDatabaseValues().First();
        }

        public void UpdateAccount(AccountEntity updatedEntity)
        {
            AccountEntity currentEntity = GetLastDatabaseValues().First(x => x.number == updatedEntity.number);

            currentEntity.number = updatedEntity.number;
            currentEntity.money = updatedEntity.money;
        }
    }
}
