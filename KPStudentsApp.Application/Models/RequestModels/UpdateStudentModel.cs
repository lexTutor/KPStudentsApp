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
            RuleFor(subject => subject.CourseIds).NotEmpty().Must(x => x.Count >= 1 && x.Count <= 3).When(x=> x.CourseIds != null)
                .WithMessage("Students Must have between 1 and 3 courses");
        }
    }
}
