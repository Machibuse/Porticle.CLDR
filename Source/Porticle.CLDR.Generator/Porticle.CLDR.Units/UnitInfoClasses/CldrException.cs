using System;

public class CldrException : Exception
{
    public CldrException()
    {
    }

    public CldrException(string message) : base(message)
    {
    }

    public CldrException(string message, Exception inner) : base(message, inner)
    {
    }
}