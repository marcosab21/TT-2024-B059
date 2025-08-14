using UnityEngine;

public class Color_Particles : MonoBehaviour
{
    [SerializeField] private Renderer objectRenderer; // Referencia al Renderer del objeto
    [SerializeField] private ParticleSystem particleSystem;

    public void ChangeBaseMapColor(Color newColor)
    {
        if (objectRenderer != null)
        {
            // Iterar sobre todos los materiales asignados al objeto
            foreach (Material mat in objectRenderer.materials)
            {
                // Cambiar el color del Base Map (propiedad "_BaseColor")
                if (mat.HasProperty("_BaseColor"))
                {
                    mat.SetColor("_BaseColor", newColor);
                }
            }
        }

        // Cambiar el startColor del sistema de partículas
        if (particleSystem != null)
        {
            var mainModule = particleSystem.main;
            mainModule.startColor = newColor;
            var trailsModule = particleSystem.trails;
            trailsModule.colorOverTrail = newColor;
            trailsModule.colorOverLifetime = newColor;
        }
    }
}