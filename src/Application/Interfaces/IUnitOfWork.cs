namespace Application.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IWeekRepository WeekRepository { get; }
    IUserRepository UserRepository { get; }
    ISchoolRepository SchoolRepository { get; }
    IUserWeeklyActivity UserWeeklyActivity { get; }
    void Commit();
    void Rollback();
}