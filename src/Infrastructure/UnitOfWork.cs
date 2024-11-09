using System.Data;
using Application.Interfaces.Repository;
using Application.Interfaces;

namespace Infrastructure;

public class UnitOfWork : IUnitOfWork
{
    private IDbTransaction _transaction;
    public IWeekRepository WeekRepository { get; }
    public IUserRepository UserRepository { get; set; }
    public ISchoolRepository SchoolRepository { get; }
    public IUserWeeklyActivityRepository UserWeeklyActivityRepository { get; }
    public IQuestionRepository QuestionRepository { get; }
    public IUserEnrollmentRepository UserEnrollmentRepository { get; }
    private bool _disposed;
    
    public UnitOfWork(IDbConnection connection,
        IWeekRepository weekRepository,
        IUserRepository userRepository,
        ISchoolRepository schoolRepository,
        IUserWeeklyActivityRepository userWeeklyActivityRepository,
        IQuestionRepository questionRepository,
        IUserEnrollmentRepository userEnrollmentRepository
        )
    {
        var connectionState = connection.State == ConnectionState.Closed;
        if (connectionState)
            connection.Open();
        _transaction = connection.BeginTransaction();
        WeekRepository = weekRepository;
        UserRepository = userRepository;
        SchoolRepository = schoolRepository;
        UserWeeklyActivityRepository = userWeeklyActivityRepository;
        QuestionRepository = questionRepository;
        UserEnrollmentRepository = userEnrollmentRepository;
    }
    
    public void Commit()
    {
        try
        {
            _transaction.Commit();
            _transaction.Connection?.Close();
        }
        catch
        {
            _transaction.Rollback();
            throw;
        }
        finally
        {
            _transaction.Dispose();
            _transaction.Connection?.Dispose();
        }
    }

    public void Rollback()
    {
        try
        {
            _transaction.Rollback();
            _transaction.Connection?.Close();
        }
        finally
        {
            _transaction.Dispose();
            _transaction.Connection?.Dispose();
        }
    }

    private void dispose(bool disposing)
    {
        if (!_disposed)
        {
            if(disposing)
            {
                if (_transaction != null)
                {
                    _transaction.Dispose();
                    _transaction = null;
                }
            }
            _disposed = true;
        }
    }
    
    public void Dispose()
    {
        dispose(true);
        GC.SuppressFinalize(this);
    }

    ~UnitOfWork()
    {
        dispose(false);
    }
}