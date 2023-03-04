using KPStudentsApp.Domain.Common;

namespace KPStudentsApp.Domain.Entities
{
    public class Student : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime RegisteredOn { get; set; }
        public string RegisterationNumber { get; set; }
        public virtual ICollection<Course> Courses { get; set; }
    }
}
