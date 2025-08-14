using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialEmissionController : MonoBehaviour
{
    [SerializeField] private Material targetMaterial; // Material asignado directamente desde el Inspector
    //[SerializeField] private float emissionIntensityTarget = 10f; // Intensidad máxima de emisión
    //[SerializeField] private float emissionIntensityStart = 0f; // Intensidad minima de emisión
    [ColorUsage(true, true)]
    [SerializeField] private Color startColor = Color.black; // Color inicial de emisión
    [ColorUsage(true, true)]
    [SerializeField] private Color endColor = Color.white; // Color final de emisión
    [SerializeField] private float duration = 5f; // Duración para alcanzar la intensidad máxima
    [SerializeField] private string fsmName; // Nombre del FSM específico
    //[SerializeField] private PlayMakerFSM playMakerFSM;
    private PlayMakerFSM fsm;

    private void Awake()
    {
        // Buscar todos los FSM en la escena
        var fsmArray = FindObjectsOfType<PlayMakerFSM>();
        fsm = System.Array.Find(fsmArray, f => f.FsmName == fsmName);

        if (fsm == null)
        {
            Debug.LogWarning($"FSM con el nombre '{fsmName}' no encontrado en la escena.");
        }
    }

    public void StartEmissionCoroutine()
    {
        StartCoroutine(AdjustEmissionOverTime());
    }

     private IEnumerator AdjustEmissionOverTime()
    {
        // Interpolar de color inicial a color final y de intensidad mínima a máxima
        yield return ChangeEmission(startColor, endColor);

        // Interpolar de color final a color inicial y de intensidad máxima a mínima
        yield return ChangeEmission(endColor, startColor);
        fsm.SendEvent("goDestapar");
        //playMakerFSM.SendEvent("Fin");
    }

    private IEnumerator ChangeEmission(Color colorStart, Color colorEnd)
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            // Interpolar color e intensidad simultáneamente
            Color currentColor = Color.Lerp(colorStart, colorEnd, elapsedTime / duration);
            //float intensity = Mathf.Lerp(intensityStart, intensityEnd, elapsedTime / duration);

            // Ajustar el color de emisión del material
            targetMaterial.SetColor("_EmissionColor", currentColor);

            elapsedTime += Time.deltaTime;
            Debug.Log($"t: {elapsedTime / duration}, currentColor: {currentColor}");
            yield return null;
        }

        // Asegurar que la intensidad final sea exacta
        targetMaterial.SetColor("_EmissionColor", colorEnd);
    }
}