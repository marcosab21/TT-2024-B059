using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HutongGames.PlayMaker; // Importa la API de PlayMaker

public class Spill_Dropper_Detection : MonoBehaviour
{
    [SerializeField] private LayerMask layerMask; // Define qué capas serán detectadas por el Raycast
    [SerializeField] private float rayLength = 0.5f; // Longitud máxima del Raycast
    [SerializeField] private PlayMakerFSM fsm; // Referencia al FSM de PlayMaker
    [SerializeField] private GameObject liquid;
    [SerializeField] private bool change_color_flag;
    [SerializeField] private Fill_detection Fill;
    private Material material_liquid;
    private GameObject lastDetected;
    private ColorChangeController change_color;
    [SerializeField] private float fillRate = 0.0f;

    private void OnEnable()
    {
        material_liquid = liquid.GetComponent<Renderer>().material;
    }

    void FixedUpdate()
    {
        RaycastHit hit;

        if(Physics.Raycast(transform.position, Vector3.down, out hit, rayLength, layerMask))
        {
            lastDetected = hit.collider.gameObject;
            if(change_color_flag)
            {
                change_color = lastDetected.GetComponent<ColorChangeController>();
                change_color.StartColorChange(material_liquid);
            }
            if(Fill != null)
            {
                Fill.detection(fillRate);
            }
            fsm.SendEvent("Activate_Sound");
        }
        else
        {
            if(lastDetected != null && change_color != null)
            {
                change_color.StopColorChange();
            }
            fsm.SendEvent("Deactivate_Sound");
            lastDetected = null;
            change_color = null;
        }
    }
}