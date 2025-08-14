using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Text.RegularExpressions; // Para usar expresiones regulares

public class UI_Info_Compounds_Manager : MonoBehaviour
{
    [SerializeField] private TMP_Text compoundText; // Referencia al TextMeshPro que mostrará los elementos
    private List<string> currentElements = new List<string>(); // Lista para almacenar los elementos

    // Método para actualizar los elementos en la lista de la UI
    public void UpdateElements(List<string> elements, CompoundData compoundData)
    {
        currentElements = elements;
        UpdateCompoundText(compoundData);
    }

    // Método modificado para incluir la verificación del CompoundData
    private void UpdateCompoundText(CompoundData compoundData)
    {
        // Si no hay elementos, mostrar "? + ? = ?"
        if (currentElements.Count == 0)
        {
            compoundText.text = "? + ? -> ?";
            return;
        }

        // Si hay elementos, generar el string con formato
        string displayText = "";
        for (int i = 0; i < currentElements.Count; i++)
        {
            // Extraer el contenido entre paréntesis usando una expresión regular
            string symbol = ExtractSymbol(currentElements[i]);

            displayText += symbol;

            if (i < currentElements.Count - 1)
            {
                displayText += " + ";
            }
        }

        // Si compoundData es null, agregar el final con "= ?"
        if (compoundData == null)
        {
            displayText += " -> ?";
        }
        else
        {
            // Si compoundData no es null, agregar la fórmula del compuesto
            displayText += " -> " + compoundData.getformula;
        }

        // Asignar el texto final al componente TextMeshPro
        compoundText.text = displayText;
    }

    // Método para extraer el texto dentro de paréntesis
    private string ExtractSymbol(string elementName)
    {
        // Usamos una expresión regular para buscar el texto dentro de paréntesis
        Match match = Regex.Match(elementName, @"\(([^)]+)\)");
        if (match.Success)
        {
            return match.Groups[1].Value; // Devolver el texto dentro del paréntesis
        }
        return elementName; // En caso de no encontrar paréntesis, devolver el nombre completo
    }
}
