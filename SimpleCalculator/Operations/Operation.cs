namespace SimpleCalculator.Operations;
public abstract class Operation : IOperation
{
    public Operation(Register register, int? value, Register? source)
    {
        Register = register;
        if (value == null && source == null) throw new ArgumentNullException($"Either {nameof(value)} or {nameof(source)} needs to have a value");
        if (value != null) Value = value;
        if (source != null) RegisterSource = source;
    }
    public Register Register { get; }

    public int? Value { get; }

    public Register? RegisterSource { get; }

    /// <summary>
    /// The implemented class method that determines how to calculate the result of the operation
    /// NOTE: For overflow check to work the result of the calculation should be wrapped in checked()
    /// </summary>
    /// <param name="registerValue"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    protected abstract int Calculate(int registerValue, int value);

    public void Execute()
    {
        int value = Value.HasValue
            ? Value.Value
            : (RegisterSource?.Value ?? throw new Exception("Operation has neither value nor a source register with a value"));

        //Overflow check
        try
        {
            Register.Value = Calculate(Register.Value, value);
        }
        catch (OverflowException)
        {
            throw new Exception("Overflow!");
        }
    }

}
