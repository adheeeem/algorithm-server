using Application.DTOs;
using Application.Interfaces;
using Application.Requests;

namespace Application.Services;

public class SchoolService(ISchoolRepository schoolRepository)
{
	public async Task<int> AddNewSchool(CreateSchoolRequest request)
	{
		var schoolDto = new CreateSchoolDto { City = request.City, Country = request.Country, Name = request.Name, Region = request.Region };
		var id = await schoolRepository.CreateSchool(schoolDto);
		return id;
	}
}
