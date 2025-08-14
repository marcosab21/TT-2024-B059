using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

[CreateAssetMenu(fileName = "PhaseDataSO", menuName = "SO/New_Phase")]
public class PhaseData : ScriptableObject
{
    [SerializeField] private string Tittle;
    [SerializeField] [Multiline(10)] private string Data;
    [SerializeField] private VideoClip videoClip;
    [SerializeField] private AudioClip audioClip;

    public string getTittle => Tittle;
    public string getData => Data;
    public VideoClip GetVideoClip => videoClip;
    public AudioClip GetAudioClip => audioClip;
}
