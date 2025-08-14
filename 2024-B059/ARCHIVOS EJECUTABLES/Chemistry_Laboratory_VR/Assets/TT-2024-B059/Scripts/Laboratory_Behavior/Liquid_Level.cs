using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HutongGames.PlayMaker; // Importa la API de PlayMaker

public class Liquid_Level : MonoBehaviour
{
    // Referencia al Renderer del GameObject para manipular propiedades del shader
    [SerializeField] private Renderer objectRenderer;
    [SerializeField] private float start_level;
    [SerializeField] private float max_angle;
    [SerializeField] private float min_level;
    [SerializeField] private float max_level;
    [SerializeField] private float m_level;
    [SerializeField] private GameObject container;
    [SerializeField] private GameObject particles; // Referencia al sistema de partículas
    [SerializeField] private PlayMakerFSM fsm;
    private float level; // Nivel actual del agua
    private float new_level; // Nuevo nivel que se ajusta en el shader
    private float spillAngle; // Ángulo en el que comienza el derrame
    private float spillSpeed = 0.01f; // Velocidad base de vaciado por grado de diferencia
    private float spillRate; // Tasa de derrame
    private float xAngle, zAngle; // Ángulos de inclinación en los ejes X y Z
    private Fill_detection fill_detection; // Referencia al script de detección de llenado

    // Se llama cuando el objeto es activado
    private void OnEnable() {
        // Obtener el Renderer para controlar el nivel del agua en el shader
        objectRenderer = GetComponent<Renderer>();
        objectRenderer.material.SetFloat("_Fill", start_level);// Inicializar el nivel de agua
        new_level = objectRenderer.material.GetFloat("_Fill"); 
        fill_detection = particles.GetComponent<Fill_detection>(); // Obtener el script de detección de llenado
    }

    // Se ejecuta cada frame fijo
    void FixedUpdate()
    {
        level = objectRenderer.material.GetFloat("_Fill"); // Actualizar el nivel del agua
        spilling(); // Llamar al método de derrame
    }
 
    // Controla el derrame del agua basado en el ángulo de inclinación
    void spilling ()
    {
        // Obtener los ángulos X y Z, ajustándolos al rango de -180 a 180
        xAngle = Mathf.DeltaAngle(0f, container.transform.eulerAngles.x); 
        zAngle = Mathf.DeltaAngle(0f, container.transform.eulerAngles.z);
        // Si el nivel del agua está por encima del mínimo permitido
        if (level > min_level)
        {
            spillAngle = CalculateSpillAngle(level); // Calcular el ángulo en que ocurre el derrame
            // Verificar si el ángulo actual en X o Z supera el ángulo de derrame
            if (IsSpilling(spillAngle))
            {
                // Calcular la diferencia entre el ángulo actual y el ángulo de derrame
                float angleDifference = Mathf.Abs(Mathf.DeltaAngle(CalculateCompositeAngle(), spillAngle));
                spillRate = spillSpeed * angleDifference; // Calcular la tasa de derrame
                // Reducir el nivel de agua según la tasa de derrame
                downLevel(spillRate);
                fsm.SendEvent("Activate_Particles");
                fill_detection.detection(spillRate); // Detectar llenado de otros objetos
            }
            else
            {
                // Si no hay derrame, desactivar las partículas
                fsm.SendEvent("Deactivate_Particles");
            }
        }
        else
        {
            // Asegurar que el nivel no baje por debajo del mínimo y desactivar partículas
            objectRenderer.material.SetFloat("_Fill", min_level);
            fsm.SendEvent("Deactivate_Particles");
        }
    }

    public void downLevel(float downRate)
    {
        if (level > min_level)
        {
            // Reducir el nivel de agua según la tasa de vaciado
            new_level -= downRate * Time.fixedDeltaTime / 25f;
            objectRenderer.material.SetFloat("_Fill", new_level); // Asignar el nuevo nivel al shader
        }
        else
        {
            // Asegurar que el nivel no baje por debajo del mínimo y desactivar partículas
            objectRenderer.material.SetFloat("_Fill", min_level);
        }
    }

    // Método que aumenta el nivel de agua según la tasa de llenado
    public void filling_out(float fillRate)
    {
        if (level < max_level + 0.01f) // Limitar el nivel de llenado
        {
            new_level += fillRate * Time.fixedDeltaTime / 25f; // Incrementar el nivel del agua
            objectRenderer.material.SetFloat("_Fill", new_level); // Actualizar el nivel en el shader
        }
    }

    // Calcula el ángulo a partir del cual comienza el derrame, basado en el nivel del agua
    private float CalculateSpillAngle(float level)
    {
        if (level == min_level)
        {
            return 90f; // Si está vacío, el matraz no se derrama
        }
        else if (level < m_level)
        {
            return max_angle; // Con niveles bajos, el ángulo es mayor
        }
        else if (level >= max_level)
        {
            return 1f; // Si está lleno, el derrame ocurre de inmediato
        }
        else
        {
            // Interpolación lineal entre ángulos de derrame según el nivel
            float normalizedFactor = (level - m_level) / (max_level + 0.1f);
            return Mathf.Lerp(max_angle, 0f, normalizedFactor);
        }
    }

    // Verifica si el ángulo actual está por encima del ángulo de derrame
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
