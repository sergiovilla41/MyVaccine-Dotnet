using AutoMapper;
using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.IIS;
using Microsoft.EntityFrameworkCore;
using MyVaccine.WebApi.Dtos.VaccineRecord;
using MyVaccine.WebApi.Models;
using MyVaccine.WebApi.Repositories.Contracts;
using MyVaccine.WebApi.Services.Contracts;
using BadHttpRequestException = Microsoft.AspNetCore.Http.BadHttpRequestException;

namespace MyVaccine.WebApi.Services.Implementations;

// para heredar, una referencia se hace de esta manera
public class VaccineRecordService : IVaccineRecordService
{
    private readonly IBaseRepository<VaccineRecord> _vaccinerecordRepository;
    private readonly IMapper _mapper;
    public VaccineRecordService(IBaseRepository<VaccineRecord> vaccinerecordRepository, IMapper mapper)
    {
        _vaccinerecordRepository = vaccinerecordRepository;
        _mapper = mapper;
    }

    public async Task<VaccineRecordResponseDto> Add(VaccineRecordRequestDto request)
    {
        //var dependents = await _dependentRepository.FindBy(x => x.DependentId == id).FirstOrDefaultAsync();
        var vaccinerecords = new VaccineRecord();
        vaccinerecords.UserId = request.UserId;
        vaccinerecords.DateAdministered = request.DateAdministered;
        vaccinerecords.DependentId = request.DependentId;
        vaccinerecords.VaccineId = request.VaccineId;
        vaccinerecords.AdministeredLocation = request.AdministeredLocation;
        vaccinerecords.AdministeredBy = request.AdministeredBy;

        await _vaccinerecordRepository.Add(vaccinerecords);
        var response = _mapper.Map<VaccineRecordResponseDto>(vaccinerecords);
        return response;
    }

    public async Task<VaccineRecordResponseDto> Delete(int id)
    {

        var vaccinerecords = await _vaccinerecordRepository.FindBy(x => x.VaccineRecordId == id).FirstOrDefaultAsync();

        await _vaccinerecordRepository.Delete(vaccinerecords);
        var response = _mapper.Map<VaccineRecordResponseDto>(vaccinerecords);
        return response;
    }

    public async Task<IEnumerable<VaccineRecordResponseDto>> GetAll()
    {
        var vaccinerecords = await _vaccinerecordRepository.GetAll().AsNoTracking().ToListAsync();
        var response = _mapper.Map<IEnumerable<VaccineRecordResponseDto>>(vaccinerecords);
        return response;
    }

    public async Task<VaccineRecordResponseDto> GetById(int id)
    {
        var vaccinerecords = await _vaccinerecordRepository.FindBy(x => x.VaccineRecordId == id).AsNoTracking().FirstOrDefaultAsync();
        var response = _mapper.Map<VaccineRecordResponseDto>(vaccinerecords);
        return response;
    }


    public async Task<VaccineRecordResponseDto> Update(VaccineRecordRequestDto request, int id)
    {
        var vaccinerecords = await _vaccinerecordRepository.FindBy(x => x.VaccineRecordId == id).FirstOrDefaultAsync();
        if (vaccinerecords == null)
        {
            return new VaccineRecordResponseDto();
            ;
        }
        vaccinerecords.UserId = request.UserId;
        vaccinerecords.DateAdministered = request.DateAdministered;
        vaccinerecords.DependentId = request.DependentId;
        vaccinerecords.VaccineId = request.VaccineId;
        vaccinerecords.AdministeredLocation = request.AdministeredLocation;
        vaccinerecords.AdministeredBy = request.AdministeredBy;


        await _vaccinerecordRepository.Update(vaccinerecords);
        var response = _mapper.Map<VaccineRecordResponseDto>(vaccinerecords);
        return response;
    }
}
