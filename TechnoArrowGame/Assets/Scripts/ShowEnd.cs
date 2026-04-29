using System.Linq;
using UnityEngine;

public class ShowEnd : MonoBehaviour
{
    public static ShowEnd Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public void ShowEndScreen()
    {
        var end = Resources.FindObjectsOfTypeAll<Transform>()
            .FirstOrDefault(t => t.CompareTag("End"));
        if (end != null)
        {
            Debug.Log("[End] Found: " + end.name);
            end.gameObject.SetActive(true);
        }
        else
        {
            Debug.LogError("[End] NOT FOUND");
        }
    }
}