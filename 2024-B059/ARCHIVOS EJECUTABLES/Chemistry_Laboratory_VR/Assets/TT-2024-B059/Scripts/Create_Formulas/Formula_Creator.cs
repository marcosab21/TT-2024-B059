using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Sirenix.OdinInspector;
using UnityEngine.UI;
using UnityEditor;
using ChemicalFormulas;

public class Formula_Creator : MonoBehaviour
{
    // Enumeración interna para el tipo de liberación; accesible solo dentro de esta clase.
    
    [SerializeField] private ChemicalFormulas.ReactionType reactionType;
    [SerializeField] private List<Sprite> reaction_sprite; // Lista de imágenes para las reacciones
    [SerializeField] private GameObject reactionImage; // Prefab para la imagen de reacción
    [SerializeField] private List<ChemicalFormulas.Compound> reactants = new List<ChemicalFormulas.Compound>(); // Lista de reactivos como Compound
    [SerializeField] private List<ChemicalFormulas.Compound> products = new List<ChemicalFormulas.Compound>();  // Lista de productos como Compound
    //[SerializeField] private PrefabSaver prefabSaver;
    [SerializeField] private FormulaBuilder formulaBuilder;
    [SerializeField] private CompoundVisualizer compoundVisualizer;
    [SerializeField] private EquationBalancer equationBalancer;

    private void Awake()
    {
        equationBalancer.start_setup(reactants, products);
    }

    [ContextMenu("Create Formula")]
    [Button]
    private void CreateFormula()
    {
        ClearPreviousChildren();
        compoundVisualizer.InstantiateCompound(reactants, true);  // Para reactivos
        InstantiateReactionImage();
        compoundVisualizer.InstantiateCompound(products, false);    // Para productos

        string formulaName = formulaBuilder.GenerateFormulaName(reactants, products);
        //prefabSaver.SaveAsPrefab(gameObject, formulaName);

        Debug.Log("Fórmula creada con éxito.");
    }

    private void ClearPreviousChildren()
    {
         // Utilizar un bucle for inverso para eliminar los hijos
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(transform.GetChild(i).gameObject);
        }
    }

    private void InstantiateReactionImage()
    {
        int reactionIndex = (int)reactionType; // Convierte el tipo de reacción a un índice
        if (reactionImage != null && reactionIndex < reaction_sprite.Count)
        {
            GameObject reactionImageObject = Instantiate(reactionImage, transform);
            Image targetImage = reactionImageObject.GetComponent<Image>();
            targetImage.sprite = reaction_sprite[reactionIndex];
        }
    }    
}
