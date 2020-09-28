public abstract class Operation
{
    public abstract string Name { get; }

    public abstract int Calculate(int first, int second);
    public abstract bool IsValid(int first, int second);
}
