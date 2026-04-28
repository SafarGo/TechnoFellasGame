using UnityEngine;

public class DoorTeleport : MonoBehaviour
{
    
    public void Teleport(Transform _point)
    {
        GameObject player = GameObject.Find("XR Origin (XR Rig) (1)");
        player.transform.position = _point.position;
    }
}
