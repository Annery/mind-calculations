public class DivisionOperation : Operation
{
    public override string Name => "/";
    public override OperationType Type => OperationType.Division;

    protected override int Calculate(int first, int second) => first / second;

    protected override bool IsValid(int first, int second) => first % second == 0 && first != second && second != 1;
}