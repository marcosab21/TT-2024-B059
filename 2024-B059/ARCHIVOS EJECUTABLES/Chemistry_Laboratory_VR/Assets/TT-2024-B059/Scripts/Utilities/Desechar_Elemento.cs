using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Desechar_Elemento : MonoBehaviour
{
    [SerializeField] private string targetTag = "Isotopo"; // Tag del objeto que debe ser destruido al colisionar
    [SerializeField] private GameObject fsmObject; // Objeto que contiene el FSM
    [SerializeField] private string fsmName; // Nombre del FSM específico en el objeto
    private PlayMakerFSM fsm;

    private void Awake()
    {
        if (fsmObject != null)
        {
            // Obtener el FSM en el objeto asignado
            var fsmArray = fsmObject.GetComponents<PlayMakerFSM>();
            fsm = System.Array.Find(fsmArray, f => f.FsmName == fsmName);

            if (fsm == null)
            {
                Debug.LogWarning($"FSM con el nombre '{fsmName}' no encontrado en el objeto {fsmObject.name}.");
            }
        }
        else
        {
            Debug.LogWarning("Objeto FSM no asignado.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Verificar si el objeto que colisiona tiene el tag especificado
        if (other.gameObject.CompareTag(targetTag))
        {
            // Destruir el objeto que colisionó
            Destroy(other.gameObject);
            fsm.SendEvent("goSeleccion");
        }
    }
}
