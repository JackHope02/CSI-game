using UnityEngine;

public class Revealable : MonoBehaviour
{
    private Color originalColor;
    private Renderer objectRenderer;

    void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        originalColor = objectRenderer.material.color;
        DimObject();
    }

    void DimObject()
    {
        Color dimColor = originalColor * new Color(0.5f, 0.5f, 0.5f, 0.5f);
        objectRenderer.material.color = dimColor;
    }

    public void RevealObject()
    {
        objectRenderer.material.color = originalColor;
    }
}
