using FluentValidation;
using MyVaccine.WebApi.Dtos.VaccineRecord;

namespace MyVaccine.WebApi.Configurations.Validators;

public class VaccineRecordsDtoValidator : AbstractValidator<VaccineRecordRequestDto>
{
    public VaccineRecordsDtoValidator()
    {
        RuleFor(dto => dto.AdministeredBy).NotEmpty().MaximumLength(255);
        RuleFor(dto => dto.AdministeredLocation).NotEmpty();
        RuleFor(dto => dto.DateAdministered).NotEmpty();
        RuleFor(dto => dto.DependentId).NotEmpty().GreaterThan(0);
        RuleFor(dto => dto.UserId).NotEmpty().GreaterThan(0);
        RuleFor(dto => dto.VaccineId).NotEmpty().GreaterThan(0);

    }

}
