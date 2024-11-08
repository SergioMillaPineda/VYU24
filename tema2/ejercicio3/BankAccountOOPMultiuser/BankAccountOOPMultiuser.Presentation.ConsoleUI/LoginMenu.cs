using BankAccountOOPMultiuser.Business.Contracts;
using BankAccountOOPMultiuser.Business.Contracts.Dtos;
using BankAccountOOPMultiuser.XCutting.Configuration;
using BankAccountOOPMultiuser.XCutting.Session;

namespace BankAccountOOPMultiuser.Presentation.ConsoleUI
{
    internal class LoginMenu
    {
        private MainMenu _mainMenu;
        private readonly ICredentialsService _credentialsService;

        private readonly string _quitSelection;
        private string? _accountNumberAsString;
        private string? _accountPinAsString;
        private bool _exitProgram;

        public LoginMenu(ICredentialsService credentialsService, MainMenu mainMenu)
        {
            _mainMenu = mainMenu;
            _credentialsService = credentialsService;
            _quitSelection = ConsoleConfiguration.QuitSelection;
        }

        public void Execute()
        {
            do
            {
                Console.Clear();
                GetAccountNumber();
                if (UserWantsToQuit())
                {
                    _exitProgram = true;
                }
                else
                {
                    GetAccountPin();
                    if (!ValidCredentials())
                    {
                        Console.WriteLine("Wrong credentials. Press any key to retry...");
                        Console.ReadKey();
                    }
                    else
                    {
                        CurrentAccountSession.SelectedBankAccountNumber = _accountNumberAsString;
                        CurrentAccountSession.SelectedBankAccountPin = _accountPinAsString;

                        _mainMenu.Execute();
                    }
                }
            } while (!_exitProgram);

            SayGoodbye();
        }

        public void GetAccountNumber()
        {
            Console.Write($"Enter account number (or {_quitSelection.ToString().ToUpper()} to quit): ");
            _accountNumberAsString = Console.ReadLine()?.Trim();
        }

        public bool UserWantsToQuit()
        {
            return _accountNumberAsString?.ToLower() == _quitSelection.ToString().ToLower();
        }

        public void GetAccountPin()
        {
            Console.Write("Enter pin: ");
            _accountPinAsString = Console.ReadLine()?.Trim();
        }

        public bool ValidCredentials()
        {
            // user input format validation
            int parsedAccountNumber;
            bool isAccountNumberInt = int.TryParse(_accountNumberAsString, out parsedAccountNumber);
            int parsedAccountPin;
            bool isAccountPinInt = int.TryParse(_accountPinAsString, out parsedAccountPin);
            if (isAccountNumberInt && isAccountPinInt)
            {
                // data value validation
                return _credentialsService.ValidateCredentials(new CredentialsDto
                {
                    AccountName = parsedAccountNumber,
                    AccountPin = parsedAccountPin
                });
            }

            return false;
        }

        public void SayGoodbye()
        {
            Console.WriteLine();
            Console.WriteLine($"Shutting down application...");
            Console.WriteLine();
        }
    }
}
