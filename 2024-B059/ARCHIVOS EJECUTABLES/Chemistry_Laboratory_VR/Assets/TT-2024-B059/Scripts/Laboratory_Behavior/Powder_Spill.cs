using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HutongGames.PlayMaker; // Importa la API de PlayMaker

public class Powder_Spill : MonoBehaviour
{
    private float xAngle, zAngle; // Ángulos de inclinación en los ejes X y Z
    private float spillAngle = 45.0f;
    private bool available;
    [SerializeField] private PlayMakerFSM fsm;
    void OnEnable()
    {
        //powders_particles = gameObject.GetComponent<Powders_Particles>();
        available = true;
    }

    void FixedUpdate()
    {
        if(!available)
        {
            spilling(); // Llamar al método de derrame
        }
    }

    void spilling()
    {
        // Obtener los ángulos X y Z, ajustándolos al rango de -180 a 180
        xAngle = Mathf.DeltaAngle(0f, gameObject.transform.eulerAngles.x); 
        zAngle = Mathf.DeltaAngle(0f, gameObject.transform.eulerAngles.z);

        if(IsSpilling(spillAngle))
        {
            //powders_particles._particles();
            fsm.SendEvent("Spilling");
            available = true;
            //powder.SetActive(false);
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

    public bool _available
    {
        get { return available; }
        set { available = value; }
    }
}