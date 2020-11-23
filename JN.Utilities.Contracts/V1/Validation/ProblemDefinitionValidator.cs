using FluentValidation;
using JN.Utilities.Contracts.V1.Requests;

namespace JN.Utilities.Contracts.V1.Validation
{
    public class ProblemDefinitionValidator : AbstractValidator<ProblemDefinition>
    {
        public ProblemDefinitionValidator()
        {
            RuleFor(p => p.AvailableAmount).GreaterThan(0).WithMessage("Available Amount must be > 0");
            RuleFor(p => p.Products).NotEmpty().WithMessage("Must specify a list of products");
            RuleFor(p => p.Products).
                ForEach(y=>y.SetValidator(new ProductDetailsValidator()));
        }
    }
}
