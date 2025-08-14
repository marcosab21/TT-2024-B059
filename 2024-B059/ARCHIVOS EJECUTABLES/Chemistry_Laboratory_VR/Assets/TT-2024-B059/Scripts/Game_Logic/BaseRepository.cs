using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ChemicalFormulas;

public abstract class BaseRepository<T>
{
    private Dictionary<string, int> initialCounts;
    private Dictionary<string, int> runtimeCounts;

    public BaseRepository(List<T> items, System.Func<T, string> getName, System.Func<T, int> getQuantity)
    {
        CreateDictionaries(items, getName, getQuantity);
    }

    private void CreateDictionaries(List<T> items, System.Func<T, string> getName, System.Func<T, int> getQuantity)
    {
        initialCounts = new Dictionary<string, int>();
        runtimeCounts = new Dictionary<string, int>();

        foreach (var item in items)
        {
            string name = getName(item);
            if (!initialCounts.ContainsKey(name))
            {
                initialCounts[name] = getQuantity(item);
                runtimeCounts[name] = 0; // Inicializamos la cantidad en el diccionario de tiempo de ejecución
            }
        }
    }

    public void IncrementCount(string name)
    {
        if(runtimeCounts.ContainsKey(name))
            runtimeCounts[name]++;
    }

    public int GetCount(string name)
    {
        return runtimeCounts.ContainsKey(name) ? runtimeCounts[name] : 0;
    }

    // Método para obtener el diccionario completo de conteos en tiempo de ejecución
    public Dictionary<string, int> GetRuntimeCounts()
    {
        return new Dictionary<string, int>(runtimeCounts); // Devolvemos una copia para evitar modificaciones externas
    }

    // Método para obtener el diccionario inicial
    public Dictionary<string, int> GetInitialCounts()
    {
        return new Dictionary<string, int>(initialCounts);
    }
}