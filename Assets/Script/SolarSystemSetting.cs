using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolarSystemSetting:MonoBehaviour
{
    [SerializeField][Range(1f,999f)] public float speed = 1f;

    public void DecreaseSpeed()
    {
        if (speed - 0.1f > 0f)
        {
            speed -= 0.1f;
        }
    }

    public float Speed => speed;

    public void IncreaseSpeed()
    {
        if (speed > 0f)
        {
            speed += 0.1f;
        }
    }
}
