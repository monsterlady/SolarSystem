using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CelestialBody : MonoBehaviour
{
    [SerializeField] public GameObject center;

    [SerializeField] public float distanceToCenter;
    
    [Tooltip("The time in days, that the planet needs for a full rotation (eg. 1 day for the Earth")]
    [SerializeField] public float rotationPeriod;

    [Tooltip("转轴倾角")] [SerializeField] public float obliquity;

    private Vector3 _rotationAxis;
    private const float AROUND = 360f;
    private float _speed;
    private float _angularPers;
    // Start is called before the first frame update
    void Start()
    {
        // draw a 5-unit white line from the origin for 2.5 seconds
        _angularPers = AROUND / rotationPeriod;
       Quaternion dot = Quaternion.Euler(0,0,obliquity);
       _rotationAxis = dot * Vector3.up;
       _speed = gameObject.GetComponentInParent<SolarSystemSetting>().Speed;
    }

    // Update is called once per frame
    void Update()
    {
        _speed = gameObject.GetComponentInParent<SolarSystemSetting>().Speed;
    }
    
    void FixedUpdate()
    {
        transform.RotateAround(transform.position,_rotationAxis,_angularPers * Time.deltaTime * _speed );
    }
}
