using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Num_Pad_Select : MonoBehaviour
{
    [SerializeField] private Num_Pad_Manager manager;
    [SerializeField] private ButtonType buttonType;

    // Diccionario para mapear tipos de botón a sus acciones
    private Dictionary<ButtonType, System.Action> buttonActions;

    private void Awake()
    {
        // Inicializamos el diccionario con las acciones correspondientes
        buttonActions = new Dictionary<ButtonType, System.Action>
        {
            { ButtonType.Button_0, () => manager.sendData("0") },
            { ButtonType.Button_1, () => manager.sendData("1") },
            { ButtonType.Button_2, () => manager.sendData("2") },
            { ButtonType.Button_3, () => manager.sendData("3") },
            { ButtonType.Button_4, () => manager.sendData("4") },
            { ButtonType.Button_5, () => manager.sendData("5") },
            { ButtonType.Button_6, () => manager.sendData("6") },
            { ButtonType.Button_7, () => manager.sendData("7") },
            { ButtonType.Button_8, () => manager.sendData("8") },
            { ButtonType.Button_9, () => manager.sendData("9") },
            { ButtonType.Button_X, manager.clear_data },
            { ButtonType.Button_Check, manager.validate_data }
        };
    }

    public void PressButton()
    {
        // Ejecutamos la acción asociada al botón
        if (buttonActions.TryGetValue(buttonType, out var action))
        {
            action.Invoke();
        }
        else
        {
            Debug.LogWarning($"No action defined for button type: {buttonType}");
        }
    }
}
