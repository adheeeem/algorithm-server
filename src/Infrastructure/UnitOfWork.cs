﻿using System.Data;
using Application.Interfaces.Repository;
using Application.Interfaces;

namespace Infrastructure;

public class UnitOfWork : IUnitOfWork
{
    private IDbTransaction? _transaction;
    public IWeekRepository WeekRepository { get; }
    public IUserRepository UserRepository { get; }
    public ISchoolRepository SchoolRepository { get; }
    public IUserWeeklyActivityRepository UserWeeklyActivityRepository { get; }
    public IQuestionRepository QuestionRepository { get; }
    public IUserEnrollmentRepository UserEnrollmentRepository { get; }
    public IQuestionAttemptRepository QuestionAttemptRepository { get; }
    public IAttemptResultRepository AttemptResultRepository { get; }
    private bool _disposed;

    public UnitOfWork(IDbConnection connection,
        IWeekRepository weekRepository,
        IUserRepository userRepository,
        ISchoolRepository schoolRepository,
        IUserWeeklyActivityRepository userWeeklyActivityRepository,
        IQuestionRepository questionRepository,
        IUserEnrollmentRepository userEnrollmentRepository,
        IQuestionAttemptRepository questionAttemptRepository,
        IAttemptResultRepository attemptResultRepository
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
        QuestionAttemptRepository = questionAttemptRepository;
        AttemptResultRepository = attemptResultRepository;
    }

    public void Commit()
    {
        try
        {
            _transaction?.Commit();
        }
        catch
        {
            _transaction?.Rollback();
            throw;
        }
        finally
        {
            if (_transaction != null)
            {
                _transaction.Dispose();
                _transaction = null;
            }
        }
    }

    public void Rollback()
    {
        try
        {
            _transaction?.Rollback();
        }
        finally
        {
            if (_transaction != null)
            {
                _transaction.Dispose();
                _transaction = null;
            }
        }
    }

    private void dispose(bool disposing)
    {
        if (_disposed) return;
        if (disposing)
        {
            if (_transaction != null)
            {
                _transaction.Dispose();
                _transaction = null;
            }
        }

        _disposed = true;
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