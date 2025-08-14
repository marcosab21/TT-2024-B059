using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "ExperimentData", menuName = "Menu/ExperimentData")]
public class ExperimentData : ScriptableObject
{
    [SerializeField] private string experimentName;
    [SerializeField] [Multiline(10)] private string description;
    [SerializeField] private int no_experiment;
    [SerializeField] private Sprite reference;

    public string getName => experimentName;
    public string getDescription => description;
    public int getNo_Experiment => no_experiment;
    public Sprite getReference => reference;
}
