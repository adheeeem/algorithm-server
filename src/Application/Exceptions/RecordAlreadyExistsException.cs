﻿namespace Application.Exceptions;

public class RecordAlreadyExistsException : Exception
{
	public RecordAlreadyExistsException(string message) : base(message)
	{

	}
}
