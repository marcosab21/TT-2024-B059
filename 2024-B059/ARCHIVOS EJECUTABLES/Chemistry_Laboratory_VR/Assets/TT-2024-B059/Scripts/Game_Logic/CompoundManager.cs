using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq; 
using ChemicalFormulas;
using Sirenix.OdinInspector;

public class CompoundManager : MonoBehaviour
{
    public static CompoundManager Instance { get; private set; }

    [SerializeField] private List<ChemicalFormulas.Compound_List> listCompounds;
    [SerializeField] private string fsmName; // Nombre del FSM específico

    private PlayMakerFSM fsm;
    private int index = 0;

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
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
            //compoundRepository = new CompoundRepository(initialCompounds);
        }
        else
        {
            Destroy(gameObject);
        }
        // Obtener el FSM en el mismo objeto por nombre
        var fsmArray = GetComponents<PlayMakerFSM>();
        fsm = System.Array.Find(fsmArray, f => f.FsmName == fsmName);

        if (fsm == null)
        {
            Debug.LogWarning($"FSM con el nombre '{fsmName}' no encontrado en el mismo objeto.");
        }
    }

    public void changePhase(CompoundData receivedCompound)
    {
        // Verifica si el índice actual es válido dentro de la lista
        if (index < listCompounds.Count)
        {
            Compound_List currentCompoundList = listCompounds[index];

            // Compara el CompoundData recibido con el del índice actual en listCompounds
            if (currentCompoundList.Compound == receivedCompound)
            {
                // Toma el string nextPhase y envía el evento al FSM
                fsm.SendEvent(currentCompoundList.NextPhase);

                // Incrementa el índice para avanzar al siguiente elemento de la lista
                index++;
            }
        }
    }
}