using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    public string BusNumber;

    private void Awake()
    {
        instance = this;
    }
}
