using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MyVaccine.WebApi.Dtos.VaccineCategory;
using MyVaccine.WebApi.Models;
using MyVaccine.WebApi.Services.Contracts;

namespace MyVaccine.WebApi.Controllers;
//[Authorize]
[Route("api/[controller]")]
[ApiController]
public class VaccineCategoriesController : ControllerBase
{
    private readonly IVaccineCategoryService _vaccinecategoryService;
    private readonly IValidator<VaccineCategoryRequestDto> _validator;
    public VaccineCategoriesController(IVaccineCategoryService vaccinecategoryService, IValidator<VaccineCategoryRequestDto> validator)
    {

        _vaccinecategoryService = vaccinecategoryService;
        _validator = validator;
    }

    [HttpGet]

    public async Task<IActionResult> GetAll()
    {
        var vaccinecategories = await _vaccinecategoryService.GetAll();
        return Ok(vaccinecategories);
    }

    [AllowAnonymous]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var vaccinecategories = await _vaccinecategoryService.GetById(id);
        return Ok(vaccinecategories);
    }



    [HttpPost]

    public async Task<IActionResult> Create(VaccineCategoryRequestDto vaccinecategoryDto)
    {
        var validationResult = await _validator.ValidateAsync(vaccinecategoryDto);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }
        var vaccinecategories = await _vaccinecategoryService.Add(vaccinecategoryDto);
        return Ok(vaccinecategories);

    }

    [HttpPut("{id}")]

    public async Task<IActionResult> Update(int id, VaccineCategoryRequestDto vaccinecategoryDto)
    {
        var vaccinecategories = await _vaccinecategoryService.Update(vaccinecategoryDto, id);
        if (vaccinecategories == null)
        {
            return NotFound();
        }

        return Ok(vaccinecategories);
    }

    [HttpDelete("{id}")]

    public async Task<IActionResult> Delete(int id)
    {

        var vaccinecategories = await _vaccinecategoryService.Delete(id);
        if (vaccinecategories == null)
        {
            return NotFound();
        }

        return Ok(vaccinecategories);
    }


}
