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

    public abstract void Execute();
}
