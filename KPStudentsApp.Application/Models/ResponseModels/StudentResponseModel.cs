namespace KPStudentsApp.Application.Models.ResponseModels
{
    public class StudentResponseModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime RegisteredOn { get; set; }
        public string RegisterationNumber { get; set; }
        public IReadOnlyCollection<CourseResponseModel> Courses { get; set; }
    }

    public class StudentResponseModelSlim
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime RegisteredOn { get; set; }
        public string RegisterationNumber { get; set; }
    }
}
