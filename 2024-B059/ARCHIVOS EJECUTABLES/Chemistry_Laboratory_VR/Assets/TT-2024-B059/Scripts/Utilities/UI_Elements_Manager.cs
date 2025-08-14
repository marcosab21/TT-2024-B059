using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Elements_Manager : MonoBehaviour
{
    private ElementosSO ElementoSO;
    [SerializeField]private Text Elemento;
    [SerializeField]private Text Símbolo;
    [SerializeField]private Text No_Atómico;
    [SerializeField]private Text Masa_Atómica;
    [SerializeField]private Text Clasificación;
    [SerializeField]private Text Configuracion_Electrónica;
    [SerializeField]private Text Punto_de_Fusión;
    [SerializeField]private Text Punto_de_Ebullición;
    [SerializeField]private Text Fase;
    [SerializeField]private Text N_Oxidación;
    [SerializeField]private Image I_Elemento;

    public void RecibirElemento(ElementosSO elemento) // Nueva función para recibir y actualizar la UI
    {
        if (elemento != null)
        {
            Elemento.text = elemento.getName;
            Símbolo.text = elemento.getformula;
            No_Atómico.text = elemento.getAtomic_Number.ToString();
            Masa_Atómica.text = "Masa Atómica: " + elemento.getAtomic_Mass.ToString();
            Clasificación.text = "Clasificación:\n" + elemento.getClassification;
            Configuracion_Electrónica.text = "Configuración Electrónica:\n" + elemento.getElectron_Configuration;
            Punto_de_Fusión.text = (elemento.getMelting_Point != 0) ? "Punto de Fusión:\n" + elemento.getMelting_Point.ToString() + " C°" : "Punto de Fusión:\nDesconocido";
            Punto_de_Ebullición.text = (elemento.getBoiling_Point != 0) ? "Punto de Ebullición:\n" + elemento.getBoiling_Point.ToString() + " C°" :  "Punto de Ebullición:\nDesconocido";
            Fase.text = "Fase: " + elemento.getPhase;
            List<string> oxidationStates = new List<string>();
            foreach (int state in elemento.getOxidation_States)
            {
                oxidationStates.Add(state > 0 ? "+" + state.ToString() : state.ToString());
            }
            N_Oxidación.text = "Números de Oxidación:\n" + string.Join(", ", oxidationStates);
            I_Elemento.sprite = elemento.getSprite;
        }
    }
}
