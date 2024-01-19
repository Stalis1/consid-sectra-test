namespace SimpleCalculator;
public class InputHandler
{
    private OperationSequencer _operationSequencer = new();

    /// <summary>
    /// Returns true if user quits program, else false
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public bool HandleInput(string? input, bool printInput)
    {
        try
        {
            input = input?.ToLower().Trim() ?? "";
            if (string.IsNullOrWhiteSpace(input)) return false;
            if (input.StartsWith("quit")) return true;
            if (input == "clear")
            {
                _operationSequencer = new OperationSequencer();
                RegisterFactory.Flush();
                return false;
            }

            var (isValidPrintCommand, register) = InputParser.IsPrintCommand(input);
            if (isValidPrintCommand && register != null)
            {
                var result = _operationSequencer.ExecuteFor(register);
                Console.WriteLine($"{register.RegisterName} = {result}");
            }
            else
            {
                var operation = InputParser.ParseInput(input);

                _operationSequencer.AppendOperation(operation);
                if (printInput) Console.WriteLine(input);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        return false;
    }
}
