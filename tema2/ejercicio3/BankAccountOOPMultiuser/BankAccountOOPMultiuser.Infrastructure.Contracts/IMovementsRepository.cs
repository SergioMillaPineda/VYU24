using BankAccountOOPMultiuser.Infrastructure.Contracts.Entities;

namespace BankAccountOOPMultiuser.Infrastructure.Contracts
{
    public interface IMovementsRepository
    {
        List<MovementEntity> GetMovements();
        void AddMovement(MovementEntity newMovement);
    }
}
