using FluentValidation;
using Validator;

namespace WebApi.Validator
{
    public class PatientValidator: AbstractValidator<PatientEnitities>
    {
        public PatientValidator()
        {
            RuleFor(x => x.ForeName)
                .NotEmpty()
                .WithMessage("The ForeName cannot be blank.")
                .Length(3)
                .WithMessage("The Product Name cannot be less than 3 characters.");

            RuleFor(x => x.SurName)
                .NotEmpty()
                .WithMessage("The SurName cannot be blank.")
                .Length(2)
                .WithMessage("The SurName cannot be less than 2 characters.");

            RuleFor(x => x.Gender)
                .NotEmpty()
                .WithMessage("The Gender cannot be blank.");
            
        }
    }
}