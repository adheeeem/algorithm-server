using Application.Exceptions;
using Application.Interfaces;
using Application.Responses;

namespace Application.Services;

public class UserEnrollmentService(IUnitOfWork unitOfWork)
{
	public async Task<UserEnrollmentResponse> GetUserEnrollmentResponse(int userId, int unitNumber)
	{
		var result = await unitOfWork.UserEnrollmentRepository.GetUserEnrollment(userId, unitNumber);
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
		if (await unitOfWork.UserRepository.GetUserById(userId) == null)
			throw new RecordNotFoundException("user with id number does not exist");
		if (await unitOfWork.UserEnrollmentRepository.GetUserEnrollment(userId, unitNumber) == null)
			throw new RecordNotFoundException("user enrollment not found");
		await unitOfWork.UserEnrollmentRepository.UpdateUserEnrollmentPaidStatus(userId, unitNumber, isPaid);
	}

	public async Task EnrollUser(int userId, int unitNumber)
	{
		if (unitNumber is < 1 or > 8)
			throw new BadRequestException("invalid unit number range.");
		if (await unitOfWork.UserRepository.GetUserById(userId) == null)
			throw new RecordNotFoundException("user with this userid does not exist.");
		if (!(await unitOfWork.UserEnrollmentRepository.CheckIfUserPaidForUnit(userId, unitNumber)))
			throw new BadRequestException("make payment before enrolling to the unit");

		await unitOfWork.UserEnrollmentRepository.EnrollUser(userId, unitNumber);
	}
}
