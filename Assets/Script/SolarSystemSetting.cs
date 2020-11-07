using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolarSystemSetting:MonoBehaviour
{
    [SerializeField] public float speed;
    [SerializeField] public GameObject[] plants;
    [SerializeField] public float scale;


        void Awake()
    {
        foreach (GameObject plant in plants)
        {
            // 初始化行星位置
            var position = plant.GetComponent<CelestialBody>().center.transform.position;
            plant.transform.position = position + new Vector3(plant.GetComponent<CelestialBody>().distanceToCenter * scale, 0, 0);
            plant.GetComponent<CelestialBody>().CreatePoints();
        }
    }

    public void DecreaseSpeed()
    {
        if (speed  >= 0f)
        {
            speed -= 0.1f;
            if (speed < 0f)
            {
                speed = 0f;
            }
        }
        Debug.Log("速度 : " + speed +" 天 / 秒");
    }

    public float Speed => speed;

    public void IncreaseSpeed()
    {
        speed += 0.1f;
        Debug.Log("速度 : " + speed +" 天 / 秒");
    }
    
}
