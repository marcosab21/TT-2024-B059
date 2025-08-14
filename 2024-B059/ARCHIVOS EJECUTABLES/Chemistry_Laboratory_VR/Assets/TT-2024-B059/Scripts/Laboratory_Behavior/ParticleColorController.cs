using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleColorController : MonoBehaviour, IColorChangable
{
    [SerializeField] private ParticleSystem particleSystem;
    [SerializeField] private Renderer objectRenderer; // Referencia al Renderer del objeto

    public void ChangeColor(List<Color> colors)
    {
        Color color = colors[0];

        if (objectRenderer != null)
        {
            // Iterar sobre todos los materiales asignados al objeto
            foreach (Material mat in objectRenderer.materials)
            {
                // Cambiar el color del Base Map (propiedad "_BaseColor")
                if (mat.HasProperty("_BaseColor"))
                {
                    mat.SetColor("_BaseColor", color);
                }
            }
        }

        if (particleSystem != null)
        {
            var mainModule = particleSystem.main;
            mainModule.startColor = color;
            var trailsModule = particleSystem.trails;
            trailsModule.colorOverTrail = color;
            trailsModule.colorOverLifetime = color;
        }
    }
}