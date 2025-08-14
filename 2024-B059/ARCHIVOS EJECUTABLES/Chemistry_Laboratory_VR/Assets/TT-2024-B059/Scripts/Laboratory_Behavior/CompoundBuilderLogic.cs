using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CompoundBuilderLogic : MonoBehaviour
{
    [SerializeField] private string targetTag = "Isotopo"; // Tag del objeto que debe ser destruido al colisionar
    [SerializeField] private Compounds_List compoundsList; // Referencia a la lista de compuestos
    private List<string> collidedObjects = new List<string>(); // Lista para almacenar los nombres de los objetos que colisionan
    [SerializeField] private UI_Compounds_Manager uiCompoundsManager;
    [SerializeField] private UI_Info_Compounds_Manager uiInfoCompoundsManager;
    private CompoundData compoundToCreate;

    private void OnTriggerEnter(Collider other)
    {
        // Verificar si el objeto que colisiona tiene el tag especificado
        if (other.gameObject.CompareTag(targetTag))
        {
            // Añadir el nombre del objeto a la lista si no está ya presente
            string objectName = other.gameObject.name;
            if (!collidedObjects.Contains(objectName))
            {
                collidedObjects.Add(objectName);
                // Llamar a CheckCompounds para verificar si se puede formar un compuesto
                compoundToCreate = CanFormCompound();
                uiInfoCompoundsManager.UpdateElements(collidedObjects, compoundToCreate);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Verificar si el objeto que salió tiene el tag especificado
        if (other.gameObject.CompareTag(targetTag))
        {
            // Quitar el nombre del objeto de la lista si está presente
            string objectName = other.gameObject.name;
            if (collidedObjects.Contains(objectName))
            {
                collidedObjects.Remove(objectName);
                compoundToCreate = CanFormCompound();
                uiInfoCompoundsManager.UpdateElements(collidedObjects, compoundToCreate);
            }
        }
    }

    private CompoundData CanFormCompound()
    {
        // Recorre cada CompoundData en la lista de compuestos
        foreach (CompoundData compoundData in compoundsList.GetCompounds())
        {
            // Obtén la lista de nombres de componentes requeridos (sin importar cantidades)
            List<string> componentNames = compoundData.getcomponentNames.Distinct().ToList(); // Usamos Distinct() para eliminar duplicados

            // Verificar si todos los objetos colisionados están presentes en la lista de componentes del compuesto
            bool allCollidedPresent = collidedObjects.All(collided => 
                componentNames.Any(component => collided.StartsWith(component)));

            // Verificar si todos los componentes del compuesto están presentes en los objetos colisionados
            bool allComponentsPresent = componentNames.All(component => 
                collidedObjects.Any(collided => collided.StartsWith(component)));

            // Si ambas condiciones son verdaderas, devolver el CompoundData correspondiente
            if (allCollidedPresent && allComponentsPresent)
            {
                return compoundData; // Devolver el compuesto que puede ser creado
            }
        }
        return null; // Si no se encontró ningún compuesto, devolver null
    }



    private void CreateCompound()
    {
        // Destruir los objetos que coinciden con los componentes (independiente de las cantidades)
        List<string> componentNames = compoundToCreate.getcomponentNames.Distinct().ToList();

        foreach (string componentName in componentNames)
        {
            // Encuentra el objeto cuyo nombre comienza con el componente
            GameObject objectToDestroy = collidedObjects
                .Select(objName => GameObject.Find(objName))
                .FirstOrDefault(obj => obj != null && obj.name.StartsWith(componentName));

            if (objectToDestroy != null)
            {
                // Eliminar el objeto
                Destroy(objectToDestroy);

                // Quitar el nombre del objeto de la lista de colisiones
                collidedObjects.Remove(objectToDestroy.name);
            }
        }

        // Enviar el compuesto formado a la UI
        SendCompounds(compoundToCreate);

        // Instanciar el resultado del compuesto en la escena
        Instantiate(compoundToCreate.getresult, transform.position, compoundToCreate.getresult.transform.rotation);
        
        validar(compoundToCreate);

        // Registrar el nuevo compuesto en el manager de cantidades
        CompoundQuantityManager manager = FindObjectOfType<CompoundQuantityManager>();
        if (manager != null)
        {
            manager.RegisterCompound(compoundToCreate.getresult);
        }

        compoundToCreate = null;
    }


    public void CheckCompounds()
    {
        // Si se encontró un compuesto válido, crearlo
        if (compoundToCreate != null)
        {
            CreateCompound();
            uiInfoCompoundsManager.UpdateElements(collidedObjects, compoundToCreate);
        }
    }

    public void SendCompounds(CompoundData compoundData) // Nueva función para enviar Elemento al UI_Elements_Manager
    {
        if (uiCompoundsManager != null)
        {
            uiCompoundsManager.ReceiveCompound(compoundData);
        }
    }

    public void validar(CompoundData compound)
    {
        if(CompoundManager.Instance != null)
        {
            //CompoundManager.Instance.IncrementCompoundCount(compound.getformula);
            //CompoundManager.Instance.ValidateCompounds();
            CompoundManager.Instance.changePhase(compoundToCreate);
        }
    }
}

