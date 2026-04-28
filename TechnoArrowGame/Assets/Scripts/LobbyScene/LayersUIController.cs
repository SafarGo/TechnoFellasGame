using UnityEngine;

public class LayersUICOntroller : MonoBehaviour
{
    void Awake()
    {
        Transform image = transform.Find("Image");
        Transform text = transform.Find("Text (TMP)");

        if (image != null) image.SetSiblingIndex(0);
        if (text != null) text.SetSiblingIndex(1);
    }
}
