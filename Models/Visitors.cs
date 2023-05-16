using System.ComponentModel.DataAnnotations;

namespace VisitorManagementSystem.Models
{
    public class Visitors
    {
        public Guid Id { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        public string Business { get; set; }
        [Display(Name = "Date Arrived")]
        public DateTime DateIn { get; set; }
        [Display(Name = "Date Left")]
        public DateTime DateOut { get; set; }
      
        public Guid StaffNamesId { get; set; }
  [Display(Name = "Staff Person Visited")]
        // Reference navigation
        public StaffNames? StaffNames { get; set; }

    }
}
