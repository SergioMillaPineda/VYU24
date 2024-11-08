using BankAccountOOPMultiuser.XCutting.Enums;

namespace BankAccountOOPMultiuser.Business.Contracts.Dtos
{
    public class IncomeResultDto
    {
        public bool ResultHasErrors;
        public IncomeErrorEnum? Error;
        public decimal maxIncomeAllowed;
        public decimal totalMoney;
    }
}
