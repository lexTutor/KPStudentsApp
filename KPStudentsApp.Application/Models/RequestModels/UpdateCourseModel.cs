using FluentValidation;

namespace KPStudentsApp.Application.Models.RequestModels
{
    public class UpdateCourseModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public int CreditUnit { get; set; }
    }

    public class UpdateCourseModelValidator : AbstractValidator<UpdateCourseModel>
    {
        public UpdateCourseModelValidator()
        {
            RuleFor(course => course.Id).NotEmpty();
            RuleFor(course => course.Name).NotEmpty().Length(1, 250);
            RuleFor(course => course.CreditUnit).InclusiveBetween(1, 6);
            RuleFor(course => course.Code).NotEmpty().Length(3, 10);
        }
    }
}
