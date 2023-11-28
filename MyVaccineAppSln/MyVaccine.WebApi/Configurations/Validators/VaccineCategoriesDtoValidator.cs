using FluentValidation;
using MyVaccine.WebApi.Dtos.VaccineCategory;

namespace MyVaccine.WebApi.Configurations.Validators;

public class VaccineCategoriesDtoValidator : AbstractValidator<VaccineCategoryRequestDto>
{
    public VaccineCategoriesDtoValidator()
    {
        RuleFor(dto => dto.Name).NotEmpty().MaximumLength(255);

    }

}
