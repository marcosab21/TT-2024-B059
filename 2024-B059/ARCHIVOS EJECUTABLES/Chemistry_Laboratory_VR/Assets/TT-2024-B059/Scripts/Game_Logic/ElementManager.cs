using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq; 
using ChemicalFormulas;

public class ElementManager : MonoBehaviour
{
    public static ElementManager Instance { get; private set; }

    [SerializeField] private List<ChemicalFormulas.Element> initialElements;
    [SerializeField] private PlayMakerFSM fsm;
    private ElementRepository elementRepository;
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);

            elementRepository = new ElementRepository(initialElements);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Métodos de acceso que llaman a la instancia de IElementManager
    public void IncrementElementCount(string elementName)
    {
        elementRepository.IncrementCount(elementName);
    }

    public int GetElementCount(string elementName)
    {
        return elementRepository.GetCount(elementName);
    }

    public void ValidateElements()
    {
        var initialDictionary = elementRepository.GetInitialCounts();
        var runtimeDictionary = elementRepository.GetRuntimeCounts();

        // Verificar si ambos diccionarios tienen el mismo número de elementos
        if (initialDictionary.Count == runtimeDictionary.Count
        && initialDictionary.All(pair => 
            runtimeDictionary.ContainsKey(pair.Key) && 
            runtimeDictionary[pair.Key] >= pair.Value)
        )
            fsm.SendEvent("goCreacion");
    }
}