namespace Porticle.CLDR.Generator;

public class ParsingException : Exception
{
    public ParsingException()
    {
    }

    public ParsingException(string message) : base(message)
    {
    }

    public ParsingException(string message, Exception inner) : base(message, inner)
    {
    }
}