namespace Application.Interfaces;

using Repository;

public interface IUnitOfWork : IDisposable
{
    IWeekRepository WeekRepository { get; }
    IUserRepository UserRepository { get; }
    ISchoolRepository SchoolRepository { get; }
    IUserWeeklyActivityRepository UserWeeklyActivityRepository { get; }
    IQuestionRepository QuestionRepository { get; } 
    IUserEnrollmentRepository UserEnrollmentRepository { get; }
    IQuestionAttemptRepository QuestionAttemptRepository { get; }
    IAttemptResultRepository AttemptResultRepository { get; }
    void Commit();
    void Rollback();
}