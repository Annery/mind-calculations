public class AdditionOperation : Operation
{
    public override string Name => "+";

    public override int Calculate(int first, int second) => first + second;

    public override bool IsValid(int first, int second) => true;
}
