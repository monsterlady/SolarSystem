using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Zoom out
        if (Input.GetAxis("Mouse ScrollWheel") <0 )
        {
            GameObject.Find("SolarSystem").GetComponent<SolarSystemSetting>().DecreaseSpeed();
        }
        //Zoom in
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            GameObject.Find("SolarSystem").GetComponent<SolarSystemSetting>().IncreaseSpeed();
        }

    }
    
    
}
