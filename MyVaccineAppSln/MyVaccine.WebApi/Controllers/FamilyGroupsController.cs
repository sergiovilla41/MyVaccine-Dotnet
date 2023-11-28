using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MyVaccine.WebApi.Dtos.FamilyGroup;
using MyVaccine.WebApi.Models;
using MyVaccine.WebApi.Services.Contracts;

namespace MyVaccine.WebApi.Controllers;
//[Authorize]
[Route("api/[controller]")]
[ApiController]
public class FamilyGroupsController : ControllerBase
{
    private readonly IFamilyGroupService _familygroupService;
    private readonly IValidator<FamilyGroupRequestDto> _validator;
    public FamilyGroupsController(IFamilyGroupService familygroupService, IValidator<FamilyGroupRequestDto> validator)
    {

        _familygroupService = familygroupService;
        _validator = validator;
    }

    [HttpGet]

    public async Task<IActionResult> GetAll()
    {
        var familygroups = await _familygroupService.GetAll();
        return Ok(familygroups);
    }

    [AllowAnonymous]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var familygroups = await _familygroupService.GetById(id);
        return Ok(familygroups);
    }

    [HttpPost]

    public async Task<IActionResult> Create(FamilyGroupRequestDto familygroupsDto)
    {
        var validationResult = await _validator.ValidateAsync(familygroupsDto);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }
        var familygroups = await _familygroupService.Add(familygroupsDto);
        return Ok(familygroups);

    }

    [HttpPut("{id}")]

    public async Task<IActionResult> Update(int id, FamilyGroupRequestDto familygroupsDto)
    {
        var familygroups = await _familygroupService.Update(familygroupsDto, id);
        if (familygroups == null)
        {
            return NotFound();
        }

        return Ok(familygroups);
    }

    [HttpDelete("{id}")]

    public async Task<IActionResult> Delete(int id)
    {

        var familygroups = await _familygroupService.Delete(id);
        if (familygroups == null)
        {
            return NotFound();
        }

        return Ok(familygroups);
    }


}
