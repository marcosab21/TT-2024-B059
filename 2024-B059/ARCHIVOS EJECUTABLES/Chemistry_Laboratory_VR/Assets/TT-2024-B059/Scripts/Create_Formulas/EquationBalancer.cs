using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ChemicalFormulas;

public class EquationBalancer : MonoBehaviour
{
    [SerializeField] private List<ChemicalFormulas.Compound> _reactants;
    [SerializeField] private List<ChemicalFormulas.Compound> _products;
    [SerializeField] private UI_Phases_Manager phases_Manager;
    [SerializeField] private AudioClip Balanced;
    [SerializeField] private AudioClip Desbalanced;
    [SerializeField] private string fsmName;
    [SerializeField] private Text outputText;
    [SerializeField] private GameObject _fsm;
    private PlayMakerFSM fsm;
    private bool isBalanced = true; 
    public void start_setup(List<ChemicalFormulas.Compound> reactants, List<ChemicalFormulas.Compound> products)
    {
        _reactants = reactants;
        _products = products;

        var fsmArray = _fsm.GetComponents<PlayMakerFSM>();
        fsm = System.Array.Find(fsmArray, f => f.FsmName == fsmName);

        // Actualizar el texto inicial
        UpdateFormattedText();

        if (fsm == null)
        {
            Debug.LogWarning($"FSM con el nombre '{fsmName}' no encontrado en el mismo objeto.");
        }
    }

     public void ValidateBalance()
    {
        isBalanced = true;
        Dictionary<string, int> reactantCounts = CountElements(_reactants);
        Dictionary<string, int> productCounts = CountElements(_products);

        // Comparar ambos diccionarios para verificar si son iguales
        foreach (var element in reactantCounts)
        {
            if (!productCounts.TryGetValue(element.Key, out int productCount) || productCount != element.Value)
            {
                phases_Manager.setAudioClip(Desbalanced);
                fsm.SendEvent("Desbalanced");
                Debug.Log($"Desbalanceado: {element.Key} tiene {element.Value} en reactantes y {productCount} en productos.");
                isBalanced = false; 
            }
        }

        if (isBalanced)
        {
            phases_Manager.setAudioClip(Balanced);
            fsm.SendEvent("Balanced");
        }
    }

    private Dictionary<string, int> CountElements(List<ChemicalFormulas.Compound> compounds)
    {
        Dictionary<string, int> elementCounts = new Dictionary<string, int>();

        foreach (var compound in compounds)
        {
            int coefficient = int.Parse(compound.Coefficient.text);
            foreach (var element in compound.Compounds)
            {
                string elementSymbol = element.ElementSO.getformula; // Asegúrate de que esta propiedad devuelva el símbolo del elemento
                int quantity = element.Quantity;

                // Agregar o actualizar el conteo del elemento en el diccionario
                if (elementCounts.ContainsKey(elementSymbol))
                {
                    elementCounts[elementSymbol] += (quantity * coefficient);
                }
                else
                {
                    elementCounts[elementSymbol] = (quantity * coefficient);
                }
            }
        }

        return elementCounts;
    }

    public void UpdateFormattedText()
    {
        Dictionary<string, int> reactantCounts = CountElements(_reactants);
        Dictionary<string, int> productCounts = CountElements(_products);

        string balancedText = FormatBalancedElements(reactantCounts, productCounts);
        outputText.text = balancedText; // Asegúrate de que `outputText` esté asignado
    }

    private string FormatBalancedElements(Dictionary<string, int> reactantCounts, Dictionary<string, int> productCounts)
    {
        string formattedText = "";

        foreach (var element in reactantCounts.Keys)
        {
            int reactantValue = reactantCounts.ContainsKey(element) ? reactantCounts[element] : 0;
            int productValue = productCounts.ContainsKey(element) ? productCounts[element] : 0;

            formattedText += $"{reactantValue} - {element} - {productValue}\n";
        }

        return formattedText;
    }
}
