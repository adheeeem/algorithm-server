using Application.DTOs;

namespace Application.Interfaces;

public interface ISchoolRepository
{
	Task<int> AddNewSchool(CreateSchoolDto school); 
}
