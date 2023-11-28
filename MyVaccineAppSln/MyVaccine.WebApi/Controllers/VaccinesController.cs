using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MyVaccine.WebApi.Dtos.Vaccine;
using MyVaccine.WebApi.Models;
using MyVaccine.WebApi.Services.Contracts;

namespace MyVaccine.WebApi.Controllers;
//[Authorize]
[Route("api/[controller]")]
[ApiController]
public class VaccinesController : ControllerBase
{
    private readonly IVaccineService _vaccineService;
    private readonly IValidator<VaccineRequestDto> _validator;
    public VaccinesController(IVaccineService vaccineService, IValidator<VaccineRequestDto> validator)
    {

        _vaccineService = vaccineService;
        _validator = validator;
    }

    [HttpGet]

    public async Task<IActionResult> GetAll()
    {
        var vaccines = await _vaccineService.GetAll();
        return Ok(vaccines);
    }

    [AllowAnonymous]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var vaccines = await _vaccineService.GetById(id);
        return Ok(vaccines);
    }



    [HttpPost]

    public async Task<IActionResult> Create(VaccineRequestDto vaccineDto)
    {
        var validationResult = await _validator.ValidateAsync(vaccineDto);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }
        var vaccines = await _vaccineService.Add(vaccineDto);
        return Ok(vaccines);

    }

    [HttpPut("{id}")]

    public async Task<IActionResult> Update(int id, VaccineRequestDto vaccineDto)
    {
        var vaccines = await _vaccineService.Update(vaccineDto, id);
        if (vaccines == null)
        {
            return NotFound();
        }

        return Ok(vaccines);
    }

    [HttpDelete("{id}")]

    public async Task<IActionResult> Delete(int id)
    {

        var vaccines = await _vaccineService.Delete(id);
        if (vaccines == null)
        {
            return NotFound();
        }

        return Ok(vaccines);
    }


}
