public class MultiplicationOperation : Operation
{
    public override string Name => "*";
    public override OperationType Type => OperationType.Multiplication;

    protected override int Calculate(int first, int second) => first * second;

    protected override bool IsValid(int first, int second) => first != 1 && second != 1;
}
