using KPStudentsApp.Domain.Common;

namespace KPStudentsApp.Domain.Entities
{
    public class ReferenceNumber : BaseEntity
    {
        public string Dimension { get; set; }
        public long NextVal { get; set; }
    }
}
