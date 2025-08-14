using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation_trash : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 30f; // Grados por segundo
    [SerializeField] private float floatAmplitude  = 0.0001f; // Distancia vertical del movimiento
    [SerializeField] private float floatFrequency  = 1f; // Velocidad del movimiento vertical

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
        float newY = startPos.y + Mathf.Sin(Time.time * Mathf.PI * floatFrequency) * floatAmplitude;

        // Aplicar la nueva posición
        transform.position = new Vector3(startPos.x, newY, startPos.z);
    }
}
