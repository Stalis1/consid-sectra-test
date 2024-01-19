namespace SimpleCalculator;
public static class RegisterFactory
{
    private static readonly List<Register> _registers = [];

    public static Register GetRegister(string registerName)
    {
        var register = _registers.Find(x => x.RegisterName == registerName);

        if (register == null)
        {
            register = new Register() { RegisterName = registerName };
            _registers.Add(register);
        }

        return register;
    }
    public static void Flush() => _registers.Clear();
}
