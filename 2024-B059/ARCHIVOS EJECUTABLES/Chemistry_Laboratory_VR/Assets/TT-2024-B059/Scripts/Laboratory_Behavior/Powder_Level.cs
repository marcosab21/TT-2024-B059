using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Powder_Level : MonoBehaviour
{
    [SerializeField] private float shrinkAmount = 0.0005f; // La cantidad que reducirá en tamaño
    [SerializeField] private PlayMakerFSM fsm; // Referencia al FSM de PlayMaker

    public void level()
    {
        // Reducir el tamaño del objeto
        transform.localScale -= new Vector3(shrinkAmount, shrinkAmount, shrinkAmount);
        // Mover el objeto hacia abajo en el eje Y
        transform.position -= new Vector3(0, shrinkAmount / 2, 0);
        fsm.SendEvent("Dig");

        // Verificar si la escala es menor o igual a cero
        if (transform.localScale.x <= 0 || transform.localScale.y <= 0 || transform.localScale.z <= 0)
        {
            // Desactivar el objeto
            gameObject.SetActive(false);
        }
    }
}