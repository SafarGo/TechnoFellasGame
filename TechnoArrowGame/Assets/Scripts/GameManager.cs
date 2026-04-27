using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public string HandForInventory;

    private void Awake()
    {
        instance = this;
    }
}
