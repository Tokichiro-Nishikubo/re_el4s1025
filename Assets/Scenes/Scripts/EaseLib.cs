using UnityEngine;

public class EaseLib
{
    public static float EaseOutQuart(float x)
    {
        return 1f - Mathf.Pow(1f - x, 4f);
    }
}
