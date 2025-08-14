using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HutongGames.PlayMaker; // Importa la API de PlayMaker

public class Ignition_Detection : MonoBehaviour
{
    [SerializeField] private LayerMask layerToDetect;
    [SerializeField] private IgnitionHandler ignitionHandler; 

    private void Awake()
    {
        ignitionHandler = GetComponentInParent<IgnitionHandler>();  // Referencia al IgnitionHandler en el objeto padre
    }

    private bool available = true;

    private void OnTriggerEnter(Collider other)
    {
        if (available && (layerToDetect & (1 << other.gameObject.layer)) != 0)
        {
            if (ignitionHandler != null)
            {
                available = false;
                ignitionHandler.TriggerIgnition();
            }
        }
    }

    public void RecheckIgnitionHandler()
    {
        ignitionHandler = GetComponentInParent<IgnitionHandler>();
        if (ignitionHandler != null)
        {
            Debug.Log($"IgnitionHandler encontrado en: {ignitionHandler.gameObject.name}");
        }
        else
        {
            Debug.LogWarning("No se encontró IgnitionHandler en los padres.");
        }
    }

    private void OnTransformParentChanged()
    {
        RecheckIgnitionHandler();
    }
}
