namespace SimpleCalculator.Operations;
public class MultiplyOperation(Register register, int? value, Register? source) : Operation(register, value, source)
{
    public const string Command = "multiply";

    protected override int Calculate(int registerValue, int value) => checked(registerValue * value);
}
