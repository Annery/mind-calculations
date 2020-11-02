using System.Collections;
using UnityEngine;

public sealed class CoroutineManager : MonoBehaviour
{
    private static CoroutineManager _instance;
    private static CoroutineManager Instance =>
        _instance != null 
            ? _instance 
            : _instance = new GameObject("[CoroutineManager]")
                .AddComponent<CoroutineManager>();

    public static void Run(IEnumerator routine) => 
        Instance.StartCoroutine(routine);

    public static void Stop(IEnumerator routine) =>
        Instance.StopCoroutine(routine);
}