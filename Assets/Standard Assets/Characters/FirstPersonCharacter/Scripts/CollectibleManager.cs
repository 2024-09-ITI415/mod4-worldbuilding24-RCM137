using UnityEngine;
using UnityEngine.UI; // To display the score

public class CollectibleManager : MonoBehaviour
{
    public int totalCollectibles = 17;  // Total number of collectibles
    private int collectiblesCollected = 0; // Track collected items
    public Text scoreText; // UI Text to display the score
    public GameObject endGamePanel; // Panel to show when all items are collected

    private void Start()
    {
        // Initialize the score display
        if (scoreText != null)
        {
            scoreText.text = "Collectibles: 0/" + totalCollectibles;  // Initial score display
        }
        else
        {
            Debug.LogError("ScoreText is not assigned in the Inspector!");
        }

        if (endGamePanel != null)
        {
            endGamePanel.SetActive(false); // Hide the end game panel at the start
        }
        else
        {
            Debug.LogError("EndGamePanel is not assigned in the Inspector!");
        }
    }

    // Call this method whenever a collectible is collected
    public void CollectItem()
    {
        collectiblesCollected++;  // Increment the collectibles counter

        // Update the score display
        UpdateScoreText();

        // Check if the player has collected all collectibles
        if (collectiblesCollected >= totalCollectibles)
        {
            EndGame(); // If all items are collected, end the game
        }
    }

    // Update the UI score text
    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Collectibles: " + collectiblesCollected + "/" + totalCollectibles;
        }
    }

    // Show the end game panel and stop the game
    private void EndGame()
    {
        if (endGamePanel != null)
        {
            endGamePanel.SetActive(true);  // Show the end game panel
        }
        Time.timeScale = 0;  // Pause the game
    }
}