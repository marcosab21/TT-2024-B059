using System.Collections.Generic;
using UnityEngine;

public class CompoundQuantityManager : MonoBehaviour
{
    [System.Serializable]
    private class CompoundLimit
    {
        public string compoundName; // Nombre del compuesto
        public int maxInstances; // Límite máximo de instancias permitidas
    }

    [SerializeField] private List<CompoundLimit> compoundLimits; // Lista de compuestos y sus límites
    private Dictionary<string, Queue<GameObject>> compoundInstances = new Dictionary<string, Queue<GameObject>>(); // Almacena las instancias activas

    public void RegisterCompound(GameObject compoundObject)
    {
        string compoundName = compoundObject.name;

        // Si no hay un límite definido para este compuesto, no hacer nada
        CompoundLimit limit = compoundLimits.Find(c => compoundName.StartsWith(c.compoundName));
        if (limit == null) return;

        // Si no existe un registro para este compuesto, crearlo
        if (!compoundInstances.ContainsKey(limit.compoundName))
        {
            compoundInstances[limit.compoundName] = new Queue<GameObject>();
        }

        // Agregar el compuesto a la cola correspondiente
        compoundInstances[limit.compoundName].Enqueue(compoundObject);

        // Verificar si se supera el límite
        if (compoundInstances[limit.compoundName].Count > limit.maxInstances)
        {
            RemoveOldestInstance(limit.compoundName);
        }
    }

    private void RemoveOldestInstance(string compoundName)
    {
        if (compoundInstances.ContainsKey(compoundName) && compoundInstances[compoundName].Count > 0)
        {
            GameObject oldestInstance = compoundInstances[compoundName].Dequeue();

            if (oldestInstance != null)
            {
                Destroy(oldestInstance);
                Debug.Log($"Se eliminó la instancia más antigua del compuesto: {compoundName}");
            }
        }
    }
}
