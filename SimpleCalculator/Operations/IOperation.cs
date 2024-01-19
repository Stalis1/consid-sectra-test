
namespace SimpleCalculator.Operations;
public interface IOperation
{
    Register Register { get; }
    /// <summary>
    /// A numeric value to mutate Registers value
    /// </summary>
    int? Value { get; }
    /// <summary>
    /// A Register from which value will be used to mutate Registers value
    /// </summary>
    Register? RegisterSource {  get; }
    void Execute();
}
