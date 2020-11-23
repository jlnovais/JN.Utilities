using FluentValidation;
using JN.Utilities.Contracts.V1.Requests;

namespace JN.Utilities.Contracts.V1.Validation
{
    public class ProductDetailsValidator : AbstractValidator<ProductDetails>
    {
        public ProductDetailsValidator()
        {
            RuleFor(p => p.UnitPrice).GreaterThan(0).WithMessage("Unit price must be > 0");
            RuleFor(p => p.Name).NotEmpty().WithMessage("Must specify a name");
            RuleFor(p => p.MaxUnits).GreaterThanOrEqualTo(0).WithMessage("Max units must be >= 0");
            RuleFor(p => p.MinUnits).GreaterThanOrEqualTo(0).WithMessage("Min units must be >= 0");
            RuleFor(p => p.MaxUnits).GreaterThanOrEqualTo(p=>p.MinUnits).WithMessage("Max units must be >= Min units");
        }
    }
}