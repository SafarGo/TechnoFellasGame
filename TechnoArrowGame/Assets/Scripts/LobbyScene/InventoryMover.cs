using UnityEngine;

public class InventoryMover : MonoBehaviour
{
    public Transform leftPoint;
    public Transform rightPoint;
    public GameObject inventory;
    public GameObject inventoryItem;
    public GameObject leftController;
    public GameObject rightController;

    public void MoveToLeft()
    {
        inventory.transform.SetParent(leftPoint);
        inventory.transform.localPosition = Vector3.zero;
        inventory.transform.localRotation = Quaternion.identity;
        inventoryItem.GetComponent<UniversalWatchUIManager>().Controller = leftController;
        inventory.transform.SetParent(leftController.transform);
    }

    public void MoveToRight()
    {
        inventory.transform.SetParent(rightPoint);
        inventory.transform.localPosition = Vector3.zero;
        inventory.transform.localRotation = Quaternion.identity;
        inventoryItem.GetComponent<UniversalWatchUIManager>().Controller = rightController;
        inventory.transform.SetParent(rightController.transform);
    }
}