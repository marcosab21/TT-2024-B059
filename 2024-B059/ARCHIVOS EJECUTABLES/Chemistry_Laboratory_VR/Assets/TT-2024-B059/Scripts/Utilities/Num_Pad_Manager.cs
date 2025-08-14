using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Num_Pad_Manager : MonoBehaviour
{
    [SerializeField] private EquationBalancer equationBalancer;
    
    [SerializeField] private TMP_InputField selectedInputField; // El campo de entrada seleccionado actualmente
    private bool isFieldEmpty  = true;

    // Método para detectar el campo de entrada seleccionado
    public void SelectInputField(TMP_InputField inputField)
    {
        selectedInputField = inputField;
        isFieldEmpty = true;
        clear_data();
    }

    public void DeselectInputField()
    {
        selectedInputField = null;
        isFieldEmpty = true;
    }
    public void sendData(string num)
    {
        if (selectedInputField == null) return;

        // Añade solo si no se ha alcanzado el límite de caracteres
        if (selectedInputField.text.Length <selectedInputField.characterLimit)
        {
            // Reemplaza el contenido si está vacío o añade el número al final
            selectedInputField.text = isFieldEmpty ? num : selectedInputField.text + num;
            isFieldEmpty = false; // Marca el campo como no vacío después de añadir el número
        }
    }

    public void clear_data()
    {
        if (selectedInputField != null)
        {
            selectedInputField.text = "1"; // Limpia el texto del campo seleccionado
            isFieldEmpty  = true;
        }
    }

    public void validate_data()
    {
        GameObject balancerObject = GameObject.FindGameObjectWithTag("Equation");
        if (balancerObject != null)
        {
            equationBalancer = balancerObject.GetComponent<EquationBalancer>();
            
            if (equationBalancer == null)
            {
                Debug.LogError("El objeto con el tag 'EquationBalancer' no tiene un componente EquationBalancer.");
            }
        }
        else
        {
            Debug.LogError("No se encontró ningún objeto con el tag 'EquationBalancer'.");
            return; // Detén la validación si no se encuentra el objeto
        }
        
        // Llama a ValidateBalance si equationBalancer está asignado correctamente
        equationBalancer.ValidateBalance();
    }

    public void assign_Equation(EquationBalancer equation)
    {
        equationBalancer = equation;
    }
}