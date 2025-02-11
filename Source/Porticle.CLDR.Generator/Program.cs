


using Porticle.CLDR.Generator;

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