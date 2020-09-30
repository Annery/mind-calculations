using System;
using Random = UnityEngine.Random;

public abstract class Operation
{
    public const int MinDigitValue = 1;
    public const int MaxDigitValue = 3;
    public const int MaxAnswerLength = MaxDigitValue * MaxDigitValue;

    public int DigitCapacity { get; set; } = MinDigitValue;
    public int Number1 { get; private set; }
    public int Number2 { get; private set; }
    public int Result { get; private set; }
    public abstract string Name { get; }

    protected abstract int Calculate(int first, int second);
    protected abstract bool IsValid(int first, int second);

    public void GenerateExpression()
    {
        do
        {
            Number1 = Random.Range(1, (int) Math.Pow(10, DigitCapacity));
            Number2 = Random.Range(1, (int) Math.Pow(10, DigitCapacity));
            Result = Calculate(Number1, Number2);
        } 
        while (!IsValid(Number1, Number2));
    }
}
