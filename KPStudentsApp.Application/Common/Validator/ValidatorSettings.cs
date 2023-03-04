using FluentValidation;

namespace KPStudentsApp.Application.Common.Validator
{
    public static class ValidatorSettings
    {
        public static IRuleBuilder<T, string> Name<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            var options = ruleBuilder.NotEmpty().WithMessage("Name must be provided")
                .Matches("[A-Za-z]").WithMessage("Name can only contain alphabeths")
                .MinimumLength(2).WithMessage("Name is limited to a minimum of 2 characters");

            return options;
        }

        public static IRuleBuilder<T, string> PhoneNumber<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            var options = ruleBuilder.NotEmpty()
                .Matches(@"^[0-9]*$").WithMessage("Invalid Phone number");

            return options;
        }
    }
}
