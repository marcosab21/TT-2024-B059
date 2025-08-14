using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PrefabSaver : MonoBehaviour
{
    public void SaveAsPrefab(GameObject gameObject, string newName)
    {
        string localPath = "Assets/Create_Formula/Formula_Prefabs/";
        if (!System.IO.Directory.Exists(localPath))
        {
            System.IO.Directory.CreateDirectory(localPath);
        }

        string prefabPath = $"{localPath}{newName}.prefab";
        PrefabUtility.SaveAsPrefabAsset(gameObject, prefabPath);
        Debug.Log($"Prefab guardado en: {prefabPath}");
    }
}
