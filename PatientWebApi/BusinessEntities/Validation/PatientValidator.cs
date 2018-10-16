

namespace BusinessEntities
{
    public class PatientValidator: AbstractValidator<PatientEnitities>
    {
        public PatientValidator()
        {
            RuleFor(x => x.ForeName)
                .NotEmpty()
                .WithMessage("The ForeName cannot be blank.");


            RuleFor(x => x.SurName)
                .NotEmpty()
                .WithMessage("The SurName cannot be blank.");


            RuleFor(x => x.Gender)
                .NotEmpty()
                .WithMessage("The Gender cannot be blank.");
            
        }
    }
}