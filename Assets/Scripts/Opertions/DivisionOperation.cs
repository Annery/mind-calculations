﻿public class DivisionOperation : Operation
{
    public override string Name => "/";

    public override int Calculate(int first, int second) => first / second;

    public override bool IsValid(int first, int second) => first % second == 0 && first != second && second != 1;
}