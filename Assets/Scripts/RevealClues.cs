using UnityEngine;

public class RevealClues : MonoBehaviour
{
    public Material hiddenMaterial;
    public Material revealedMaterial;

    private Renderer objectRenderer;
    public bool isRevealed = false;

    void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        objectRenderer.material = hiddenMaterial;
    }

    public void RevealMaterial()
    {
        if (!isRevealed)
        {
            objectRenderer.material = revealedMaterial;
            isRevealed = true;
            // Don't call ClueManager.Instance.ClueFound(); here
        }
    }
}
