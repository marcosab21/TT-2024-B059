using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using ChemicalFormulas;

public class CompoundVisualizer : MonoBehaviour
{
   // [SerializeField] private Transform parentTransform;
    [SerializeField] private GameObject coefficient;
    [SerializeField] private GameObject elementCompound;
    [SerializeField] private GameObject operation;
    [SerializeField] private GameObject releasePrecipitation;

    public void InstantiateCompound(List<ChemicalFormulas.Compound> compounds, bool isReactant)
    {
        for (int i = 0; i < compounds.Count; i++)
        {
            if (i > 0)
            {
                Instantiate(operation, transform);
            }

            GameObject coefficientInstance = Instantiate(coefficient, transform);
            TMP_InputField tmpInputField = coefficientInstance.GetComponent<TMP_InputField>();
            compounds[i].Coefficient = tmpInputField;

            GameObject elementObj = Instantiate(elementCompound, transform);
            TextMeshProUGUI textComponent = elementObj.GetComponentInChildren<TextMeshProUGUI>();
            if (textComponent != null)
            {
                textComponent.text = GenerateFormulaText(compounds[i].Compounds);
            }

            // Solo para productos, verifica el tipo de liberación
            if (!isReactant && compounds[i].Release != ReleaseType.None)
            {
                Instantiate(releasePrecipitation, transform);
            }
        }
    }

    private string GenerateFormulaText(List<ChemicalFormulas.Element> elements)
    {
        string formula = "";
        foreach (var element in elements)
        {
            formula += element.Quantity > 1
                ? $"{element.ElementSO.getformula}<sub>{element.Quantity}</sub>" 
                : element.ElementSO.getformula;
        }
        return formula;
    }
}
