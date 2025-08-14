using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class Set_Colors_Liquid : MonoBehaviour
{
    // Variable para almacenar el material del objeto actual
    private Material objectMaterial;

    // Nombres de las propiedades de color en el Shader Graph
    [SerializeField] private string property_name_Liquid_Color;
    [SerializeField] private string property_name_Surface_Color;
    [SerializeField] private string property_name_Fresnel_Color;
    [SerializeField] private Color Liquid_Color;
    [SerializeField] private Color Surface_Color;
    [SerializeField] private Color Fresnel_Color;
    [SerializeField] private GameObject particles;
    private ParticleColorController colorParticles;
    private List<Color> Colors = new List<Color>();


    private void OnEnable()
    {
        Colors.Add(Liquid_Color);
        Colors.Add(Surface_Color);
        Colors.Add(Fresnel_Color);
        
        // Obtener el material del objeto
        objectMaterial = GetComponent<Renderer>().material;

        // Verificar si el material fue obtenido correctamente
        if (objectMaterial == null)
        {
            Debug.LogError("Material no asignado al objeto.");
        }

        objectMaterial.SetColor(property_name_Liquid_Color, Liquid_Color);
        objectMaterial.SetColor(property_name_Surface_Color, Surface_Color);
        objectMaterial.SetColor(property_name_Fresnel_Color, Fresnel_Color);
        
        colorParticles = particles.GetComponent<ParticleColorController>();
        colorParticles.ChangeColor(Colors);
    }

    private void Awake()
    {
        Colors.Add(Liquid_Color);
        Colors.Add(Surface_Color);
        Colors.Add(Fresnel_Color);

        // Obtener el material del objeto
        objectMaterial = GetComponent<Renderer>().material;

        // Verificar si el material fue obtenido correctamente
        if (objectMaterial == null)
        {
            Debug.LogError("Material no asignado al objeto.");
        }

        objectMaterial.SetColor(property_name_Liquid_Color, Liquid_Color);
        objectMaterial.SetColor(property_name_Surface_Color, Surface_Color);
        objectMaterial.SetColor(property_name_Fresnel_Color, Fresnel_Color);
        
        colorParticles = particles.GetComponent<ParticleColorController>();
        colorParticles.ChangeColor(Colors);
    }
}