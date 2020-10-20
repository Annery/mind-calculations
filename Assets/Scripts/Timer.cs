using UnityEngine;

public sealed class Timer
{
    public float TimeToEnd { get; private set; }
    public bool IsPaused { get; set; }

    public Timer(float duration)
    {
        TimeToEnd = duration;
    }

    public void Update()
    {
        if (IsPaused)
        {
            return;
        }
        TimeToEnd -= Time.deltaTime;
    }
}