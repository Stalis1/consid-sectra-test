namespace SimpleCalculator.Operations;
public class AddOperation(Register register, int? value, Register? source) : Operation(register, value, source)
{
    public const string Command = "add";

    public override void Execute()
    {
        if (Value.HasValue)
        {
            Register.Value += Value.Value;
        }
        else if (RegisterSource != null)
        {
            Register.Value += RegisterSource.Value;
        }
    }
}
