using FluentValidation;
using ProductApi.Models;

namespace ProductApi.Validator;

public class CreateProductValidator:AbstractValidator<CreateProductModel>
{
    public CreateProductValidator()
    {
        RuleFor(p => p.Name).NotNull().MinimumLength(8).MaximumLength(50);
        RuleFor(p=>p.Description).NotNull().MinimumLength(1).MaximumLength(200);
    }
}