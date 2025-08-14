using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HutongGames.PlayMaker; // Importa la API de PlayMaker


public class Fill_detection : MonoBehaviour
{
    [SerializeField] private LayerMask layerMask; // Define qué capas serán detectadas por el Raycast
    [SerializeField] private float rayLength = 0.5f; // Longitud máxima del Raycast
    [SerializeField] private GameObject parentObject; // Referencia al objeto padre
    [SerializeField] private bool isChangeColor;
    [SerializeField] private PlayMakerFSM fsm;
    [SerializeField] private float fillTimeThreshold = 2f;
    private Liquid_Level liquid; // Referencia al script de nivel de agua
    private ColorChangeController change_color;
    private bool filling = false;
    private SubstanceTracker substanceTracker;
    [SerializeField] private bool Experiment2;
    [SerializeField] private bool canfill = true;
    private PlayMakerFSM playMakerFSM;

    private Dictionary<GameObject, float> fillTimers = new Dictionary<GameObject, float>(); // Almacena el tiempo de llenado acumulado para cada objeto detectado

    // Método encargado de la detección del objeto a llenar
    public void detection(float spillrate)
    {
        Debug.Log("detectando");
        // Lanza un Raycast en todas las direcciones hacia abajo y guarda los impactos
        RaycastHit[] hits = Physics.RaycastAll(transform.position, Vector3.down, rayLength, layerMask);
        
        // Ordena los impactos por distancia, de más cercano a más lejano
        System.Array.Sort(hits, (hit1, hit2) => hit1.distance.CompareTo(hit2.distance));

        if (hits.Length > 0) // Si hay algún impacto detectado
        {
            // Obtiene el primer objeto impactado
            GameObject detectedObject = hits[0].collider.gameObject;
            Debug.Log("impacto");
            if(detectedObject != parentObject)
            {
                Debug.Log("diferente");
                // Incrementa el temporizador del objeto detectado
                if (!fillTimers.ContainsKey(detectedObject))
                {
                    fillTimers[detectedObject] = 0f;
                }
                fillTimers[detectedObject] += Time.deltaTime;

                // Si se alcanza el umbral
                if (fillTimers[detectedObject] >= fillTimeThreshold)
                {

                    substanceTracker = detectedObject.GetComponent<SubstanceTracker>();
                    if(substanceTracker != null)
                    {
                        substanceTracker.AddSubstance(gameObject.tag);
                        fillTimers[detectedObject] = 0f; // Reinicia el temporizador para el siguiente ciclo
                    }
                }

                // Verifica que el objeto impactado no sea el mismo que el objeto padre
                if (parentObject != null)
                {
                    if(fsm != null)
                        fsm.SendEvent("Fill");
                    // Intenta obtener el componente Liquid_Level del objeto impactado
                    if(canfill)
                        liquid = detectedObject.GetComponent<Liquid_Level>();
                    // Si el objeto impactado tiene el componente Liquid_Level, ajusta el nivel de llenado
                    if (liquid != null)
                    {
                        liquid.filling_out(spillrate); // Pasa la tasa de derrame para llenar el objeto impactado
                    }
                    if(isChangeColor)
                    {
                        change_color = detectedObject.GetComponent<ColorChangeController>();
                        change_color.StartColorChange(parentObject.GetComponent<Renderer>().material);
                    }
                    if(Experiment2)
                    {
                        playMakerFSM = detectedObject.GetComponent<PlayMakerFSM>();
                        string tag1 = gameObject.tag;
                        string tag2 = detectedObject.tag;

                        if ((tag1 == "HCl" && tag2 == "NH4OH") || (tag1 == "NH4OH" && tag2 == "HCl"))
                        {
                            playMakerFSM.SendEvent("Smoke");
                        }
                    }
                }
                else
                {
                    change_color.StopColorChange();
                    if(fsm != null)
                        fsm.SendEvent("No_Fill");
                }
            }
        }
    }

    public void ResetFillTimer(GameObject obj)
    {
        if (fillTimers.ContainsKey(obj))
        {
            fillTimers[obj] = 0f;
        }
    }
}
