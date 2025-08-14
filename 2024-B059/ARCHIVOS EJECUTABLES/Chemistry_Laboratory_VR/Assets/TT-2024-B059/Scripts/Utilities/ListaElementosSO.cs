using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ElementosSO", menuName = "SO/Nueva_Lista")]
public class ListaElementosSO : ScriptableObject
{
    [SerializeField]
    private List<ElementosSO> Elementos;

    // Método para obtener la lista de elementos.
    public List<ElementosSO> GetElementos()
    {
        return Elementos;
    }
}
