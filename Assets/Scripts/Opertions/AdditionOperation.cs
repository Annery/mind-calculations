public class AdditionOperation : Operation
{
    public override string Name => "+";
    public override OperationType Type => OperationType.Addition;

    protected override int Calculate(int first, int second) => first + second;

    protected override bool IsValid(int first, int second) => true;
}
