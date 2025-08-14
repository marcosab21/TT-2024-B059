using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace ChemicalFormulas
{
    public enum ReleaseType
    {
        None,
        Release,
        Precipitation
    }

    public enum ReactionType
    {
        Reaction,
        Reaction_Heat,
        Reversible
    }

    [System.Serializable]
    public class Compound
    {
        [SerializeField] private List<Element> compound = new List<Element>();
        [SerializeField] private ReleaseType release;
        [SerializeField] private TMP_InputField coefficient;

        public List<Element> Compounds => compound;
        public ReleaseType Release => release;
        public TMP_InputField Coefficient
        {
            get => coefficient;
            set => coefficient = value;
        } 
    }

    [System.Serializable]
    public class Element
    {
        [SerializeField] private ElementosSO element;
        [SerializeField] [Range(1, 99)] private int quantity;

        public ElementosSO ElementSO => element;
        public int Quantity => quantity;
    }

    [System.Serializable]
    public class CompoundSO
    {
        [SerializeField] private CompoundData compoundData;
        [SerializeField] [Range(1, 99)] private int quantity;

        public CompoundData Compound => compoundData;
        public int Quantity => quantity;
    }

    [System.Serializable]
    public class Compound_List
    {
        [SerializeField] private CompoundData compoundData;
        [SerializeField] private string nextPhase;
        
        public CompoundData Compound => compoundData;
        public string NextPhase => nextPhase;
    }
}
