using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class SolarSystemSetting:MonoBehaviour
{
    [SerializeField] public float speed;
    [SerializeField] public GameObject[] plants;
    [SerializeField] public float scale;
    public bool showAxialTilt;
    public Text universalSpeed;
    public Text info;


        void Awake()
    {
        foreach (GameObject plant in plants)
        {
            // 初始化行星位置
            if (plant.GetComponent<CelestialBody>().center)
            {
                var position = plant.GetComponent<CelestialBody>().center.transform.position;
                plant.transform.position = position + new Vector3(plant.GetComponent<CelestialBody>().distanceToCenter * scale, 0, 0);
            }
            plant.GetComponent<CelestialBody>().Scale = scale;
        }
    }


         void Update()
        {
            if (Input.GetKey(KeyCode.M))
            {
                if (showAxialTilt)
                {
                    showAxialTilt = false;
                }
                else
                {
                    showAxialTilt = true;
                }
            }
        }

        public bool isShow => showAxialTilt;
        

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
        UpdateSpeedText();
    }

    public float Speed => speed;

    public void IncreaseSpeed()
    {
        speed += 0.1f;
        UpdateSpeedText();
        //Debug.Log("速度 : " + speed +" 天 / 秒");
    }

    void UpdateSpeedText()
    {
        universalSpeed.text = "Speed : " + speed + " days / sec";
    }

    public void UpdatePlantInfo( string plantInfo)
    {
        info.text = plantInfo;
    }

    public void WrapPlantInfo()
    {
        info.text = "";
    }

}
