namespace WorkersManagementAdminMode.Library.Contracts.DTOs
{
    public class RegisterITWorkerRqDTO
    {
        public string Name;
        public string Surname;
        public DateTime BirthDate;
        public int YearsOfExperience;
        public List<string> TechKnowledges;
        public string Level;

        public RegisterITWorkerRqDTO(
            string name,
            string surname,
            DateTime birthDate,
            int yearsOfExperience,
            List<string> techKnowledges,
            string level)
        {
            Name = name;
            Surname = surname;
            BirthDate = birthDate;
            YearsOfExperience = yearsOfExperience;
            TechKnowledges = techKnowledges;
            Level = level;
        }
    }
}
