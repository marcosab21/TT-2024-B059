using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "ElementosSO", menuName = "SO/Nuevo_Elemento")]
public class ElementosSO : ScriptableObject
{
    [SerializeField] private GameObject Element;
    [SerializeField] private string Name;
    [SerializeField] private string Symbol;
    [SerializeField] private int Atomic_Number;
    [SerializeField] private float Atomic_Mass;
    [SerializeField] private string Classification;
    [SerializeField] private string Electron_Configuration;
    [SerializeField] private float Melting_Point;
    [SerializeField] private float Boiling_Point;
    [SerializeField] private string Phase;
    [SerializeField] private List<int> Oxidation_States;
    [SerializeField] private Material Effect;
    [SerializeField] private Sprite I_Element;
    [SerializeField] [Multiline(10)] private string  Description;

    public GameObject getElement => Element;
    public string getName => Name;
    public string getformula => Symbol;
    public int getAtomic_Number => Atomic_Number;
    public float getAtomic_Mass => Atomic_Mass;
    public string getClassification => Classification;
    public string getElectron_Configuration => Electron_Configuration;
    public float getMelting_Point => Melting_Point;
    public float getBoiling_Point => Boiling_Point;
    public string getPhase => Phase;
    public List<int> getOxidation_States => Oxidation_States;
    public Material getEffect => Effect;
    public string getDescription => Description;
    public Sprite getSprite => I_Element;
}
