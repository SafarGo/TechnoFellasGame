using UnityEngine;

public class ObjectScaler : MonoBehaviour
{
    public Transform player;
    public float maxDistance = 5f;
    public float minDistance = 1f;
    public float maxScale = 2f;
    public float minScale = 0.1f;

    void Update()
    {
        if (player == null) return;
        float distance = Vector3.Distance(transform.position, player.position);
        float t = 1f - (distance - minDistance) / (maxDistance - minDistance);
        t = Mathf.Clamp01(t);
        float scale = Mathf.Lerp(minScale, maxScale, t);
        transform.localScale = new Vector3(scale, scale, scale);
        transform.LookAt(player);
        transform.Rotate(0, 180, 0);
    }

}
