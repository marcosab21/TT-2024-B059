using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CompoundsSO", menuName = "SO/List_Compounds")]
public class Compounds_List : ScriptableObject
{
    [SerializeField]
    private List<CompoundData> Compounds;

    // Método para obtener la lista de Compuestos.
    public List<CompoundData> GetCompounds()
    {
        return Compounds;
    }
}
