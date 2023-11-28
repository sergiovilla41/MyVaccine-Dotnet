using AutoMapper;
using Azure.Core;
using Microsoft.EntityFrameworkCore;
using MyVaccine.WebApi.Dtos.Allergy;
using MyVaccine.WebApi.Models;
using MyVaccine.WebApi.Repositories.Contracts;
using MyVaccine.WebApi.Services.Contracts;

namespace MyVaccine.WebApi.Services.Implementations;

// para heredar, una referencia se hace de esta manera
public class AllergyService : IAllergyService
{
    private readonly IBaseRepository<Allergy> _allergyRepository;
    private readonly IMapper _mapper;
    public AllergyService(IBaseRepository<Allergy> allergyRepository, IMapper mapper)
    {
        _allergyRepository = allergyRepository;
        _mapper = mapper;
    }

    public async Task<AllergyResponseDto> Add(AllergyRequestDto request)
    {
        //var dependents = await _dependentRepository.FindBy(x => x.DependentId == id).FirstOrDefaultAsync();
        var allergies = new Allergy();
        allergies.Name = request.Name;
        allergies.UserId = request.UserId;

        await _allergyRepository.Add(allergies);
        var response = _mapper.Map<AllergyResponseDto>(allergies);
        return response;
    }

    public async Task<AllergyResponseDto> Delete(int id)
    {

        var allergies = await _allergyRepository.FindBy(x => x.AllergyId == id).FirstOrDefaultAsync();

        await _allergyRepository.Delete(allergies);
        var response = _mapper.Map<AllergyResponseDto>(allergies);
        return response;
    }

    public async Task<IEnumerable<AllergyResponseDto>> GetAll()
    {
        var allergies = await _allergyRepository.GetAll().AsNoTracking().ToListAsync();
        var response = _mapper.Map<IEnumerable<AllergyResponseDto>>(allergies);
        return response;
    }

    public async Task<AllergyResponseDto> GetById(int id)
    {
        var allergies = await _allergyRepository.FindBy(x => x.AllergyId == id).AsNoTracking().FirstOrDefaultAsync();
        var response = _mapper.Map<AllergyResponseDto>(allergies);
        return response;
    }


    public async Task<IEnumerable<AllergyResponseDto>> GetAllergyByUserId(int userId)
    {
        var allergies = await _allergyRepository.FindBy(x => x.UserId == userId).ToListAsync();
        var response = _mapper.Map<IEnumerable<AllergyResponseDto>>(allergies);
        return response;
    }

    public async Task<AllergyResponseDto> Update(AllergyRequestDto request, int id)
    {
        var allergies = await _allergyRepository.FindBy(x => x.AllergyId == id).FirstOrDefaultAsync();
        allergies.Name = request.Name;
        allergies.UserId = request.UserId;

        await _allergyRepository.Update(allergies);
        var response = _mapper.Map<AllergyResponseDto>(allergies);
        return response;
    }
}
