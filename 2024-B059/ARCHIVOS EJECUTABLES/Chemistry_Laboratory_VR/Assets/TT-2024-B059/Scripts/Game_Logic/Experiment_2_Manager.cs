using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Sirenix.OdinInspector;

public class Experiment_2_Manager : MonoBehaviour, ISubstanceManager
{
    [SerializeField] private List<string> validCombination; // Lista de sustancias esperadas para una mezcla
    [SerializeField] private string fsmName; // Nombre del FSM específico
    private PlayMakerFSM fsm;
    private Dictionary<SubstanceTracker, HashSet<string>> trackerSubstances  = new Dictionary<SubstanceTracker, HashSet<string>>();
    private Dictionary<SubstanceTracker, float> boilingTimeCounters = new Dictionary<SubstanceTracker, float>();
    private Dictionary<SubstanceTracker, bool> boilingFlags = new Dictionary<SubstanceTracker, bool>();
    private float boilingDelay = 10f;

    [Button("Main Menu")]
    [GUIColor(0f, 1f, 0f)]
    public void Go_Menu()
    {
        fsm.SendEvent("Main_Menu");
    }

    [Button("Reset Level")]
    [GUIColor(1f, 0f, 0f)]
    public void Reset_Level()
    {
        fsm.SendEvent("Reset");
    }
    private void Awake() 
    {
        // Obtener el FSM en el mismo objeto por nombre
        var fsmArray = GetComponents<PlayMakerFSM>();
        fsm = System.Array.Find(fsmArray, f => f.FsmName == fsmName);
        if (fsm == null)
        {
            Debug.LogWarning($"FSM con el nombre '{fsmName}' no encontrado en el mismo objeto.");
        }
    }
    public void RegisterTracker(SubstanceTracker tracker)
    {
        if (!trackerSubstances.ContainsKey(tracker))
        {
            trackerSubstances[tracker] = new HashSet<string>();
        }
        // Inicializar el contador de tiempo para este tracker
        if (!boilingTimeCounters.ContainsKey(tracker))
        {
            boilingTimeCounters[tracker] = 0f;
        }

        if (!boilingFlags.ContainsKey(tracker))
        {
            boilingFlags[tracker] = false;
        }
    }

    public void OnSubstanceAdded(SubstanceTracker tracker, string substance)
    {
        if (!trackerSubstances.ContainsKey(tracker)) return;

        // Agregar la sustancia al conjunto de sustancias del tracker
        trackerSubstances[tracker].Add(substance);

        // Comprobar si la combinación es válida
        if (IsCombinationValid(tracker.GetSubstances()))
        {
            fsm.SendEvent("goCalentamiento");
            PlayMakerFSM playMakerFSM = tracker.gameObject.GetComponent<PlayMakerFSM>();
            playMakerFSM.SendEvent("Smoke");
        }
    }

    private bool IsCombinationValid(List<string> substances)
    {
        // Validar estrictamente
        bool isValid = substances.Count == validCombination.Count 
                    && !substances.Except(validCombination).Any() 
                    && !validCombination.Except(substances).Any();

        return isValid;
    }

    private void HandleTemperature(float temperature, float boilingPoint, SubstanceTracker tracker)
    {
        // Realizar acciones dependiendo de la temperatura
        if (temperature > boilingPoint && !boilingFlags[tracker])
        {
            // Incrementar el contador de tiempo específico para este tracker
            boilingTimeCounters[tracker] += Time.deltaTime;

            if (boilingTimeCounters[tracker] >= boilingDelay)
            {
                fsm.SendEvent("goEnfriamiento");
                boilingFlags[tracker] = true;
                PlayMakerFSM playMakerFSM = tracker.gameObject.GetComponent<PlayMakerFSM>();
                playMakerFSM.SendEvent("Activate_Cristals");
            }
        }
        else if (temperature < boilingPoint)
        {
            PlayMakerFSM playMakerFSM = tracker.gameObject.GetComponent<PlayMakerFSM>();
            boilingTimeCounters[tracker] -= Time.deltaTime;

            if(boilingTimeCounters[tracker] <= 0 && boilingFlags[tracker])
            {
                fsm.SendEvent("goExplicacion");
            }
        }
    }

    private void FixedUpdate()
    {
        // Comprobar la temperatura de cada tracker de manera periódica
        foreach (var tracker in trackerSubstances.Keys)
        {
            TemperatureController temperatureController = tracker.gameObject.GetComponent<TemperatureController>();
            if (temperatureController != null && (temperatureController.GetFlag() ||  boilingFlags[tracker]))
            {
                // Comprobar la temperatura actual y ejecutar las acciones basadas en la temperatura
                HandleTemperature(temperatureController.GetCurrentTemperature(), temperatureController.GetBoilingPoint(), tracker);
            }
        }
    }
}
