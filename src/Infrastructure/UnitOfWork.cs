using System.Data;
using Application.Interfaces;

namespace Infrastructure;

public class UnitOfWork : IUnitOfWork
{
    private IDbTransaction _transaction;
    public IWeekRepository WeekRepository { get; }
    private IDbConnection _connection;
    public IUserRepository UserRepository { get; }
    public ISchoolRepository SchoolRepository { get; }
    public IUserWeeklyActivity UserWeeklyActivity { get; }
    private bool _disposed;
    
    public UnitOfWork(IDbConnection connection)
    {
        _transaction = connection.BeginTransaction();
        _connection = connection;
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
                if(_connection != null)
                {
                    _connection.Dispose();
                    _connection = null;
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