using AutoMapper;
using Azure.Core;
using Microsoft.EntityFrameworkCore;
using MyVaccine.WebApi.Dtos.Vaccine;
using MyVaccine.WebApi.Models;
using MyVaccine.WebApi.Repositories.Contracts;
using MyVaccine.WebApi.Services.Contracts;

namespace MyVaccine.WebApi.Services.Implementations;

// para heredar, una referencia se hace de esta manera
public class VaccineService : IVaccineService
{
    private readonly IBaseRepository<Vaccine> _vaccineRepository;
    private readonly IMapper _mapper;
    public VaccineService(IBaseRepository<Vaccine> vaccineRepository, IMapper mapper)
    {
        _vaccineRepository = vaccineRepository;
        _mapper = mapper;
    }

    public async Task<VaccineResponseDto> Add(VaccineRequestDto request)
    {
        //var dependents = await _dependentRepository.FindBy(x => x.DependentId == id).FirstOrDefaultAsync();
        var vaccines = new Vaccine();
        vaccines.Name = request.Name;

        await _vaccineRepository.Add(vaccines);
        var response = _mapper.Map<VaccineResponseDto>(vaccines);
        return response;
    }

    public async Task<VaccineResponseDto> Delete(int id)
    {

        var vaccines = await _vaccineRepository.FindBy(x => x.VaccineId == id).FirstOrDefaultAsync();

        await _vaccineRepository.Delete(vaccines);
        var response = _mapper.Map<VaccineResponseDto>(vaccines);
        return response;
    }

    public async Task<IEnumerable<VaccineResponseDto>> GetAll()
    {
        var vaccines = await _vaccineRepository.GetAll().AsNoTracking().ToListAsync();
        var response = _mapper.Map<IEnumerable<VaccineResponseDto>>(vaccines);
        return response;
    }

    public async Task<VaccineResponseDto> GetById(int id)
    {
        var vaccines = await _vaccineRepository.FindBy(x => x.VaccineId == id).AsNoTracking().FirstOrDefaultAsync();
        var response = _mapper.Map<VaccineResponseDto>(vaccines);
        return response;
    }


    public async Task<VaccineResponseDto> Update(VaccineRequestDto request, int id)
    {
        var vaccines = await _vaccineRepository.FindBy(x => x.VaccineId == id).FirstOrDefaultAsync();
        vaccines.Name = request.Name;


        await _vaccineRepository.Update(vaccines);
        var response = _mapper.Map<VaccineResponseDto>(vaccines);
        return response;
    }
}
