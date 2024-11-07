namespace WorkersManagementAdminMode.Domain.DomainModels
{
    public partial class ITWorker: Worker
    {
        public int YearsOfExperience { get; set; }
        public List<string> TechKnowledges { get; set; }
        public string Level { get; set; }

        private readonly List<string> _validLevels;
        private const string SENIOR = "Senior";
        private const int MINEXPYEARS = 5;
        private const int MINYEARSTOWORK = 18;

        public ITWorker(
            string name,
            string surname,
            DateTime birthDate,
            int yearsOfExperience,
            List<string> techKnowledges,
            string level)
            : base(name, surname, birthDate)
        {
            YearsOfExperience = yearsOfExperience;
            TechKnowledges = techKnowledges;
            Level = level;

            _validLevels = new List<string> { "Junior", "Medium", SENIOR };
        }

        #region Validations
        public bool IsValidLevel {
            get
            {
                return _validLevels.Contains(Level);
            }
        }

        public bool CanBeAssignedAsTeamManager
        {
            get
            {
                return Level == SENIOR;
            }
        }

        public bool CanBeConsideredSenior
        {
            get
            {
                return YearsOfExperience >= MINEXPYEARS;
            }
        }

        public bool CanWork
        {
            get
            {
                return (new DateTime(1, 1, 1) + (DateTime.Now - BirthDate)).Year - 1 >= MINYEARSTOWORK;
            }
        }
        #endregion
    }
}
