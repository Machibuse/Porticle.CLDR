using System.Diagnostics.CodeAnalysis;

namespace Porticle.CLDR.Generator;

internal static class Program
{
    [RequiresDynamicCode("Calls Porticle.CLDR.Generator.Parser.Run()")]
    [RequiresUnreferencedCode("Calls Porticle.CLDR.Generator.Parser.Run()")]
    public static void Main(string[] args)
    {
        if (args.Length != 1)
        {
            Console.Error.WriteLine("Expected repository root path as parameter");
            Environment.Exit(1);
        }

        if (!Directory.Exists(args[0]))
        {
            Console.Error.WriteLine($"Repository root path '{args[0]} does not exist");
            Environment.Exit(2);
        }


        new Parser(args[0]).Run();
    }
}