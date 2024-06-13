using UnityEngine;
using UnityEngine.SceneManagement; // Needed for loading scenes
using UnityEngine.UI; // Needed for UI elements like Buttons

public class MainMenu : MonoBehaviour
{
    public Button startButton; // Reference to the start button
    public Button quitButton; // Reference to the quit button

    void Start()
    {
        // Add listeners to the buttons to respond to clicks
        startButton.onClick.AddListener(StartGame);
        quitButton.onClick.AddListener(QuitGame);
    }

    // Method to start the game
    void StartGame()
    {
        // Load the scene that starts your game. Replace "GameScene" with your game's starting scene name.
        // Make sure the scene is added to the build settings (File > Build Settings).
        SceneManager.LoadScene("GameScene");
    }

    // Method to quit the game
    void QuitGame()
    {
        // Log message to confirm quit (useful for debugging in the editor)
        Debug.Log("Quit game request");

        // Close the application (does nothing in the editor, but works in a built game)
        Application.Quit();
    }
}
