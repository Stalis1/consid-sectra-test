namespace SimpleCalculator.Operations;
public class AddOperation(Register register, int? value, Register? source) : Operation(register, value, source)
{
    public const string Command = "add";

    protected override int Calculate(int registerValue, int value) => checked(registerValue + value);
}
