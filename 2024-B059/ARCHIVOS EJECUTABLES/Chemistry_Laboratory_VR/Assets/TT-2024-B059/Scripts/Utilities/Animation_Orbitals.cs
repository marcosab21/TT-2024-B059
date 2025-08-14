using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Animation_Orbital : MonoBehaviour
{
    bool direction = true;
    float startTime;
    float axisY;
    float axisZ;
    float targetTime = 2f; // Tiempo objetivo en segundos
    public bool flag;
    void Start()
    {
        startTime = Time.fixedTime; // Momento en que comienza la acción
        axisY = (flag ? 180 : -180) * Time.deltaTime;
        axisZ = (flag ? 90 : -90) * Time.deltaTime;
    }

    void FixedUpdate()
    {
        transform.Rotate(Vector3.up * axisY);
        transform.Rotate(Vector3.forward * axisZ);

        float axisX = (direction ? 22.5f : -22.5f) * Time.deltaTime;
        transform.Rotate(Vector3.right * axisX);

        if (Time.fixedTime - startTime >= targetTime)
        {
            direction = !direction;
            startTime = Time.fixedTime;
        }
    }

}