using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CompoundDataSO", menuName = "SO/CompoundData")]
public class CompoundData : ScriptableObject
{
    [Header("Component Names")]
    [SerializeField] private List<string> componentNames;
    [Header("Result")]
    [SerializeField] private GameObject result;
    [SerializeField] private string Name;
    [SerializeField] private string Common_Denomination;
    [SerializeField] private string Formula;
    [SerializeField] [Multiline(5)] private string Use;
    [SerializeField] [Multiline(5)] private string Natural_Distribution;
    [SerializeField] private string Link_Type;
    [SerializeField] private float Point1;
    [SerializeField] private float Point2;
    [SerializeField] private string Phase;
    [SerializeField] private Sprite I_Compound;

    public List<string> getcomponentNames => componentNames;
    public GameObject getresult => result;
    public string getName => Name;
    public string getDenomination => Common_Denomination;
    public string getformula => Formula;
    public string getuse => Use;
    public string getnaturaldistribution => Natural_Distribution;
    public string getlinktype => Link_Type;
    public float getpoint1 => Point1;
    public float getpoint2 => Point2;
    public string getPhase => Phase;
    public Sprite getSprite => I_Compound;
}
