namespace BankAccountOOPMultiuser.Domain.Models.Account
{
    public class AccountModel
    {
        public const decimal maxIncome = 5000;
        public const decimal maxOutcome = 1000;
        public const decimal maxDebtAllowed = -200;

        public string? Number { get; set; }
        public decimal Money { get; set; }
        public List<MovementModel>? Movements { get; set; }

        public bool incomeNegative;
        public bool incomeOverMaxValue;

        public bool outcomeNegative;
        public bool outcomeOverMaxValue;
        public bool outcomeLeavesAccountOverMaxAllowedDebt;

        public bool ValidIncome(decimal income)
        {
            incomeNegative = income < 0;
            incomeOverMaxValue = income > maxIncome;

            return !incomeNegative && !incomeOverMaxValue;
        }

        public bool ValidOutcome(decimal outcome)
        {
            outcomeNegative = outcome < 0;
            outcomeOverMaxValue = outcome > maxOutcome;
            outcomeLeavesAccountOverMaxAllowedDebt = Money - outcome < maxDebtAllowed;

            return !outcomeNegative && !outcomeOverMaxValue && !outcomeLeavesAccountOverMaxAllowedDebt;
        }

        public void AddIncome(decimal income)
        {
            Money += income;
            Movements?.Add(new MovementModel
            {
                Value = income,
                Timestamp = DateTime.Now,
            });
        }

        public void AddOutcome(decimal outcome)
        {
            Money -= outcome;
            Movements?.Add(new MovementModel
            {
                Value = -outcome,
                Timestamp = DateTime.Now,
            });
        }
    }
}
