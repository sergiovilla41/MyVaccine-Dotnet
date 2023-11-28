using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MyVaccine.WebApi.Dtos.Allergy;
using MyVaccine.WebApi.Models;
using MyVaccine.WebApi.Services.Contracts;

namespace MyVaccine.WebApi.Controllers;
[Authorize]
[Route("api/[controller]")]
[ApiController]
public class AllergiesController : ControllerBase
{
    private readonly IAllergyService _allergyService;
    private readonly IValidator<AllergyRequestDto> _validator;
    public AllergiesController(IAllergyService allergyService, IValidator<AllergyRequestDto> validator)
    {

        _allergyService = allergyService;
        _validator = validator;
    }

    [HttpGet]

    public async Task<IActionResult> GetAll()
    {
        var allergies = await _allergyService.GetAll();
        return Ok(allergies);
    }

    [AllowAnonymous]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var allergies = await _allergyService.GetById(id);
        return Ok(allergies);
    }

    [HttpGet("get-allergies-by-userid/{userId}")]
    public async Task<IActionResult> GetAllergiesByUserId(int userId)
    {
        var allergies = await _allergyService.GetAllergyByUserId(userId);
        return Ok(allergies);
    }

    [HttpPost]

    public async Task<IActionResult> Create(AllergyRequestDto allergiesDto)
    {
        var validationResult = await _validator.ValidateAsync(allergiesDto);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }
        var allergies = await _allergyService.Add(allergiesDto);
        return Ok(allergies);

    }

    [HttpPut("{id}")]

    public async Task<IActionResult> Update(int id, AllergyRequestDto allergiesDto)
    {
        var allergies = await _allergyService.Update(allergiesDto, id);
        if (allergies == null)
        {
            return NotFound();
        }

        return Ok(allergies);
    }

    [HttpDelete("{id}")]

    public async Task<IActionResult> Delete(int id)
    {

        var allergies = await _allergyService.Delete(id);
        if (allergies == null)
        {
            return NotFound();
        }

        return Ok(allergies);
    }


}
