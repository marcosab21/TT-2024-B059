using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ChemicalFormulas;

public class FormulaBuilder : MonoBehaviour
{
    public string GenerateFormulaName(List<ChemicalFormulas.Compound> reactants, List<ChemicalFormulas.Compound> products)
    {
        // Generación de nombre de fórmula para reactantes y productos.
        string reactantString = GenerateCompoundString(reactants);
        string productString = GenerateCompoundString(products);
        
        return $"{reactantString}={productString}";
    }

    private string GenerateCompoundString(List<ChemicalFormulas.Compound> compounds)
    {
        List<string> compoundStrings = new List<string>();
        foreach (var compound in compounds)
        {
            foreach (var element in compound.Compounds)
            {
                string elementString = element.Quantity > 1 
                    ? $"{element.ElementSO.getformula}_{element.Quantity}" 
                    : element.ElementSO.getformula;
                compoundStrings.Add(elementString);
            }
        }
        return string.Join("+", compoundStrings);
    }
}
