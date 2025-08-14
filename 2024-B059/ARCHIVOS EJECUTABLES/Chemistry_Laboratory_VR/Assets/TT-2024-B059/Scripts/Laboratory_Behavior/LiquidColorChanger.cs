using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiquidColorChanger : MonoBehaviour, IColorChangable
{
    [SerializeField] private string property_name_Liquid_Color;
    [SerializeField] private string property_name_Surface_Color;
    [SerializeField] private string property_name_Fresnel_Color;
    
    private Material objectMaterial;

    private void Awake()
    {
        objectMaterial = GetComponent<Renderer>().material;
    }

    public void ChangeColor(List<Color> colors)
    {
        objectMaterial.SetColor(property_name_Liquid_Color, colors[0]);
        objectMaterial.SetColor(property_name_Surface_Color, colors[1]);
        objectMaterial.SetColor(property_name_Fresnel_Color, colors[2]);
    }

    public List<Color> GetCurrentColors()
    {
        return new List<Color> {
            objectMaterial.GetColor(property_name_Liquid_Color),
            objectMaterial.GetColor(property_name_Surface_Color),
            objectMaterial.GetColor(property_name_Fresnel_Color)
        };
    }
}