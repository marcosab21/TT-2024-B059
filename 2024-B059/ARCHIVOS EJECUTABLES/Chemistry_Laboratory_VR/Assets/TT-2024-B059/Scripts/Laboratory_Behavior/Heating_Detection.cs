using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heating_Detection : MonoBehaviour
{
    [SerializeField] private LayerMask layerMask; // Define qué capas serán detectadas por el Raycast
    [SerializeField] private float rayLength = 0.5f; // Longitud máxima del Raycast
    [SerializeField] private IgnitionHandler ignitionHandler;
    private bool _ignition = false;
    private GameObject lastDetected;
    private TemperatureController temperatureController;

    void FixedUpdate()
    {
        // Actualizar estado de ignición
        _ignition = ignitionHandler.IsIgnited();

        // Detectar objeto con Raycast
        if (_ignition && Physics.Raycast(transform.position, Vector3.up, out RaycastHit hit, rayLength, layerMask))
        {
            GameObject hitObject = hit.collider.gameObject;

            // Si detectamos un nuevo objeto, actualizar referencias
            if (lastDetected != hitObject)
            {
                lastDetected = hitObject;
                temperatureController = lastDetected.GetComponent<TemperatureController>();
            }

            // Aplicar calentamiento al objeto detectado
            if (temperatureController != null)
            {
                temperatureController.SetHeating(true);
            }
        }
        else
        {
            // Si no hay ignición o no se detecta objeto, enfriar el último detectado
            if (temperatureController != null)
            {
                temperatureController.SetHeating(false);
            }

            ResetDetection();
        }
    }

   private void ResetDetection()
    {
        lastDetected = null;
        temperatureController = null;
    }
}
