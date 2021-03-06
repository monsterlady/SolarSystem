﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantInfo : MonoBehaviour
{
    // [Header("Name")] private String plantName;
    // [Header("Rotation Period (days)")] private float rp;
    // [Header("Rotation direction")] private bool isClockwise;
    // [Header("Center")] private String center;
    // [Header("Revolution Period (days)")] private float rep;
    // [Header("Revolution direction")] private bool isRClockwise;
    // [Header("Axial tilt (°)")] private float obliquity;
    private float doubleClickStart;

    void OnMouseUp()
    {
        if ((Time.time - doubleClickStart) < 0.3f)
        {
            OnDoubleClick();
            doubleClickStart = -1;
        }
        else
        {
            doubleClickStart = Time.time;
        }
    }

    void OnDoubleClick()
    {
        if (GameObject.FindWithTag("MainCamera").GetComponent<FreeCam>().focus == null)
        {
            GameObject.FindWithTag("MainCamera").GetComponent<FreeCam>().focus = gameObject;
        }
        else if (GameObject.FindWithTag("MainCamera").GetComponent<FreeCam>().focus == gameObject)
        {
            GameObject.FindWithTag("MainCamera").GetComponent<FreeCam>().focus = null;
        }
    }

    public override string ToString()
    {
        CelestialBody rt = transform.root.gameObject.GetComponent<CelestialBody>();
        return "Name: " + rt.tag + "\n"
               + (rt.center ? "Center: " + rt.center.tag + "\n" : "")
               + "Rotation Period: " + rt.rotationPeriod + " days" + "\n"
               + "Rotation Direction: " + (rt.sClockwise ? "Clockwise" : "Anticlockwise") + "\n"
               + (rt.center
                   ? "Revolution Period: " + rt.periodRevolution + " days" + "\n"
                     + "Revolution Direction: " + (rt.rClockwise ? "Clockwise" : "Anticlockwise") + "\n"
                   : "")
               + "Axial tilt: " + rt.obliquity + "°";
    }
}