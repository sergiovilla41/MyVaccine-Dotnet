using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MyVaccine.WebApi.Dtos.VaccineRecord;
using MyVaccine.WebApi.Models;
using MyVaccine.WebApi.Services.Contracts;

namespace MyVaccine.WebApi.Controllers;
//[Authorize]
[Route("api/[controller]")]
[ApiController]
public class VaccineRecordsController : ControllerBase
{
    private readonly IVaccineRecordService _vaccinerecordService;
    private readonly IValidator<VaccineRecordRequestDto> _validator;
    public VaccineRecordsController(IVaccineRecordService vaccinerecordService, IValidator<VaccineRecordRequestDto> validator)
    {

        _vaccinerecordService = vaccinerecordService;
        _validator = validator;
    }

    [HttpGet]

    public async Task<IActionResult> GetAll()
    {
        var vaccinerecords = await _vaccinerecordService.GetAll();
        return Ok(vaccinerecords);
    }

    [AllowAnonymous]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var vaccinerecords = await _vaccinerecordService.GetById(id);
        return Ok(vaccinerecords);
    }



    [HttpPost]

    public async Task<IActionResult> Create(VaccineRecordRequestDto vaccinerecordDto)
    {
        var validationResult = await _validator.ValidateAsync(vaccinerecordDto);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }
        var vaccinerecords = await _vaccinerecordService.Add(vaccinerecordDto);
        return Ok(vaccinerecords);

    }

    [HttpPut("{id}")]

    public async Task<IActionResult> Update(int id, VaccineRecordRequestDto vaccinerecordDto)
    {
        var vaccinerecords = await _vaccinerecordService.Update(vaccinerecordDto, id);
        if (vaccinerecords == null)
        {
            return NotFound();
        }

        return Ok(vaccinerecords);
    }

    [HttpDelete("{id}")]

    public async Task<IActionResult> Delete(int id)
    {

        var vaccinerecords = await _vaccinerecordService.Delete(id);
        if (vaccinerecords == null)
        {
            return NotFound();
        }

        return Ok(vaccinerecords);
    }


}
