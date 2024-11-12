using System.Data;
using Domain.Entities;

namespace Application.Interfaces.Repository;

public interface IUserEnrollmentRepository
{
	Task<UserEnrollment> GetUserEnrollment(int userId, int unitNumber);
	Task UpdateUserEnrollmentPaidStatus(int userId, int unitNumber, bool isPaid);
	Task<int> CreateUserEnrollment(int userId, int unitNumber, bool isPaid = false, IDbTransaction? transaction = null);
	Task EnrollUser(int userId, int unitNumber);
	Task<bool> CheckIfUserPaidForUnit(int userId, int unitNumber);
}
