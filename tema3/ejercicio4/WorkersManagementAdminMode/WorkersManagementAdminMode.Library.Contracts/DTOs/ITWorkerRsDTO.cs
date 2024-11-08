namespace WorkersManagementAdminMode.Library.Contracts.DTOs
{
    public class ITWorkerRsDTO
    {
        public int Id;
        public string Name;
        public string Surname;
        public string BirthDate;
        public int YearsOfExperience;
        public List<string> TechKnowledges;
        public string Level;

        public ITWorkerRsDTO(
            int id,
            string name,
            string surname,
            DateTime birthDate,
            int yearsOfExperience,
            List<string> techKnowledges,
            string level)
        {
            Id = id;
            Name = name;
            Surname = surname;
            BirthDate = birthDate.ToString("dd/MM/yyyy");
            YearsOfExperience = yearsOfExperience;
            TechKnowledges = techKnowledges;
            Level = level;
        }
    }
}
