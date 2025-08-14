using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChangeController : MonoBehaviour
{
    [SerializeField] private GameObject particles;
    [SerializeField] private float lerpDuration;
    [SerializeField] private PlayMakerFSM fsm;

    private LiquidColorChanger liquidColorChanger;
    private ParticleColorController particleColorController;
    private ColorInterpolator interpolator;
    private List<Color> previousTargetColors;
    private List<Color> TargetColors;

    private bool isLerping = false;

    private void Awake()
    {
        liquidColorChanger = GetComponent<LiquidColorChanger>();
        particleColorController = particles.GetComponent<ParticleColorController>();
        interpolator = new ColorInterpolator(lerpDuration);
        previousTargetColors = new List<Color>();
        TargetColors = new List<Color>();
    }

    public void StartColorChange(Material newMaterial)
    {
        // Obtener los nuevos colores objetivos
        TargetColors = new List<Color> {
            newMaterial.GetColor("_LiquidColor"),
            newMaterial.GetColor("_SurfaceColor"),
            newMaterial.GetColor("_FresnelColor")
        };

        // Comparar los nuevos colores con los anteriores
        if (!ColorsAreEqual(TargetColors, previousTargetColors))
        {
            // Si los colores son diferentes, reiniciar la interpolación
            List<Color> startColors = liquidColorChanger.GetCurrentColors();
            interpolator.StartInterpolation(startColors, TargetColors);
            previousTargetColors = new List<Color>(TargetColors); // Actualizar los colores objetivos
            isLerping = true;
        }
        else
        {
            ResumeColorChange();
        }
    }

    public void StartColorChange(Color newColor, float newLerpDuration)
    {
        // Actualizar la duración de la interpolación
        interpolator = new ColorInterpolator(newLerpDuration);

        // Definir los nuevos colores objetivos basados en el único color proporcionado
        TargetColors = new List<Color> { newColor, newColor, newColor };

        // Comparar los nuevos colores con los anteriores
        if (!ColorsAreEqual(TargetColors, previousTargetColors))
        {
            // Si los colores son diferentes, reiniciar la interpolación
            List<Color> startColors = liquidColorChanger.GetCurrentColors();
            interpolator.StartInterpolation(startColors, TargetColors);
            previousTargetColors = new List<Color>(TargetColors); // Actualizar los colores objetivos
            isLerping = true;
        }
        else
        {
            ResumeColorChange();
        }
    }

    private bool ColorsAreEqual(List<Color> colors1, List<Color> colors2)
    {
        // Compara cada color de las listas
        if (colors1.Count != colors2.Count) return false;

        for (int i = 0; i < colors1.Count; i++)
        {
            if (!colors1[i].Equals(colors2[i])) return false;
        }
        return true;
    }

    public void StopColorChange()
    {
        // Detener la interpolación pero mantener el color actual
        isLerping = false;
    }

    public void ResumeColorChange()
    {
        // Reanudar la interpolación
        isLerping = true;
    }

    private void FixedUpdate()
    {
        if (isLerping)
        {
            bool isComplete = interpolator.Interpolate(out List<Color> currentColors);

            liquidColorChanger.ChangeColor(currentColors);
            particleColorController.ChangeColor(currentColors);

            if (isComplete)
            {
                StopColorChange();
                fsm.SendEvent("Complete_Change");
            }
        }
    }

    private void DebugColors(List<Color> colors, string listName)
    {
        string colorValues = $"{listName}: ";
        foreach (Color color in colors)
        {
            colorValues += $"[{color.r:F2}, {color.g:F2}, {color.b:F2}, {color.a:F2}] ";
        }
        Debug.Log(colorValues);
    }
}
