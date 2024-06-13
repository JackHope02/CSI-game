using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class ClueManager : MonoBehaviour
{
    public static ClueManager Instance { get; private set; }

    public int totalClues; // Total number of clues in the scene
    private int cluesRevealed = 0; // Counter for how many clues have been revealed
    private int cluesCollected = 0; // Counter for how many clues have been collected

    public Text clueCounterText; // Reference to the UI Text that displays the clue count
    public GameObject completionMenu; // Reference to the completion menu panel
    public Text countdownText; // Reference to the text component where the countdown is shown
    public Text finalTimeText; // Reference to display the final time on the completion menu

    // Timer variables
    public Text gameplayTimerText; // Reference to the UI Text that displays the gameplay time
    private float elapsedTime = 0; // Tracks the elapsed time since the game started
    private bool isGameActive = true; // Tracks if the game is still active

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        if (completionMenu != null)
            completionMenu.SetActive(false);
    }

    void Start()
    {
        UpdateClueCounter();
    }

    void Update()
    {
        if (isGameActive)
        {
            elapsedTime += Time.deltaTime;
            UpdateGameplayTimer();
        }
    }

    // Called when a clue is revealed
    public void ClueRevealed()
    {
        cluesRevealed++;
        UpdateClueCounter();
    }

    // Called when a clue is actually collected
    public void ClueCollected()
    {
        if (allCluesRevealed)
        {
            cluesCollected++;
            UpdateClueCounter();

            if (allCluesCollected)
            {
                Debug.Log("All clues collected. Showing completion menu.");
                StartCoroutine(ShowCompletionMenu());
            }
        }
        else
        {
            Debug.Log("Not all clues revealed yet!");
        }
    }

    void UpdateClueCounter()
    {
        if (clueCounterText != null)
        {
            clueCounterText.text = "Revealed: " + cluesRevealed + "/" + totalClues + ", Collected: " + cluesCollected;
        }
    }

    void UpdateGameplayTimer()
    {
        int minutes = (int)(elapsedTime / 60);
        int seconds = (int)(elapsedTime % 60);
        int milliseconds = (int)((elapsedTime * 1000) % 1000);
        gameplayTimerText.text = string.Format("{0:00}:{1:00}.{2:000}", minutes, seconds, milliseconds);
    }

    public bool allCluesRevealed => cluesRevealed == totalClues;
    public bool allCluesCollected => cluesCollected == totalClues;

    IEnumerator ShowCompletionMenu()
    {
        isGameActive = false;

        if (completionMenu != null)
        {
            completionMenu.SetActive(true);

            if (finalTimeText != null)
            {
                finalTimeText.text = "Your Time: " + gameplayTimerText.text;
            }

            int countdown = 5;
            while (countdown > 0)
            {
                if (countdownText != null)
                {
                    countdownText.text = "Returning to Main Menu in " + countdown + "...";
                }
                yield return new WaitForSeconds(1);
                countdown--;
            }

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            SceneManager.LoadScene("MainMenu");
        }
    }
}
