using FluentValidation;

public class CreateProductDTOValidator : AbstractValidator<CreateProductDTO>
{
    public CreateProductDTOValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.Description)
            .NotEmpty();

        RuleFor(x => x.Price)
            .GreaterThan(0);

        RuleFor(x => x.Stock)
            .GreaterThanOrEqualTo(0);
    }
}