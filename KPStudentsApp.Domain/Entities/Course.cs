using KPStudentsApp.Domain.Common;

namespace KPStudentsApp.Domain.Entities
{
    public class Course : BaseEntity
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public int CreditUnit { get; set; }
        public virtual ICollection<Student> Students { get; set; }
    }
}
