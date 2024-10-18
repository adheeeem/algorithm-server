using Application.DTOs;

namespace Application.Interfaces;

public interface ISchoolRepository
{
	Task<int> CreateSchool(CreateSchoolDto school); 
	Task<bool> CheckIfSchoolExists(int schoolId);
}
