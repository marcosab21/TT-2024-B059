using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PhasesSO", menuName = "SO/New_List_Phases")]
public class ListPhaseSO : ScriptableObject
{
    [SerializeField] private List<PhaseData> Phases;

    public List<PhaseData> GetPhases()
    {
        return Phases;
    }
}
