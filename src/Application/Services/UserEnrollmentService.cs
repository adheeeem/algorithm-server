using Application.Exceptions;
using Application.Interfaces;
using Application.Responses;
using System.Runtime.CompilerServices;

namespace Application.Services;

public class UserEnrollmentService(IUserEnrollmentRepository userEnrollmentRepository, IUserRepository userRepository)
{
	public async Task<UserEnrollmentResponse> GetUserEnrollmentResponse(int userId, int unitNumber)
	{
		var result = await userEnrollmentRepository.GetUserEnrollment(userId, unitNumber);
		if (result == null)
			throw new RecordNotFoundException("user enrollment not found");
		var response = new UserEnrollmentResponse()
		{
			UnitNumber = result.UnitNumber,
			IsCompleted = result.IsCompleted,
			Paid = result.Paid,
			Enrolled = result.Enrolled,
			Date = result.Date,
		};
		return response;
	}

	public async Task UpdateUserEnrollmentPaidStatus(int userId, int unitNumber, bool isPaid)
	{
		if (await userRepository.GetUserById(userId) == null)
			throw new RecordNotFoundException("user with id number does not exist");
		if (await userEnrollmentRepository.GetUserEnrollment(userId, unitNumber) == null)
			throw new RecordNotFoundException("user enrollment not found");
		await userEnrollmentRepository.UpdateUserEnrollmentPaidStatus(userId, unitNumber, isPaid);
	}

	public async Task EnrollUser(int userId, int unitNumber)
	{
		if (unitNumber < 1 || unitNumber > 8)
			throw new BadRequestException("invalid unit number range.");
		if (await userRepository.GetUserById(userId) == null)
			throw new RecordNotFoundException("user with this userid does not exist.");
		if (!(await userEnrollmentRepository.CheckIfUserPaidForUnit(userId, unitNumber)))
			throw new BadRequestException("make payment before enrolling to the unit");

		await userEnrollmentRepository.EnrollUser(userId, unitNumber);
	}
}
