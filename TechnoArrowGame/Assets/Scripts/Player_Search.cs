using UnityEngine;

public class Player_Search : MonoBehaviour
{
    public GameObject obj;

    private void Start()
    {
        if (obj == null)
        {
            obj = GameObject.Find("End");
        }

        if (obj != null)
        {
            obj.SetActive(false);
        }
        else
        {
            Debug.LogWarning("Объект с именем End не найден!");
        }
    }

    public void Search()
    {
        if (obj == null)
        {
            obj = GameObject.Find("End");
        }

        if (obj != null)
        {
            obj.SetActive(true);
        }
        else
        {
            Debug.LogWarning("Объект с именем End не найден!");
        }
    }
}