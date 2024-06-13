using UnityEngine;
using UnityEngine.UI;

public class Hotbar : MonoBehaviour
{
    [Header("Inventory")]
    public Item[] items; // Items to be displayed on the hotbar
    public Image[] itemImages; // UI Images corresponding to each hotbar slot
    public GameObject[] highlights; // Highlights corresponding to each slot

    [Header("Interaction")]
    public GameObject flashlight; // Reference to the Flashlight GameObject

    private int selectedItemIndex = -1; // -1 means no item is selected initially

    void Start()
    {
        InitializeHotbar(); // Initialize hotbar slots based on the items
        flashlight.SetActive(false); // Ensure flashlight is initially off
    }

    void InitializeHotbar()
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] != null)
            {
                itemImages[i].sprite = items[i].icon; // Set the sprite for the item
                itemImages[i].enabled = true; // Enable the item image UI
            }
            else
            {
                itemImages[i].enabled = false; // Disable the item image UI
            }
            highlights[i].SetActive(false); // Initially, no slot is highlighted
        }
    }

    void TryRevealClue()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.CompareTag("Clue"))
            {
                RevealClues revealScript = hit.collider.GetComponent<RevealClues>();
                if (revealScript != null && !revealScript.isRevealed)
                {
                    revealScript.RevealMaterial(); // Reveal the clue material
                    ClueManager.Instance.ClueRevealed(); // Inform ClueManager that a clue has been revealed
                    Debug.Log("Clue has been revealed!");
                }
                else
                {
                    Debug.Log("Clue is already revealed or no reveal script attached.");
                }
            }
            else
            {
                Debug.Log("Hit object is not tagged as Clue.");
            }
        }
        else
        {
            Debug.Log("No object hit by ray.");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) SelectItem(0);
        if (Input.GetKeyDown(KeyCode.Alpha2)) SelectItem(1);
        if (Input.GetKeyDown(KeyCode.Alpha3)) SelectItem(2);

        if (selectedItemIndex == 0 && Input.GetMouseButtonDown(0))
        {
            TryRevealClue(); // Attempt to reveal a clue if the first item is selected
        }
        else if (selectedItemIndex == 1 && Input.GetMouseButtonDown(0))
        {
            TryPickupClue(); // Attempt to pick up a clue if the second item is selected
        }

        UpdateSelectedItem(); // Update the flashlight based on the selected item
    }

    public void UpdateSelectedItem()
    {
        Debug.Log("Current Selected Item Index: " + selectedItemIndex);
        if (selectedItemIndex == 2)
        {
            flashlight.SetActive(true); // Activate flashlight if the third item is selected
        }
        else
        {
            flashlight.SetActive(false); // Deactivate flashlight otherwise
        }
    }

    public void SelectItem(int index)
    {
        if (index >= 0 && index < items.Length)
        {
            if (selectedItemIndex != -1)
            {
                highlights[selectedItemIndex].SetActive(false); // Remove highlight from previously selected item
            }

            selectedItemIndex = index; // Update the selected item index
            highlights[selectedItemIndex].SetActive(true); // Highlight the newly selected item

            Debug.Log("Selected item: " + items[index].itemName);
        }
    }

    void TryPickupClue()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            var revealScript = hit.collider.GetComponent<RevealClues>();
            if (hit.collider.CompareTag("Clue") && revealScript && revealScript.isRevealed && ClueManager.Instance.allCluesRevealed)
            {
                ClueManager.Instance.ClueCollected(); // Inform the ClueManager that a clue was collected
                Destroy(hit.collider.gameObject); // Remove the clue from the scene
                Debug.Log("Clue picked up using the Evidence Bag.");
            }
            else
            {
                Debug.Log("Clue cannot be picked up yet, or it's not revealed.");
            }
        }
    }
}
