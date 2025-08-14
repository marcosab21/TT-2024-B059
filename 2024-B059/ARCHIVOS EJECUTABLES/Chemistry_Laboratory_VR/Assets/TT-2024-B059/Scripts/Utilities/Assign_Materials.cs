using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsignarMaterial : MonoBehaviour
{
    [SerializeField] private Material Electrón;
    [SerializeField] private Material Protón;
    [SerializeField] private Material Neutrón;

    void Start()
    {
        string tag = gameObject.tag;

        switch (tag)
        {
            case "Electrón":
                GetComponent<Renderer>().material = Electrón;
                break;
            case "Protón":
                GetComponent<Renderer>().material = Protón;
                break;
            case "Neutrón":
                GetComponent<Renderer>().material = Neutrón;
                break;
        }
    }
}
