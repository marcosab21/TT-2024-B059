using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class Experiment_1_Manager : MonoBehaviour, ISubstanceManager
{
    [SerializeField] private List<string> substanceSequence; // Secuencia esperada de sustancias
    [SerializeField] private List<string> fsmEvents; // Eventos FSM asociados a la secuencia
    [SerializeField] private string fsmName; // Nombre del FSM específico
    [SerializeField] private List<Color> targetColors; // Colores para cada estado
    [SerializeField] private List<float> lerpDurations; // Duraciones de interpolación para cada estado
    private PlayMakerFSM fsm;
    private Dictionary<SubstanceTracker, int> trackerStates = new Dictionary<SubstanceTracker, int>();

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
        if (!trackerStates.ContainsKey(tracker))
        {
            trackerStates[tracker] = 0;
        }
    }

    public void OnSubstanceAdded(SubstanceTracker tracker, string substance)
    {
        if (!trackerStates.ContainsKey(tracker)) return;

        int currentState = trackerStates[tracker];
        if (currentState < substanceSequence.Count && substance == substanceSequence[currentState])
        {
            trackerStates[tracker]++;
            TriggerFSMEvent(tracker, fsmEvents[currentState]);

            if (currentState > 0 && currentState <= targetColors.Count)
            {
                ChangeColorForState(tracker, currentState - 1); // Ajusta el índice para coincidir con la lista
            }
        }
    }

    private void TriggerFSMEvent(SubstanceTracker tracker, string eventName)
    {
        if (fsm != null)
        {
            fsm.SendEvent(eventName);
            Debug.Log($"Event '{eventName}' triggered for tracker '{tracker.name}'");
        }
    }

    private void ChangeColorForState(SubstanceTracker tracker, int stateIndex)
    {
        GameObject trackerObject = tracker.gameObject;
        ColorChangeController colorController = trackerObject.GetComponent<ColorChangeController>();

        if (colorController != null)
        {
            colorController.StartColorChange(targetColors[stateIndex], lerpDurations[stateIndex]);
            Debug.Log($"Cambio de color iniciado para '{trackerObject.name}' con el color {targetColors[stateIndex]} y duración {lerpDurations[stateIndex]}.");
        }
        else
        {
            Debug.LogWarning($"El objeto '{trackerObject.name}' no tiene un componente ColorChangeController.");
        }
    }
}
