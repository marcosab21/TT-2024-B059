using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class UI_Phases_Manager : MonoBehaviour
{
    [SerializeField] private ListPhaseSO PhasesSO;
    [SerializeField] private Text Tittle;
    [SerializeField] private Text PhaseData;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private VideoPlayer videoPlayer;
    private List<PhaseData> Phases;
    private PhaseData Actual_Phase;

    private void Awake()
    {
        Phases = PhasesSO.GetPhases();

        // Verifica si hay un AudioSource asignado, si no lo hay, agrega uno
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    public void ActualizeData(int index)
    {
        if(index < Phases.Count)
        {
            Actual_Phase = Phases[index];
            Tittle.text = Actual_Phase.getTittle;
            PhaseData.text = Actual_Phase.getData;
            PlayAudio(Actual_Phase.GetAudioClip);
            PlayVideo(Actual_Phase.GetVideoClip);
        }
    }

    public void PlayAudio(AudioClip audio)
    {
        setAudioClip(audio);
        audioSource.Play();
    }

    public void setAudioClip(AudioClip audio)
    {
        audioSource.clip = audio;
    }

    public void PlayVideo(VideoClip video)
    {
        setVideoClip(video);
        videoPlayer.Play();
    }

    public void setVideoClip(VideoClip video)
    {
        videoPlayer.clip = video;
    }
}