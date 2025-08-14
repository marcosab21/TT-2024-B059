using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lighter : MonoBehaviour
{
    [SerializeField] private float targetAnglePositive = 30f;  // Ángulo objetivo positivo
    [SerializeField] private float targetAngleNegative = -90f;  // Ángulo objetivo negativo
    private bool isAngleReachedPositive = false; 
    private bool isAngleReachedNegative = false;
    [SerializeField] private PlayMakerFSM fsm;

    private void FixedUpdate()
    {
        // Obtener el ángulo actual en el eje Y
        float currentAngleY = transform.localEulerAngles.y;

        // Verificar si se ha alcanzado el ángulo positivo
        if (!isAngleReachedPositive && currentAngleY >= targetAnglePositive && currentAngleY < 180)
        {
            isAngleReachedPositive = true;  // Marcar que se alcanzó el ángulo positivo
            isAngleReachedNegative = false;
            fsm.SendEvent("Opening");
        }
        else if (!isAngleReachedNegative /*&& currentAngleY <= targetAngleNegative*/
            && (currentAngleY > 360 + targetAngleNegative && currentAngleY < 360)) // Verifica el wrap-around para -30 grados
        {
            isAngleReachedNegative = true;  // Marcar que se alcanzó el ángulo negativo
            isAngleReachedPositive = false;
            fsm.SendEvent("Closing");
        }
    }
}
