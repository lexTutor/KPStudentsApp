using FluentValidation;
using KPStudentsApp.Application.Common.Validator;

namespace KPStudentsApp.Application.Models.RequestModels
{
    public class UpdateStudentModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public List<int> CourseIds { get; set; }
    }

    public class UpdateStudentModelValidator : AbstractValidator<UpdateStudentModel>
    {
        public UpdateStudentModelValidator()
        {
            RuleFor(subject => subject.Id).NotEmpty().GreaterThanOrEqualTo(1);
            RuleFor(subject => subject.FirstName).Name();
            RuleFor(subject => subject.PhoneNumber).PhoneNumber();
            RuleFor(subject => subject.Email).EmailAddress();
            RuleFor(subject => subject.CourseIds).Must(x => x.Count <= 3 && x.Distinct().Count() == x.Count)
                .When(x => x.CourseIds != null && x.CourseIds.Count > 0).WithMessage("Courses must be unique and cannot be more than 3");
        }
    }
}
