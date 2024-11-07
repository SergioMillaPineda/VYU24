namespace WorkersManagementAdminMode.Domain.DomainModels
{
    public class Worker
    {
        private static int _counter = 1;

        public readonly int Id;
        public readonly string Name;
        public readonly string Surname;
        public readonly DateTime BirthDate;
        protected DateTime? LeavingDate;

        public Worker(
            string name,
            string surname,
            DateTime birthDate)
        {
            Id = _counter++;
            Name = name;
            Surname = surname;
            BirthDate = birthDate;
        }
    }
}
