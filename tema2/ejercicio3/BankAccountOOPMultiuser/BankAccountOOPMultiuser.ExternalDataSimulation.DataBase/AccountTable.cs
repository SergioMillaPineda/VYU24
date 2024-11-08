namespace BankAccountOOPMultiuser.ExternalDataSimulation.DataBase
{
    public static class AccountTable
    {
        public static List<AccountTableRow> Rows = new() {
            new AccountTableRow
            {
                Id = 1,
                Number = 1000,
                Pin = 1111,
                Money = 0
            },
            new AccountTableRow
            {
                Id = 2,
                Number = 2000,
                Pin = 2222,
                Money = 0
            },
            new AccountTableRow
            {
                Id = 3,
                Number = 3000,
                Pin = 3333,
                Money = 0
            }
        };
    }
}
