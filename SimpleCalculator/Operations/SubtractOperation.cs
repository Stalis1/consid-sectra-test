namespace SimpleCalculator.Operations;
public class SubtractOperation(Register register, int? value, Register? source) : Operation(register, value, source)
{
    public const string Command = "subtract";

    protected override int Calculate(int registerValue, int value) => checked(registerValue - value);
}
