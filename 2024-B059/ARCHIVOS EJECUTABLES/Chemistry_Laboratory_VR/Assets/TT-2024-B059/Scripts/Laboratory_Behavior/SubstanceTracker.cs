using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SubstanceTracker : MonoBehaviour
{
    // Lista de tags que representan las sustancias contenidas en este objeto
    [SerializeField] private List<string> containedSubstances = new List<string>();
    [SerializeField] private LayerMask allowedLayers;
    [SerializeField] private PlayMakerFSM fsm;
    [SerializeField] private Vector3 startPosition;
    [SerializeField] private Vector3 targetPositionOffset; // Offset para la posición del hermano
    [SerializeField] private Vector3 targetRotationEuler;
    private ISubstanceManager manager;

    private void Awake()
    {
        FindAndConnectManager();
        // Verifica si el objeto tiene un tag asignado antes de llamar a AddSubstance
        if (gameObject.tag != "Untagged")
        {
            AddSubstance(gameObject.tag);
        }
    }
    
    private void FindAndConnectManager()
    {
         // Determinar el tipo de manager según la escena actual
        string currentSceneName = SceneManager.GetActiveScene().name;

        switch (currentSceneName)
        {
            case "Experiment_1":
                manager = FindObjectOfType<Experiment_1_Manager>();
                break;
            case "Experiment_2":
                manager = FindObjectOfType<Experiment_2_Manager>();
                break;
            case "Experiment_3":
                manager = FindObjectOfType<Experiment_3_Manager>();
                break;
            case "Experiment_4":
                manager = FindObjectOfType<Experiment_4_Manager>();
                break;
            default:
                Debug.LogError($"No se encontró un manager en la escena '{currentSceneName}'.");
                break;
        }

        if (manager != null)
        {
            ConnectToManager(manager); // Conexión al manager
        }
    }

    // Método para agregar una nueva sustancia a la lista
    public void AddSubstance(string substanceTag)
    {
        if (!containedSubstances.Contains(substanceTag))
        {
            containedSubstances.Add(substanceTag);
            // Notificar al manager cuando se añade una nueva sustancia
            manager?.OnSubstanceAdded(this, substanceTag);
        }
    }

    // Método para compartir sustancias con otro objeto
    public void ShareSubstances(SubstanceTracker targetTracker)
    {
        foreach (string substance in containedSubstances)
        {
            targetTracker.AddSubstance(substance);
        }
    }

    // Devuelve la lista de sustancias contenidas
    public List<string> GetSubstances()
    {
        return containedSubstances;
    }

    // Detecta interacciones con objetos en el Layer permitido usando un Trigger
    private void OnTriggerEnter(Collider other)
    {
        // Verificar si el objeto pertenece al Layer permitido
        if ((allowedLayers.value & (1 << other.gameObject.layer)) != 0)
        {
            AddSubstance(other.gameObject.tag);

            // Cambiar el objeto colisionado a hermano
            MakeObjectSibling(other.gameObject);

            if (fsm != null)
            {
                fsm.SendEvent("Fog");
            }
        }
    }

    // Método para conectar este tracker con un manager externo
    public void ConnectToManager<T>(T manager) where T : ISubstanceManager
    {
        manager.RegisterTracker(this);
    }

    // Método para hacer que el objeto colisionado sea hermano del objeto actual
    private void MakeObjectSibling(GameObject otherObject)
    {
        Transform originalParent = otherObject.transform.parent;

        originalParent.SetParent(transform.parent);

        // Desactivar el Rigidbody y los Colliders de originalParent
        Rigidbody rb = originalParent.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true; // Desactivar la física, si es necesario
            rb.useGravity = false; // Desactivar la gravedad
        }

        // Desactivar todos los colliders en el originalParent
        Collider[] colliders = originalParent.GetComponentsInChildren<Collider>();
        foreach (Collider col in colliders)
        {
            if (!col.isTrigger) // Verifica si el collider no es un trigger
            {
                col.enabled = false;
            }
        }

        // Definir la rotación antes de la interpolación de posición
        Quaternion targetRotation = Quaternion.Euler(targetRotationEuler); // Convertir los valores serializados a Quaternion
        originalParent.localRotation = targetRotation;
        Debug.Log(originalParent.localRotation);

        // Realizar la transición de posición usando una corutina para un movimiento suave
        StartCoroutine(ChangePositionSmoothly(originalParent, 3f)); // 3 segundo de transición
    }
    // Corutina para hacer un cambio suave de posición
    private IEnumerator ChangePositionSmoothly(Transform originalParent, float transitionDuration)
    {
        //Vector3 startPosition = originalParent.localPosition;

        float timeElapsed = 0f;

        // Realizar la interpolación hasta que el tiempo de transición se haya completado
        while (timeElapsed < transitionDuration)
        {
            // Interpolamos la posición de manera suave
            originalParent.localPosition = Vector3.Lerp(startPosition, targetPositionOffset, timeElapsed / transitionDuration);

            timeElapsed += Time.deltaTime;
            yield return null; // Esperamos hasta el siguiente frame
        }

        // Aseguramos que la posición final sea exactamente la targetPositionOffset
        originalParent.localPosition = targetPositionOffset;
    }
}