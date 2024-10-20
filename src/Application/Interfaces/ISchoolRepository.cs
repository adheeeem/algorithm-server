using Application.DTOs;
using Domain.Entities;

namespace Application.Interfaces;

public interface ISchoolRepository
{
	Task<int> CreateSchool(CreateSchoolDto school);
	Task<bool> CheckIfSchoolExists(int schoolId);
	Task<List<School>> GetSchools(int limit = 0, int page = 0, string name = "", string region = "", string city = "", string country = "");
}
