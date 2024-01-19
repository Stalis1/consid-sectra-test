using SimpleCalculator;
using SimpleCalculator.Operations;

var inputHandler = new InputHandler();
Console.WriteLine("Simple calculator");
Console.WriteLine("Instructions: Use one the following three commands");
Console.WriteLine(" <register> <operation> <value> - Applies the <operation> using <register> and <value> and stores the result in <register>. <value> can be either a number or a register. Examples: a add 2, a subtract b");
Console.WriteLine(" print <register> - Evaluates the value of <register> and prints it");
Console.WriteLine(" quit");
Console.WriteLine("Input commands either as a file by launching app with command line argument of file path or by manual input below.");
Console.WriteLine("");
Console.WriteLine($"Implemented operations are: {AddOperation.Command}, {SubtractOperation.Command}, {MultiplyOperation.Command}");

var filePath = args?.Length == 0 ? null : args?[0];

if (filePath != null)
{
    Console.WriteLine($"Reading input from {filePath}");
    var inputLines = File.ReadAllLines(filePath);
    foreach (var line in inputLines)
    {
        var breakLoop = inputHandler.HandleInput(line, true);
        if (breakLoop) return;
    }
}
else
{
    Console.WriteLine($"Reading input from console");
    bool breakLoop;
    do
    {
        var input = Console.ReadLine();
        breakLoop = inputHandler.HandleInput(input, false);
    }
    while (!breakLoop);
}
