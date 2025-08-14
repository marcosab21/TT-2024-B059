using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation_Núcleo : MonoBehaviour
{
    Vector3 initialPosition; // Posición inicial del objeto

    void FixedUpdate()
    {
        transform.Rotate(Vector3.up * 90f * Time.deltaTime);
        transform.Rotate(Vector3.forward * 90f * Time.deltaTime);
    }
}