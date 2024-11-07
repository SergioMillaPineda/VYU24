namespace WorkersManagementAdminMode.Domain.DomainModels
{
    public class Team
    {
        public Worker Manager { get; set; }
        public List<Worker> Technicians { get; set; }

        public Team(Worker manager, List<Worker> technicians)
        {
            Manager = manager;
            Technicians = technicians;
        }
    }
}
