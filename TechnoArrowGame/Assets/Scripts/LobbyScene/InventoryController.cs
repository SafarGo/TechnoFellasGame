using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using static UnityEngine.Rendering.GPUSort;

public class InventoryController : MonoBehaviour
{
    public GameObject Parent;
    public void PlugInInventory(SelectEnterEventArgs args)
    {
        GameObject obj = args.interactableObject.transform.gameObject;

        obj.transform.SetParent(Parent.transform);
    }

    public void SetOutOfInventory(SelectExitEventArgs args)
    {
        GameObject obj = args.interactableObject.transform.gameObject;
        obj.transform.parent = null;
    }
}
