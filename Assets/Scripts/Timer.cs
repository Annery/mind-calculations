using UnityEngine;

public sealed class Timer
{
    public float TimeToEnd { get; private set; }
    public bool IsPaused { get; set; }
    public bool IsElapsed => Mathf.Approximately(TimeToEnd, 0);

    public Timer(float duration)
    {
        TimeToEnd = duration;
    }

    public void Update()
    {
        if (IsElapsed || IsPaused)
        {
            return;
        }
        TimeToEnd = Mathf.Clamp(TimeToEnd - Time.deltaTime, 0, int.MaxValue);
    }
}