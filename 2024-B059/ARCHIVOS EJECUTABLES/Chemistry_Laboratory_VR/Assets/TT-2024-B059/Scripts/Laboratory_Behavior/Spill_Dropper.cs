using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HutongGames.PlayMaker; // Importa la API de PlayMaker

public class Spill_Dropper : MonoBehaviour
{
    [SerializeField] private PlayMakerFSM fsm;
    [SerializeField] private GameObject container;
    [SerializeField] private float spillAngle; // Ángulo en el que comienza el derrame
    [SerializeField] private bool canFill = false;
    [SerializeField] private float fillRate = 9.0f;
    [SerializeField] private Fill_detection fill;
    //private Spill_Dropper_Detection sound;
    //private bool Spill = false;
    private float xAngle, zAngle; // Ángulos de inclinación en los ejes X y Z

    void FixedUpdate()
    {
        xAngle = Mathf.DeltaAngle(0f, container.transform.eulerAngles.x); 
        zAngle = Mathf.DeltaAngle(0f, container.transform.eulerAngles.z);
        if (/*!Spill &&*/ IsSpilling(spillAngle))
        {
            //Spill = !Spill;
            fsm.SendEvent("Activate_Particles");
            if(canFill)
                Debug.Log("derramando");
                fill.detection(fillRate);
        }
        else //if(Spill && !IsSpilling(spillAngle))
        {
            //Spill = !Spill;
            fsm.SendEvent("Deactivate_Particles");
        }
    }

    private bool IsSpilling(float spillAngle)
    {
        // Calcula la diferencia compuesta de los ángulos X y Z con el ángulo de derrame
        float compositeAngle = CalculateCompositeAngle();
        return compositeAngle > spillAngle || compositeAngle > 360f - spillAngle;
    }

    private float CalculateCompositeAngle()
    {
        return Mathf.Sqrt(Mathf.Pow(xAngle, 2f) + Mathf.Pow(zAngle, 2f));
    }
}