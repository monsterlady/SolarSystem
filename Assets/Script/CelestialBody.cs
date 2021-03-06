﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vectrosity;

public class CelestialBody : MonoBehaviour
{
    [SerializeField] public GameObject center;

    [SerializeField] public float distanceToCenter;

    [Header("Revolution direction")] [Tooltip("公转方向")] [SerializeField]
    public bool rClockwise;

    [Header("Self-Rotation direction")] [Tooltip("自转方向")] [SerializeField]
    public bool sClockwise;

    [Tooltip("The time in days, that the planet needs for a full rotation (eg. 1 day for the Earth")] [SerializeField]
    public float rotationPeriod;

    [Tooltip("period revolution")] [SerializeField]
    public float periodRevolution;

    [Tooltip("转轴倾角")] [SerializeField] public float obliquity;

    [Tooltip("Defines the color of the rendered circle for the trajectory.")]
    public Color lineColor = Color.red;

    [Tooltip("Defines the line width of the rendered circle for the trajectory.")]
    public float lineWidth = 0.05f;

    [Tooltip("System controller")] public GameObject controller;

    [Tooltip("Large plant comparing to Earth")]public bool isLarge;

    private Vector3 m_RotationAxis;
    private const float AROUND = 360f;
    private float m_Speed;
    private float m_AngularPers;
    private Vector3 m_DesiredPosition;
    private float m_Revolutiondegree;
    private int segments = 128;
    private LineRenderer line;
    public Material m_Material;
    private Vector3 desiredPosition;
    private float m_Scale;
    private bool showingAT = false;
    
    // Start is called before the first frame update
    void Start()
    {
        // draw a 5-unit white line from the origin for 2.5 seconds
        m_AngularPers = AROUND / rotationPeriod;

        Quaternion dot = Quaternion.Euler(0, 0, obliquity);
        m_RotationAxis = dot * (sClockwise ? Vector3.up : Vector3.down);
        m_Speed = controller.GetComponentInParent<SolarSystemSetting>().Speed;
        if (center)
        {
            m_Revolutiondegree = AROUND / periodRevolution;
            desiredPosition = transform.position - center.transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //
        m_Speed = controller.GetComponentInParent<SolarSystemSetting>().Speed;
        showingAT = controller.GetComponentInParent<SolarSystemSetting>().isShow;
        //
        SelfRotate();
        if (showingAT)
        {
            DrawAxialTilt();
        }
    }

    void SelfRotate()
    {
        transform.RotateAround(transform.position, m_RotationAxis, m_AngularPers * Time.deltaTime * m_Speed);
    }
    
    void LateUpdate()
    {
         if (center)
        {
            CreatePoints();
            OrbitRotate();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    void OrbitRotate()
    {
        // Adjust the radius to the center of gravity if needed
        // Set the final position for this update
        transform.position = center.transform.position +  desiredPosition;
        transform.RotateAround(center.transform.position, rClockwise ? Vector3.up : Vector3.down,
            m_Speed * Time.deltaTime * m_Revolutiondegree);
        desiredPosition = transform.position - center.transform.position;
    }
    
    /// <summary>
    /// Draws the axial tilt
    /// </summary>
    void DrawAxialTilt()
    {
        VectorLine.SetRay3D(Color.green, Time.deltaTime ,transform.position, m_RotationAxis  * (isLarge ? (Scale / 2) : 1));
        VectorLine.SetRay3D(Color.green, Time.deltaTime ,transform.position, -m_RotationAxis * (isLarge ? (Scale / 2) : 1));
    }

    public void CreatePoints()
    {
        if (center)
        {
            // Setup our line renderer
            line = gameObject.GetComponent<LineRenderer>();
            // create a new material for it
            line.material = m_Material;
            line.startColor = lineColor;
            line.endColor = lineColor;
            // define its size
            line.startWidth = lineWidth;
            line.endWidth = lineWidth;
            line.positionCount = (segments + 1);
            // and make sure its in world space
            line.useWorldSpace = true;
            // Loops through the amount of segments, our circle should have and set the line position for each segment
            for (int i = 0; i < (segments + 1); i++)
            {
                var rad = Mathf.Deg2Rad * (i * 360f / segments);
                var linePos = new Vector3(Mathf.Sin(rad) * distanceToCenter *  Scale, 0, Mathf.Cos(rad) * distanceToCenter * Scale) + center.transform.position;
                line.SetPosition(i,linePos);
            }
        }
    }

    public float Scale
    {
        get => m_Scale;
        set => m_Scale = value;
    }
    
    
}