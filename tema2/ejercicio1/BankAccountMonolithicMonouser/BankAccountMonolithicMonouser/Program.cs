namespace BankAccountMonolithicMonouser
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // bank business logic limits
            const decimal maxIncome = 5000;
            const decimal maxOutcome = 1000;
            const decimal maxDebtAllowed = -200;

            // bank business available currencies
            List<string> AvailableCurrencies = new()
            {
                "EUR",
                "USD"
            };

            // customized currency value
            string currency = AvailableCurrencies[0];

            // data model containing the information that will be managed by this program
            decimal userMoney = 0;
            List<decimal> movementAmountList = new();
            List<DateTime> movementInstantList = new();

            // data used during console interface interaction
            // - console custom configuration
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            // - customized main menu options
            const string incomeOption = "1";
            const string outcomeOption = "2";
            const string listAllOption = "3";
            const string listIncomesOption = "4";
            const string listOutcomesOption = "5";
            const string showMoneyOption = "6";
            const string exitOption = "7";

            // customized input user has to enter in order to leave program
            const char quitSelection = 'x';

            // - data model to manage user navigation
            string? userOption;
            bool exitProgram = false;

            // - business->presentation mapping: data model currency -> currency symbol to show on console
            Dictionary<string, string> currencySymbolDictionary = new()
            {
                { "EUR", "€" },
                { "USD", "$" }
            };
            string currencySymbol = currencySymbolDictionary[currency];

            // abstraction of line jump string
            const string newLine = "\r\n";

            // main menu full implementation code
            do
            {
                Console.Clear();
                Console.Write(
                    $"======================{newLine}" +
                    $"{incomeOption}. Money income{newLine}" +
                    $"{outcomeOption}. Money outcome{newLine}" +
                    $"{listAllOption}. List all movements{newLine}" +
                    $"{listIncomesOption}. List incomes{newLine}" +
                    $"{listOutcomesOption}. List outcomes{newLine}" +
                    $"{showMoneyOption}. Show current money{newLine}" +
                    $"{exitOption}. Exit{newLine}" +
                    $"======================{newLine}"
                );
                Console.Write("Select an option: ");

                userOption = Console.ReadLine()?.Trim();

                string outputMsg = "";
                switch (userOption)
                {
                    case incomeOption:
                    case outcomeOption:
                    case listAllOption:
                    case listIncomesOption:
                    case listOutcomesOption:
                    case showMoneyOption:
                        switch (userOption)
                        {
                            case incomeOption:
                                Console.Write("Enter desired income: ");
                                string? incomeAsString = Console.ReadLine()?.Trim().Replace(",", "");

                                decimal parsedIncome;
                                bool isIncomeDecimal = decimal.TryParse(incomeAsString, out parsedIncome);
                                if (!isIncomeDecimal)
                                {
                                    outputMsg = "Entered value is not a valid decimal";
                                }
                                else if (parsedIncome <= 0)
                                {
                                    outputMsg = "Income has to be a positive amount";
                                }
                                else if (parsedIncome > maxIncome)
                                {
                                    outputMsg = $"Income cannot exceed {maxIncome:0.00}{currencySymbol}";
                                }

                                if (outputMsg == "")
                                {
                                    userMoney += parsedIncome;
                                    movementAmountList.Add(parsedIncome);
                                    movementInstantList.Add(DateTime.Now);
                                    outputMsg = $"Added {parsedIncome}{currencySymbol}. Your current money amounts to {userMoney:0.00}{currencySymbol}";
                                }

                                Console.WriteLine();
                                Console.WriteLine(outputMsg);
                                break;
                            case outcomeOption:
                                Console.Write("Enter desired outcome: ");
                                string? outcomeAsString = Console.ReadLine()?.Trim().Replace(",", "");

                                decimal parsedOutcome;
                                bool isOutcomeDecimal = decimal.TryParse(outcomeAsString, out parsedOutcome);
                                if (!isOutcomeDecimal)
                                {
                                    outputMsg = "Entered value is not a valid decimal";
                                }
                                else if (parsedOutcome <= 0)
                                {
                                    outputMsg = "Outcome has to be a positive amount";
                                }
                                else if (parsedOutcome > maxOutcome)
                                {
                                    outputMsg = $"Outcome cannot exceed {maxOutcome:0.00}{currencySymbol}";
                                }
                                else if (userMoney - parsedOutcome < maxDebtAllowed)
                                {
                                    outputMsg = $"debt cannot surpass {Math.Abs(maxDebtAllowed):0.00}{currencySymbol}";
                                }

                                if (outputMsg == "")
                                {
                                    userMoney -= parsedOutcome;
                                    movementAmountList.Add(-parsedOutcome);
                                    movementInstantList.Add(DateTime.Now);
                                    outputMsg = $"Retired {parsedOutcome}{currencySymbol}. Your current money amounts to {userMoney:0.00}{currencySymbol}";
                                }

                                Console.WriteLine();
                                Console.WriteLine(outputMsg);
                                break;
                            case listAllOption:
                                if (movementAmountList.Count == 0)
                                {
                                    Console.WriteLine("No movements so far");
                                }
                                else
                                {
                                    Console.Write(
                                        $"{newLine}" +
                                        $"====================={newLine}" +
                                        $"=== All movements ==={newLine}" +
                                        $"====================={newLine}" +
                                        $"{newLine}"
                                    );
                                    Console.WriteLine();
                                    for (int i = 0; i < movementAmountList.Count; i++)
                                    {
                                        Console.WriteLine($"{movementInstantList[i]:dd/MM/yyyy-hh:mm:ss} | {movementAmountList[i]:0.00}{currencySymbol}");
                                    }
                                    Console.WriteLine($"              TOTAL | {userMoney:0.00}{currencySymbol}");
                                }
                                break;
                            case listIncomesOption:
                                if (movementAmountList.Where(x => x > 0).ToList().Count == 0)
                                {
                                    Console.WriteLine("No incomes so far");
                                }
                                else
                                {
                                    Console.Write(
                                        $"{newLine}" +
                                        $"==================={newLine}" +
                                        $"=== All incomes ==={newLine}" +
                                        $"==================={newLine}" +
                                        $"{newLine}"
                                    );
                                    decimal totalIncomes = 0;
                                    for (int i = 0; i < movementAmountList.Count; i++)
                                    {
                                        if (movementAmountList[i] > 0)
                                        {
                                            totalIncomes += movementAmountList[i];
                                            Console.WriteLine($"{movementInstantList[i]:dd/MM/yyyy-hh:mm:ss} | {movementAmountList[i]:0.00}{currencySymbol}");
                                        }
                                    }
                                    Console.WriteLine($"              TOTAL | {totalIncomes:0.00}{currencySymbol}");
                                }
                                break;
                            case listOutcomesOption:
                                if (movementAmountList.Where(x => x < 0).ToList().Count == 0)
                                {
                                    Console.WriteLine("No outcomes so far");
                                }
                                else
                                {
                                    Console.Write(
                                        $"{newLine}" +
                                        $"===================={newLine}" +
                                        $"=== All outcomes ==={newLine}" +
                                        $"===================={newLine}" +
                                        $"{newLine}"
                                    );
                                    decimal totalOutcomes = 0;
                                    for (int i = 0; i < movementAmountList.Count; i++)
                                    {
                                        if (movementAmountList[i] < 0)
                                        {
                                            totalOutcomes -= movementAmountList[i];
                                            Console.WriteLine($"{movementInstantList[i]:dd/MM/yyyy-hh:mm:ss} | {-movementAmountList[i]:0.00}{currencySymbol}");
                                        }
                                    }
                                    Console.WriteLine($"              TOTAL | {totalOutcomes:0.00}{currencySymbol}");
                                }
                                break;
                            case showMoneyOption:
                                Console.WriteLine();
                                Console.WriteLine($"Your current money amounts to {userMoney:0.00}{currencySymbol}");
                                break;
                        }
                        Console.WriteLine();
                        Console.Write($"Press any key to continue, or {quitSelection.ToString().ToUpper()} to exit main menu: ");
                        ConsoleKeyInfo userKeyPressed = Console.ReadKey();
                        if (userKeyPressed.KeyChar.ToString().ToLower() == quitSelection.ToString().ToLower())
                        {
                            exitProgram = true;
                            Console.WriteLine();
                            Console.Write($"Your current money amounts to {userMoney:0.00}{currencySymbol}, press any key to leave...");
                            Console.ReadKey();
                        }
                        break;
                    case exitOption:
                        exitProgram = true;
                        break;
                    default:
                        Console.WriteLine();
                        Console.Write($"{userOption} is not a valid option. Press any key to try again...");
                        Console.ReadKey();
                        break;
                }
            } while (!exitProgram);
        }
    }
}
