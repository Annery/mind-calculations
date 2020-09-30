public class AdditionOperation : Operation
{
    public override string Name => "+";

    protected override int Calculate(int first, int second) => first + second;

    protected override bool IsValid(int first, int second) => true;
}
