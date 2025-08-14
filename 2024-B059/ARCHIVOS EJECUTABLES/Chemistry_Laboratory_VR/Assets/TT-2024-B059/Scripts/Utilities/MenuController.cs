using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; 
using Sirenix.OdinInspector;

public class MenuController : MonoBehaviour
{
    private Text Tittle;
    private TextMeshProUGUI Data;
    private Image Reference;
    [SerializeField] private PlayMakerFSM fsm;
    private string experiment = "Experiment_";
    private ExperimentData dataSelected;

    [Button("Tutorial")]
    [GUIColor(0f, 0.5f, 1f)]
    public void Go_Tutorial()
    {
        fsm.SendEvent("Tutorial");
    }
    [Button("Basico_Acido")]
    [GUIColor(0f, 1f, 1f)]
    public void Go_Experiment_1()
    {
        fsm.SendEvent("Experiment_1");
    }
    [Button("Nieve_Quimica")]
    [GUIColor(1f, 1f, 0f)]
    public void Go_Experiment_2()
    {
        fsm.SendEvent("Experiment_2");
    }
    [Button("Bruja_Bromo")]
    [GUIColor(1f, 0.5f, 0f)]
    public void Go_Experiment_3()
    {
        fsm.SendEvent("Experiment_3");
    }
    [Button("Lampara_Magnesio")]
    [GUIColor(0.6f, 0f, 0.8f)]
    public void Go_Experiment_4()
    {
        fsm.SendEvent("Experiment_4");
    }


    public void assign_fields(GameObject tittle, GameObject data, GameObject reference)
    {
        if(tittle == null)
        {
            Debug.LogError("Tittle");
        }
        Tittle = tittle.GetComponent<Text>();
        if(data == null)
        {
            Debug.LogError("Data");
        }
        Data = data.GetComponent<TextMeshProUGUI>();
        if(reference == null)
        {
            Debug.LogError("Reference");
        }
        Reference = reference.GetComponent<Image>();
    }

    public void SelectedData(ExperimentData selected)
    {
        if (selected != null) 
        {
            dataSelected = selected;
        }
        else
        {
            Debug.LogError("Selected data is null.");
        }
    }

    public void change_Data()
    {
        if(Tittle != null && Data != null && Reference != null && dataSelected != null)
        {
            Tittle.text = dataSelected.getName;
            Data.text = dataSelected.getDescription;
            Reference.sprite = dataSelected.getReference;
        }
        else
        {
            Debug.LogError("Missing references or dataSelected is null.");
        }
    }

    public enum FSMEvent
    {
        Tutorial_Data,
        Experiment_1_Data,
        Experiment_2_Data,
        Experiment_3_Data,
        Experiment_4_Data,
        Return_Hub,
        Return_Experiments_Data,
        Experiments_Data
    }

    private FSMEvent GetFSMEventByIndex(int index)
    {
        FSMEvent[] values = (FSMEvent[])Enum.GetValues(typeof(FSMEvent));
        
        if (index == 6)
        {
            int aux = dataSelected.getName == "Tutorial" ? 5 : 6;
            return values[aux];
        }

        if (index >= 0 && index < values.Length)
        {
            return values[index]; // Devuelve el valor correspondiente al índice
        }
        else
        {
            Debug.LogError("Índice fuera de rango");
            return FSMEvent.Return_Hub;
        }
    }

    // Función para enviar un evento al FSM directamente usando el enum
    public void SendFSMEventByIndex(int index)
    {
        FSMEvent eventToSend = GetFSMEventByIndex(index);
        if (fsm != null)
        {
            Debug.Log(eventToSend.ToString());
            fsm.SendEvent(eventToSend.ToString());
        }
    }

    public void change_scene()
    {
        string name_scene = dataSelected.getName == "Tutorial" ? "Tutorial" : experiment+dataSelected.getNo_Experiment;

        fsm.SendEvent(name_scene);
    }
}