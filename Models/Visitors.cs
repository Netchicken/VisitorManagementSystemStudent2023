namespace VisitorManagementSystem.Models
{
    public class Visitors
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Business { get; set; }
        public DateTime DateIn { get; set; }
        public DateTime DateOut { get; set; }
        public Guid StaffNamesId { get; set; }

        //join
        public StaffNames StaffNames { get; set; }

    }
}
