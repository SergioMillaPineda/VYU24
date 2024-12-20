﻿namespace WorkersManagementAdminMode.Library.Contracts.DTOs
{
    public class RegisterITWorkerRqDTO
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime BirthDate { get; set; }
        public int YearsOfExperience { get; set; }
        public List<string> TechKnowledges { get; set; }
        public string Level { get; set; }

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
