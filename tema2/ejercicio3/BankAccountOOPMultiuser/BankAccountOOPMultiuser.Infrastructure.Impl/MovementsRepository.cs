using BankAccountOOPMultiuser.Infrastructure.Contracts.Entities;
using BankAccountOOPMultiuser.Infrastructure.Contracts;

namespace BankAccountOOPMultiuser.Infrastructure.Impl
{
    public class MovementsRepository : IMovementsRepository
    {
        private static List<MovementEntity> simulatedMovementsDBTable = new();

        public List<MovementEntity> GetMovements()
        {
            return simulatedMovementsDBTable;
        }

        public void AddMovement(MovementEntity newMovement)
        {
            simulatedMovementsDBTable.Add(newMovement);
        }
    }
}
