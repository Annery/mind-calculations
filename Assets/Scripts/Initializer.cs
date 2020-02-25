using UnityEngine;

public class Initializer : MonoBehaviour
{
    private void Awake()
    {
#if UNITY_STANDALONE_WIN
        Screen.SetResolution(480, 960, false);
#endif
    }
}