using FluentValidation;
using MyVaccine.WebApi.Dtos.Vaccine;

namespace MyVaccine.WebApi.Configurations.Validators;

public class VaccinesDtoValidator : AbstractValidator<VaccineRequestDto>
{
    public VaccinesDtoValidator()
    {
        RuleFor(dto => dto.Name).NotEmpty().MaximumLength(255);

    }

}
