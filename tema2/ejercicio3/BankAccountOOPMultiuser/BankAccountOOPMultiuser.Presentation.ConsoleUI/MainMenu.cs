using BankAccountOOPMultiuser.Business.Contracts;
using BankAccountOOPMultiuser.Business.Contracts.Dtos;
using BankAccountOOPMultiuser.XCutting.Enums;
using BankAccountOOPMultiuser.XCutting.Session;

namespace BankAccountOOPMultiuser.Presentation.ConsoleUI
{
    internal class MainMenu
    {
        private readonly IAccountService _accountService;

        // - customized main menu options
        private const string incomeOption = "1";
        private const string outcomeOption = "2";
        private const string listAllOption = "3";
        private const string listIncomesOption = "4";
        private const string listOutcomesOption = "5";
        private const string showMoneyOption = "6";
        private const string exitOption = "7";

        // abstraction of line jump string
        private const string newLine = "\r\n";

        // bank business available currencies
        private List<string> AvailableCurrencies;

        private string currencySymbol;

        public MainMenu(IAccountService accountService)
        {
            _accountService = accountService;

            AvailableCurrencies = new()
            {
                "EUR",
                "USD"
            };

            // customized currency value
            string currency = AvailableCurrencies[1];

            // - business->presentation mapping: data model currency -> currency symbol to show on console
            Dictionary<string, string> currencySymbolDictionary = new()
            {
                { "EUR", "€" },
                { "USD", "$" }
            };
            currencySymbol = currencySymbolDictionary[currency];
        }

