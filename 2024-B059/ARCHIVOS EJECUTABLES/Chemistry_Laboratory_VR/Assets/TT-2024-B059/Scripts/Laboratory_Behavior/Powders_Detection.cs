using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HutongGames.PlayMaker; // Importa la API de PlayMaker

public class Powders_Detection : MonoBehaviour
{
    [SerializeField] private LayerMask layerToDetect;
    [SerializeField] private GameObject powder;
    [SerializeField] private PlayMakerFSM fsm; // Referencia al FSM de PlayMaker
    private Powder_Spill powder_spill;
    private string powder_tag = "Powders";

    void Start()
    {
        powder_spill = gameObject.GetComponent<Powder_Spill>();
    }

     // Este método se llamará cuando un objeto con collider entre en contacto con el trigger
    private void OnTriggerEnter(Collider other)
    {
        // Verifica si el objeto que colisionó está en el Layer especificado
        if (((layerToDetect & (1 << other.gameObject.layer)) != 0) && other.gameObject.tag == powder_tag && powder_spill._available)
        {
            powder_spill._available = false;
            fsm.SendEvent("Taking");
            
            // Obtiene el componente Powder_Level directamente del objeto colisionado
            Powder_Level powder_level = other.gameObject.GetComponent<Powder_Level>();
            if (powder_level != null)
            {
                powder_level.level();
            }
        }
    }
}
