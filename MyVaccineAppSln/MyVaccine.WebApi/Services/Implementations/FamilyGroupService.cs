using AutoMapper;
using Azure.Core;
using Microsoft.EntityFrameworkCore;
using MyVaccine.WebApi.Dtos.FamilyGroup;
using MyVaccine.WebApi.Models;
using MyVaccine.WebApi.Repositories.Contracts;
using MyVaccine.WebApi.Services.Contracts;

namespace MyVaccine.WebApi.Services.Implementations;

public class FamilyGroupService : IFamilyGroupService
{
    private readonly IBaseRepository<FamilyGroup> _familygroupRepository;
    private readonly IMapper _mapper;
    public FamilyGroupService(IBaseRepository<FamilyGroup> familygroupRepository, IMapper mapper)
    {
        _familygroupRepository = familygroupRepository;
        _mapper = mapper;
    }

    public async Task<FamilyGroupResponseDto> Add(FamilyGroupRequestDto request)
    {
        //var dependents = await _dependentRepository.FindBy(x => x.DependentId == id).FirstOrDefaultAsync();
        var familygroups = new FamilyGroup();
        familygroups.Name = request.Name;

        await _familygroupRepository.Add(familygroups);
        var response = _mapper.Map<FamilyGroupResponseDto>(familygroups);
        return response;
    }

    public async Task<FamilyGroupResponseDto> Delete(int id)
    {

        var familygroups = await _familygroupRepository.FindBy(x => x.FamilyGroupId == id).FirstOrDefaultAsync();

        await _familygroupRepository.Delete(familygroups);
        var response = _mapper.Map<FamilyGroupResponseDto>(familygroups);
        return response;
    }

    public async Task<IEnumerable<FamilyGroupResponseDto>> GetAll()
    {
        var familygroups = await _familygroupRepository.GetAll().AsNoTracking().ToListAsync();
        var response = _mapper.Map<IEnumerable<FamilyGroupResponseDto>>(familygroups);
        return response;
    }

    public async Task<FamilyGroupResponseDto> GetById(int id)
    {
        var familygroups = await _familygroupRepository.FindBy(x => x.FamilyGroupId == id).AsNoTracking().FirstOrDefaultAsync();
        var response = _mapper.Map<FamilyGroupResponseDto>(familygroups);
        return response;
    }


    public async Task<FamilyGroupResponseDto> Update(FamilyGroupRequestDto request, int id)
    {
        var familygroups = await _familygroupRepository.FindBy(x => x.FamilyGroupId == id).FirstOrDefaultAsync();
        familygroups.Name = request.Name;

        await _familygroupRepository.Update(familygroups);
        var response = _mapper.Map<FamilyGroupResponseDto>(familygroups);
        return response;
    }
}
