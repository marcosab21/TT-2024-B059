using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Elements_ValidateSO", menuName = "SO/Elements_Validate")]
public class Elements_ValidateSO : ScriptableObject
{
    [SerializeField] private List<GameObject> Elementos;

    public List<GameObject> GetElementos()
    {
        return Elementos;
    }
}
