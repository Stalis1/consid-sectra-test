using SimpleCalculator.Operations;

namespace SimpleCalculator;
public class OperationSequencer
{
    private List<IOperation> Operations { get; set; } = [];

    public void AppendOperation(IOperation operation) => Operations.Add(operation);

    /// <summary>
    /// Executes operation sequence for a given register
    /// </summary>
    /// <param name="register"></param>
    /// <returns>The value of the register after every operation has been executed</returns>
    public int ExecuteFor(Register register)
    {
        ExecuteForRecursive(register, []);

        return register.Value;
    }

    /// <summary>
    /// Recursively executes all operations. Order of execution is based on given register.
    /// DependencyRegisters is used to keep track of what registers and operations are allowed to be executed for the given recursion loop.
    /// Checks for circular dependencies
    /// </summary>
    /// <param name="register"></param>
    /// <param name="dependencyRegisters"></param>
    /// <exception cref="Exception"></exception>
    private void ExecuteForRecursive(Register register, List<Register> dependencyRegisters)
    {
        var operationsForRegister = Operations.Where(x => x.Register == register).ToList();

        //All operations where register does not depend on itself, e.g. a add a
        foreach (var operation in operationsForRegister.Where(x => x.RegisterSource != x.Register))
        {
            if (operation.RegisterSource != null)
            {
                if (dependencyRegisters != null)
                {
                    if (dependencyRegisters.Contains(operation.RegisterSource)) throw new Exception("Circular dependency detected!");
                    dependencyRegisters.Add(register);
                }
                else
                {
                    dependencyRegisters = [register];
                }
                ExecuteForRecursive(operation.RegisterSource, dependencyRegisters);
            }
            ExecuteOperation(operation);
        }
        //All operations where register depends on itself, e.g. a add a
        //Since all other operations for this register have been executed it is safe to do this without recursion
        foreach(var operation in operationsForRegister.Where(x => x.Register == x.RegisterSource))
        {
            ExecuteOperation(operation);
        }
    }
    private void ExecuteOperation(IOperation operation)
    {
        operation.Execute();
        Operations.Remove(operation);
    }
}
