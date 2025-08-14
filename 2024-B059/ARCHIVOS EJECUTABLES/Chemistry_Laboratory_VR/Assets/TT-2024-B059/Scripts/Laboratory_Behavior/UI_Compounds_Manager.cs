using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_Compounds_Manager : MonoBehaviour
{
    private CompoundData Compound;
    [SerializeField] private Text Compuesto;
    [SerializeField] private Text Denominación_Común;
    [SerializeField] private TextMeshProUGUI Fórmula_Química;
    [SerializeField] private Text Usos;
    [SerializeField] private Text Distribución_Natural;
    [SerializeField] private Text Punto1;
    [SerializeField] private Text Punto2;
    [SerializeField] private Text Fase;
    [SerializeField] private Text Enlace;
    [SerializeField] private Image I_Compuesto;

    public void ReceiveCompound(CompoundData Compound)
    {
        if (Compound != null)
        {
            Compuesto.text = Compound.getName;
            Denominación_Común.text = "Denominación Común: " + Compound.getDenomination;
            Fórmula_Química.text = Compound.getformula;
            Usos.text = "Usos: " + Compound.getuse;
            Distribución_Natural.text = "Distribución Natural: " + Compound.getnaturaldistribution;
            Fase.text = "Fase: " + Compound.getPhase;

             // Verificar los puntos y establecer "No data" si son nulos o inválidos
            string point1Text = Compound.getpoint1 <=  -274.15f ? Compound.getpoint1.ToString() + " C°" : "No data";
            string point2Text = Compound.getpoint2 <=  -274.15f ? Compound.getpoint2.ToString() + " C°" : "No data";

            Punto1.text = "Punto de Fusión:\n" + point1Text; 
            Punto2.text = "Punto de Ebullición:\n" + point2Text;
/*
            switch(Compound.getPhase)
            {
                case "Sólido": 
                    Punto1.text = "Punto de Fusión:\n" + point1Text; 
                    Punto2.text = "Punto de Sublimación:\n" + point2Text;
                    break;

                case "Líquido": 
                    Punto1.text = "Punto de Solidificación:\n" + point1Text;
                    Punto2.text = "Punto de Ebullición:\n" + point2Text; 
                    break;

                case "Gas": 
                    Punto1.text = "Punto de Deposición:\n" + point1Text;
                    Punto2.text = "Punto de  Condensación:\n" + point2Text; 
                    break;
            }
*/
            Enlace.text = "Tipo de Enlace: " + Compound.getlinktype;
            I_Compuesto.sprite = Compound.getSprite;
        }
    }
}
