using BankAccountOOPMultiuser.Business.Contracts;
using BankAccountOOPMultiuser.Business.Contracts.Dtos;
using BankAccountOOPMultiuser.Infrastructure.Contracts;

namespace BankAccountOOPMultiuser.Business.Impl
{
    public class CredentialsService : ICredentialsService
    {
        private readonly IAccountRepository _acountRepository;

        public CredentialsService(IAccountRepository acountRepository)
        {
            _acountRepository = acountRepository;
        }

        public bool ValidateCredentials(CredentialsDto dto)
        {
            return _acountRepository.FindByNumberAndPin(dto.AccountName, dto.AccountPin) != null;
        }
    }
}
