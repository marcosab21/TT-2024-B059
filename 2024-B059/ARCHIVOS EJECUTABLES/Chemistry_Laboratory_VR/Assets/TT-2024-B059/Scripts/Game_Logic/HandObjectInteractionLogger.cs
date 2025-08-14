using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Interaction;
using Oculus.Interaction.HandGrab;

public class HandObjectInteractionLogger : MonoBehaviour
{
    private HandGrabInteractable handGrabInteractable;
    [SerializeField] private GameObject targetObject;
    private Collider[] colliders;

    private void Awake()
    {
        // Obtén el componente HandGrabInteractable
        handGrabInteractable = GetComponent<HandGrabInteractable>();

        if (handGrabInteractable == null)
        {
            Debug.LogError($"HandGrabInteractable no encontrado en {gameObject.name}. Asegúrate de que este componente está presente.");
        }

        if (targetObject == null)
        {
            Debug.LogError($"No se ha asignado el objeto objetivo en {gameObject.name}. Asegúrate de configurarlo en el inspector.");
        }

        colliders = GetComponents<Collider>();
    }

    private void OnEnable()
    {
        if (handGrabInteractable != null)
        {
            // Subscribirse a los eventos de agarre y liberación
            handGrabInteractable.WhenPointerEventRaised += HandleGrabEvent;
        }
    }

    private void OnDisable()
    {
        if (handGrabInteractable != null)
        {
            // Desuscribirse de los eventos para evitar errores
            handGrabInteractable.WhenPointerEventRaised -= HandleGrabEvent;
        }
    }

    private void HandleGrabEvent(PointerEvent pointerEvent)
    {
        switch (pointerEvent.Type)
        {
            case PointerEventType.Select:
                targetObject.SetActive(false);
                break;

            case PointerEventType.Unselect:
                targetObject.SetActive(true);
                break;
        }
    }

    private void FixedUpdate()
    {
        // Revisar si todos los colliders del objeto actual están deshabilitados
        if (AreAllCollidersDisabled())
        {
            targetObject.SetActive(false);
        }
    }

    private bool AreAllCollidersDisabled()
    {
        foreach (var col in colliders)
        {
            if (col.enabled) return false; // Si algún collider está activo, no desactives el objeto
        }
        return true; // Todos los colliders están deshabilitados
    }
}
