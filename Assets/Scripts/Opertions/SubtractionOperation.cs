public class SubtractionOperation : Operation
{
    public override string Name => "-";
    public override OperationType Type => OperationType.Subtraction;

    protected override int Calculate(int first, int second) => first - second;

    protected override bool IsValid(int first, int second) => first > second;
}