using BankAccountOOPMultiuser.XCutting.Enums;

namespace BankAccountOOPMultiuser.Business.Contracts.Dtos
{
    public class OutcomeResultDto
    {
        public bool ResultHasErrors;
        public OutcomeErrorEnum? Error;
        public decimal maxOutcomeAllowed;
        public decimal maxDebtAllowed;
        public decimal totalMoney;
    }
}
