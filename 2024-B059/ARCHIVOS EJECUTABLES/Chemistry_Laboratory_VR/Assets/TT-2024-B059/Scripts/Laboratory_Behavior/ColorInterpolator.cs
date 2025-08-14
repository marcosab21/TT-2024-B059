using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ColorInterpolator : MonoBehaviour
{
    private float lerpDuration;
    private float lerpProgress;
    private List<Color> startColors;
    private List<Color> targetColors;

    public ColorInterpolator(float duration)
    {
        lerpDuration = duration;
        lerpProgress = 0f;
    }

    public void StartInterpolation(List<Color> start, List<Color> target)
    {
        startColors = start;
        targetColors = target;
        lerpProgress = 0f;
    }

    public bool Interpolate(out List<Color> currentColors)
    {
        currentColors = new List<Color>();
        lerpProgress += Time.deltaTime / lerpDuration;
        
        for (int i = 0; i < startColors.Count; i++)
        {
            currentColors.Add(Color.Lerp(startColors[i], targetColors[i], lerpProgress));
        }

        return lerpProgress >= 1f;
    }
}