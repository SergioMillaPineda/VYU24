using BankAccountOOPMultiuser.Business.Contracts.Dtos;

namespace BankAccountOOPMultiuser.Business.Contracts
{
    public interface ICredentialsService
    {
        public bool ValidateCredentials(CredentialsDto dto);
    }
}
