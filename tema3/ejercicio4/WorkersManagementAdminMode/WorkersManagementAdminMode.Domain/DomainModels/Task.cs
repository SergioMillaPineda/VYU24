namespace WorkersManagementAdminMode.Domain.DomainModels
{
    public class Task
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Technology { get; set; }
        public string Status { get; set; }
        public int IdWorker { get; set; }

        private readonly List<string> _validStatuses;
        private const string DONE = "Done";

        public Task(
            int id,
            string description,
            string technology,
            string status,
            int idWorker)
        {
            Id = id;
            Description = description;
            Technology = technology;
            Status = status;
            IdWorker = idWorker;

            _validStatuses = new List<string> { "To do", "Doing", DONE };
        }

        public bool IsValidStatus
        {
            get
            {
                return _validStatuses.Contains(Status);
            }
        }

        public bool CanBeAssignedToWorker(ITWorker worker)
        {
            return worker.TechKnowledges.Contains(Technology) && Status != DONE;
        }
    }
}
