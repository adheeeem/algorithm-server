using Application.DTOs;
using Application.Interfaces;
using Application.Requests;

namespace Application.Services;

public class SchoolService(ISchoolRepository schoolRepository)
{
	public async Task<int> AddNewSchool(AddNewSchoolRequest request)
	{
		var schoolDto = new AddNewSchoolDto { City = request.City, Country = request.Country, Name = request.Name, Region = request.Region };
		var id = await schoolRepository.AddNewSchool(schoolDto);
		return id;
	}
}
