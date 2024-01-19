using SimpleCalculator.Operations;

namespace SimpleCalculator;
public static class InputParser
{
    const string _print = "print";

    /// <summary>
    /// Determines if an input is a valid print command
    /// </summary>
    /// <param name="input"></param>
    /// <returns>A tuple with bool determining if it is a valid print command and the register to print for</returns>
    public static (bool isValidPrintCommand, Register? register) IsPrintCommand(string input)
    {
        var split = input.Split(' ');
        var isValidPrintCommand = split.Length == 2
            && split[0] == _print
            && split[1] != null
            && IsValidRegisterName(split[1]);
        var register = isValidPrintCommand ? RegisterFactory.GetRegister(split[1]) : null;

        return (isValidPrintCommand, register);
    }

    public static IOperation ParseInput(string input)
    {
        var split = input.Split(' ');

        if (split.Length != 3) throw new ArgumentException("Unexpected input");

        if (!IsValidRegisterName(split[0])) throw new ArgumentException("The Regisiter name is not valid");

        var register = RegisterFactory.GetRegister(split[0]);
        var command = split[1];
        var source = split[2];
        var isValue = long.TryParse(source, out long value);

        if (isValue)
        {
            //Currently only allowing int as max value
            if (Math.Abs(value) > int.MaxValue) throw new Exception("Value is too large");
            return CreateOperation(command, register, (int)value);
        }
        else if (IsValidRegisterName(source))
        {
            return CreateOperation(command, register, source: RegisterFactory.GetRegister(source));
        }
        throw new Exception("Unable to parse input");
    }

    private static IOperation CreateOperation(string command, Register register, int? value = null, Register? source = null)
    {
        return command switch
        {
            AddOperation.Command => new AddOperation(register, value, source),
            SubtractOperation.Command => new SubtractOperation(register, value, source),
            MultiplyOperation.Command => new MultiplyOperation(register, value, source),
            _ => throw new ArgumentException($"Unknown command \"{command}\""),
        };
    }

    private static bool IsValidRegisterName(string registerName) => registerName.All(char.IsLetterOrDigit);
}