        public void Execute()
        {
            #region initial configuration
            // data model containing the information that will be managed by this program
            decimal userMoney = 0;
            List<decimal> movementAmountList = new();
            List<DateTime> movementInstantList = new();

            // data used during console interface interaction
            // - console custom configuration
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            // customized input user has to enter in order to leave program
            const char quitSelection = 'x';

            // - data model to manage user navigation
            string? userOption;
            bool exitProgram = false;
            #endregion

            // main menu full implementation code
            do
            {
                ShowMenu();
                Console.Write("Select an option: ");

                userOption = Console.ReadLine()?.Trim();

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
                                MoneyIncome();
                                break;
                            case outcomeOption:
                                MoneyOutcome();
                                break;
                            case listAllOption:
                                ListMovements();
                                break;
                            case listIncomesOption:
                                ListIncomes();
                                break;
                            case listOutcomesOption:
                                ListOutcomes();
                                break;
                            case showMoneyOption:
                                ShowMoney();
                                break;
                        }
                        Console.WriteLine();
                        Console.Write($"Press any key to continue, or {quitSelection.ToString().ToUpper()} to exit main menu: ");
                        ConsoleKeyInfo userKeyPressed = Console.ReadKey();
                        if (userKeyPressed.KeyChar.ToString().ToLower() == quitSelection.ToString().ToLower())
                        {
                            exitProgram = true;
                            Console.WriteLine();
                            Console.Write($"Your current money amounts to {_accountService.GetMoney():0.00}{currencySymbol}, press any key to leave...");
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

        public void ShowMenu()
        {
            Console.Clear();
            Console.Write(
                $"Account {CurrentAccountSession.SelectedBankAccountNumber}{newLine}" +
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
        }

        #region Option1: Money income
        public void MoneyIncome()
        {
            Console.Write("Enter desired income: ");
            string? incomeAsString = Console.ReadLine()?.Trim().Replace(",", "");

            decimal parsedIncome;
            bool isIncomeDecimal = decimal.TryParse(incomeAsString, out parsedIncome);

            string outputMsg = "";
            if (!isIncomeDecimal)
            {
                outputMsg = "Entered value is not a valid decimal";
            }
            else
            {
                IncomeResultDto incomeResult = _accountService.AddMoney(parsedIncome);
                if (incomeResult.ResultHasErrors)
                {
                    outputMsg = ManageIncomeError(incomeResult);
                }
                else
                {
                    outputMsg = $"Added {parsedIncome}{currencySymbol}. Your current money amounts to {incomeResult.totalMoney:0.00}{currencySymbol}";
                }
            }

            Console.WriteLine();
            Console.WriteLine(outputMsg);
        }

        private string ManageIncomeError(IncomeResultDto incomeResult)
        {
            return incomeResult.Error switch
            {
                IncomeErrorEnum.Negative => "Income has to be a positive amount",
                IncomeErrorEnum.OverMaxValue => $"Income cannot exceed {incomeResult.maxIncomeAllowed:0.00}{currencySymbol}",
                _ => "",
            };
        }
        #endregion

        #region Option2: Money outcome
        public void MoneyOutcome()
        {
            Console.Write("Enter desired outcome: ");
            string? outcomeAsString = Console.ReadLine()?.Trim().Replace(",", "");

            decimal parsedOutcome;
            bool isOutcomeDecimal = decimal.TryParse(outcomeAsString, out parsedOutcome);

            string outputMsg = "";
            if (!isOutcomeDecimal)
            {
                outputMsg = "Entered value is not a valid decimal";
            }
            else
            {
                OutcomeResultDto outcomeResult = _accountService.RetireMoney(parsedOutcome);
                if (outcomeResult.ResultHasErrors)
                {
                    outputMsg = ManageOutcomeError(outcomeResult);
                }
                else
                {
                    outputMsg = $"Retired {parsedOutcome}{currencySymbol}. Your current money amounts to {outcomeResult.totalMoney:0.00}{currencySymbol}";
                }
            }

            Console.WriteLine();
            Console.WriteLine(outputMsg);
        }

        private string ManageOutcomeError(OutcomeResultDto outcomeResult)
        {
            return outcomeResult.Error switch
            {
                OutcomeErrorEnum.Negative => "Outcome has to be a positive amount",
                OutcomeErrorEnum.OverMaxValue => $"Outcome cannot exceed {outcomeResult.maxOutcomeAllowed:0.00}{currencySymbol}",
                OutcomeErrorEnum.MaxAllowedDebtSurpassed => $"debt cannot surpass {Math.Abs(outcomeResult.maxDebtAllowed):0.00}{currencySymbol}",
                _ => "",
            };
        }
        #endregion

        #region Option3: List all movements
        public void ListMovements()
        {
            MovementListDto movementsInfo = _accountService.GetMovements();
            if (movementsInfo.movements.Count == 0)
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
                foreach (MovementDto movement in movementsInfo.movements)
                {
                    Console.WriteLine($"{movement.timestamp:dd/MM/yyyy-hh:mm:ss} | {movement.value:0.00}{currencySymbol}");
                }
                Console.WriteLine($"              TOTAL | {movementsInfo.totalMoney:0.00}{currencySymbol}");
            }
        }
        #endregion

        #region Option4: List incomes
        public void ListIncomes()
        {
            MovementListDto incomesInfo = _accountService.GetIncomes();
            if (incomesInfo.movements.Count == 0)
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
                Console.WriteLine();
                foreach (MovementDto movement in incomesInfo.movements)
                {
                    Console.WriteLine($"{movement.timestamp:dd/MM/yyyy-hh:mm:ss} | {movement.value:0.00}{currencySymbol}");
                }
                Console.WriteLine($"              TOTAL | {incomesInfo.totalMoney:0.00}{currencySymbol}");
            }
        }
        #endregion

        #region Option5: List outcomes
        public void ListOutcomes()
        {
            MovementListDto outcomesInfo = _accountService.GetOutcomes();
            if (outcomesInfo.movements.Count == 0)
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
                Console.WriteLine();
                foreach (MovementDto movement in outcomesInfo.movements)
                {
                    Console.WriteLine($"{movement.timestamp:dd/MM/yyyy-hh:mm:ss} | {movement.value:0.00}{currencySymbol}");
                }
                Console.WriteLine($"              TOTAL | {outcomesInfo.totalMoney:0.00}{currencySymbol}");
            }
        }
        #endregion

        #region Option6: Show current money
        public void ShowMoney()
        {
            try
            {
                Console.WriteLine();
                Console.WriteLine($"Your current money amounts to {_accountService.GetMoney():0.00}{currencySymbol}");
            }
            catch (Exception)
            {
                Console.WriteLine();
                Console.WriteLine($"Some error ocurred while trying to access account data");
            }
        }
        #endregion
    }
}
