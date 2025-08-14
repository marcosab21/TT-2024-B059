using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HutongGames.PlayMaker; // Importa la API de PlayMaker

public class Collision_Detect : MonoBehaviour
{
    [SerializeField] private PlayMakerFSM fsm; // Referencia al FSM de PlayMaker
    private string tag = "Cristal";

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == tag)
        {
            fsm.SendEvent("Clink");
        }
    }
}
