using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HutongGames.PlayMaker;
using System;

public class IgnitionHandler : MonoBehaviour
{
    [SerializeField] private PlayMakerFSM fsm;
    private bool _isIgnited = false;

    // Método para activar la ignición
    public void TriggerIgnition()
    {
        if (fsm != null)
        {
            fsm.SendEvent("Ignition");
            _isIgnited = true;
        }
    }

    // Método para apagar la ignición
    public void ResetIgnition()
    {
        if (fsm != null)
        {
            fsm.SendEvent("Shutdown");
            _isIgnited = false;
        }
    }

    public bool IsIgnited()
    {
        return _isIgnited;
    }
}