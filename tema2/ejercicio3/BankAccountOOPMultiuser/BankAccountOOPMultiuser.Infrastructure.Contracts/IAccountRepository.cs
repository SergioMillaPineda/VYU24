using BankAccountOOPMultiuser.Infrastructure.Contracts.Entities;

namespace BankAccountOOPMultiuser.Infrastructure.Contracts
{
    public interface IAccountRepository
    {
        public AccountEntity? FindByNumberAndPin(int number, int pin);
        AccountEntity? GetAccountInfo();
        void UpdateAccount(AccountEntity updatedEntity);
    }
}
