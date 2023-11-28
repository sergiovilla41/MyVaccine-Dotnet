using AutoMapper;
using Azure.Core;
using Microsoft.EntityFrameworkCore;
using MyVaccine.WebApi.Dtos.VaccineCategory;
using MyVaccine.WebApi.Models;
using MyVaccine.WebApi.Repositories.Contracts;
using MyVaccine.WebApi.Services.Contracts;

namespace MyVaccine.WebApi.Services.Implementations;

// para heredar, una referencia se hace de esta manera
public class VaccineCategoryService : IVaccineCategoryService
{
    private readonly IBaseRepository<VaccineCategory> _vaccinecategoryRepository;
    private readonly IMapper _mapper;
    public VaccineCategoryService(IBaseRepository<VaccineCategory> vaccinecategoryRepository, IMapper mapper)
    {
        _vaccinecategoryRepository = vaccinecategoryRepository;
        _mapper = mapper;
    }

    public async Task<VaccineCategoryResponseDto> Add(VaccineCategoryRequestDto request)
    {
        //var dependents = await _dependentRepository.FindBy(x => x.DependentId == id).FirstOrDefaultAsync();
        var vaccinecategories = new VaccineCategory();
        vaccinecategories.Name = request.Name;

        await _vaccinecategoryRepository.Add(vaccinecategories);
        var response = _mapper.Map<VaccineCategoryResponseDto>(vaccinecategories);
        return response;
    }

    public async Task<VaccineCategoryResponseDto> Delete(int id)
    {

        var vaccinecategories = await _vaccinecategoryRepository.FindBy(x => x.VaccineCategoryId == id).FirstOrDefaultAsync();

        await _vaccinecategoryRepository.Delete(vaccinecategories);
        var response = _mapper.Map<VaccineCategoryResponseDto>(vaccinecategories);
        return response;
    }

    public async Task<IEnumerable<VaccineCategoryResponseDto>> GetAll()
    {
        var vaccinecategories = await _vaccinecategoryRepository.GetAll().AsNoTracking().ToListAsync();
        var response = _mapper.Map<IEnumerable<VaccineCategoryResponseDto>>(vaccinecategories);
        return response;
    }

    public async Task<VaccineCategoryResponseDto> GetById(int id)
    {
        var vaccinecategories = await _vaccinecategoryRepository.FindBy(x => x.VaccineCategoryId == id).AsNoTracking().FirstOrDefaultAsync();
        var response = _mapper.Map<VaccineCategoryResponseDto>(vaccinecategories);
        return response;
    }


    public async Task<VaccineCategoryResponseDto> Update(VaccineCategoryRequestDto request, int id)
    {
        var vaccinecategories = await _vaccinecategoryRepository.FindBy(x => x.VaccineCategoryId == id).FirstOrDefaultAsync();
        vaccinecategories.Name = request.Name;


        await _vaccinecategoryRepository.Update(vaccinecategories);
        var response = _mapper.Map<VaccineCategoryResponseDto>(vaccinecategories);
        return response;
    }
}
