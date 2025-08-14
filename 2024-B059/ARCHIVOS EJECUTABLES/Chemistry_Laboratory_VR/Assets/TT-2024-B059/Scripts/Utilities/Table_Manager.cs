using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table_Manager : MonoBehaviour
{
    [SerializeField] private ListaElementosSO listaElementosSO;
    [SerializeField] private UI_Elements_Manager uiElementsManager;
    [SerializeField] private Renderer objectRenderer;
    [SerializeField] private Color select_color;
    [SerializeField] private Color emission_Color;
    [SerializeField] private PlayMakerFSM fsm;
    private List<ElementosSO> Elementos;
    private ElementosSO Elemento;
    private GameObject instancia;
    private Material material;
    private Material letters;
    private Color initialColor; // Variable para almacenar el color inicial
    private string Nombre;
    private int index;
    
    void OnEnable()
    {
        Elementos = listaElementosSO.GetElementos();
        Nombre = gameObject.name;
        int guionBajoIndex = Nombre.IndexOf('_');
        string index_s = Nombre.Substring(0, guionBajoIndex);
        if (int.TryParse(index_s, out index)) // Intentamos convertir la cadena del número a un entero
        {
            index -= 1;
            Elemento = Elementos[index];
        }

        initialColor = color_initial();
        material = objectRenderer.materials[1];
        letters = objectRenderer.materials[0];
    }

    public void instanciar_elemento(){
        GameObject isotopo = Elemento.getElement;

        // Verifica si el elemento ya ha sido instanciado
        if(instancia != null){
            Destroy(instancia);
        }
        
        // Instancia el nuevo elemento
        instancia = Instantiate(isotopo);
    }

    public void hover_Effect()
    {
        material.SetColor("_BaseColor", select_color);
        letters.EnableKeyword("_EMISSION");
        letters.SetColor("_EmissionColor", emission_Color);
    }

    public void select_sound()
    {
        fsm.SendEvent("Select");
    }

    public void unhover_efect()
    {
        letters.DisableKeyword("_EMISSION");
        material.SetColor("_BaseColor", initialColor);
    }

    private Color color_initial()
    {
        // Guardar el color inicial del primer material (índice 0) al iniciar
        if (objectRenderer != null && objectRenderer.materials.Length > 0)
        {
            Material mat = objectRenderer.materials[1];
            if (mat.HasProperty("_BaseColor"))
            {
                return mat.GetColor("_BaseColor");
            }
        }

        return select_color;
    }

    public void EnviarElemento() // Nueva función para enviar Elemento al UI_Elements_Manager
    {
        if (uiElementsManager != null)
        {
            uiElementsManager.RecibirElemento(Elemento);
        }
    }

    public void validar()
    {
        if(ElementManager.Instance != null)
        {
            ElementManager.Instance.IncrementElementCount(Elemento.getformula);
            ElementManager.Instance.ValidateElements();
        }
    }
}
