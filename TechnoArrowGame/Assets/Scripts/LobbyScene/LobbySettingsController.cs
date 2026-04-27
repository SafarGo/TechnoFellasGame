using UnityEngine;

public class LobbySettingsController : MonoBehaviour
{
    public void SetHandName(string _name)
    {
        GameManager.instance.HandForInventory = _name;
    }
}
