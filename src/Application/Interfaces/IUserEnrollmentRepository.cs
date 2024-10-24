using Domain.Entities;

namespace Application.Interfaces;

public interface IUserEnrollmentRepository
{
	Task<UserEnrollment> GetUserEnrollment(int userId, int unitNumber);
	Task UpdateUserEnrollmentPaidStatus(int userId, int unitNumber, bool isPaid);
	Task<int> CreateUserEnrollment(int userId, int unitNumber, bool isPaid = false);
	Task EnrollUser(int userId, int unitNumber);
	Task<bool> CheckIfUserPaidForUnit(int userId, int unitNumber);
}
