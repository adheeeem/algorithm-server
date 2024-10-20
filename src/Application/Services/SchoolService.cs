using Application.DTOs;
using Application.Interfaces;
using Application.Requests;
using Domain.Entities;

namespace Application.Services;

public class SchoolService(ISchoolRepository schoolRepository)
{
	public async Task<int> AddNewSchool(CreateSchoolRequest request)
	{
		var schoolDto = new CreateSchoolDto { City = request.City, Country = request.Country, Name = request.Name, Region = request.Region };
		var id = await schoolRepository.CreateSchool(schoolDto);
		return id;
	}
	public async Task<List<School>> GetSchools(int limit = 0, int page = 0, string name = "", string region = "", string city = "", string country = "")
	{
		var response = await schoolRepository.GetSchools(limit, page, name, region, city, country);
		return response;
	}
}
