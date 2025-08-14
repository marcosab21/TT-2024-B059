using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Sirenix.OdinInspector;

public class Experiment_4_Manager : MonoBehaviour, ISubstanceManager
{
    [SerializeField] private List<string> validCombination; // Lista de sustancias esperadas para una mezcla
    [SerializeField] private string fsmName; // Nombre del FSM específico
    private PlayMakerFSM fsm;
    private Dictionary<SubstanceTracker, HashSet<string>> trackerSubstances  = new Dictionary<SubstanceTracker, HashSet<string>>();

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
    }

    public void OnSubstanceAdded(SubstanceTracker tracker, string substance)
    {
        if (!trackerSubstances.ContainsKey(tracker)) return;

        // Agregar la sustancia al conjunto de sustancias del tracker
        trackerSubstances[tracker].Add(substance);

        // Comprobar si la combinación es válida
        if (IsCombinationValid(tracker.GetSubstances()))
        {
            fsm.SendEvent("next");
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
}