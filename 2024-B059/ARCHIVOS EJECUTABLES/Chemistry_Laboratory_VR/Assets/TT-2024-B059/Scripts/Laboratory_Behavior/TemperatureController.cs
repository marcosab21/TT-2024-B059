using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HutongGames.PlayMaker; // Importa la API de PlayMaker

public class TemperatureController : MonoBehaviour
{
    [SerializeField] private PlayMakerFSM fsm;
    [SerializeField] private float maxTemperature = 100f; // Temperatura máxima
    [SerializeField] private float minTemperature = 20f;  // Temperatura mínima ambiente
    [SerializeField] private float boilingPoint = 80f;    // Temperatura de ebullición
    [SerializeField] private float heatingRate = 6f; // Tasa de calentamiento por segundo
    [SerializeField] private float coolingRate = 3f; // Tasa de enfriamiento por segundo 
    [SerializeField] private Liquid_Level liquid_Level;
    private float currentTemperature;
    private bool isHeating = false;

    void Awake()
    {
        currentTemperature = minTemperature;
    }

    void FixedUpdate()
    {
         // Si está calentando, aumentar la temperatura
        if (isHeating)
        {
            UpdateTemperature(heatingRate * Time.deltaTime); // Aumentar temperatura con la tasa de calentamiento
        }
        else
        {
            // Si no está calentando, disminuir la temperatura
            UpdateTemperature(-coolingRate * Time.deltaTime); // Disminuir temperatura con la tasa de enfriamiento
        }
    }

    public void UpdateTemperature(float delta)
    {
        // Ajustar temperatura dentro del rango permitido
        currentTemperature = Mathf.Clamp(currentTemperature + delta, minTemperature, maxTemperature);

        // Determinar el estado de calentamiento según la temperatura
        if (currentTemperature > boilingPoint)
        {
            fsm.SendEvent("Heating");
        }
        else
        {
            fsm.SendEvent("No_Heat");
        }
    }

    // Método para establecer el estado de calentamiento
    public void SetHeating(bool heating)
    {
        isHeating = heating;
    }

    public float GetCurrentTemperature()
    {
        return currentTemperature;
    }

    public float GetBoilingPoint()
    {
        return boilingPoint;
    }

    public bool GetFlag()
    {
        return isHeating;
    }
}