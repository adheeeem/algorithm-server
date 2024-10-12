using Application.DTOs;

namespace Application.Interfaces;

public interface IWeekRepository
{
	Task<int> GetWeekId(int unitNumber, int weekNumber);
	Task<int> CreateWeek(CreateWeekDto week);
	Task<bool> CheckIfWeekExists(int unitNumber, int weekNumber);
}
