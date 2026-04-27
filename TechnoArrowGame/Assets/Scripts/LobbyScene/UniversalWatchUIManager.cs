using UnityEngine;

public class UniversalWatchUIManager : MonoBehaviour
{
    [Header("Настройки")]
    public GameObject inventoryObject;
    public GameObject Controller;
    public float showAngle = 40f;
    public float smoothSpeed = 8f;
    private Vector3 hiddenScale = Vector3.zero;
    private Vector3 visibleScale = Vector3.one;
    private Vector3 targetScale;
    private bool wasVisible = false;
    public static UniversalWatchUIManager instance;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        inventoryObject.transform.localScale = hiddenScale;
        targetScale = hiddenScale;
    }

    void Update()
    {
        float angle = Vector3.Angle(Controller.transform.forward, Vector3.up);

        bool shouldShow = angle > showAngle && angle <110;

        Debug.Log($"{angle}");

        if (shouldShow)
            targetScale = visibleScale;
        else
            targetScale = hiddenScale;

        
        inventoryObject.transform.localScale = Vector3.Lerp(inventoryObject.transform.localScale, targetScale, Time.deltaTime * smoothSpeed);
        if (shouldShow != wasVisible)
        {
            wasVisible = shouldShow;
            Collider[] colliders = inventoryObject.GetComponentsInChildren<Collider>();
            foreach (Collider col in colliders)
            {
                col.enabled = shouldShow;
            }
        }
    }
}