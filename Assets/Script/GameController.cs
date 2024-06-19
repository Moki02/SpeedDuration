using TMPro;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI speedText;
    private int score;
    private PlayerControls playerControls;

    void Start()
    {
        score = 0;
        UpdateScoreText();
        playerControls = FindObjectOfType<PlayerControls>();
        
        if (playerControls == null)
        {
            Debug.LogError("PlayerControls component not found!");
        }
    }

    void Update()
    {
        if (playerControls != null)
        {
            UpdateSpeedText(playerControls.moveSpeed);
        }
    }

    public void AddScore(int value)
    {
        score += value;
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        scoreText.text = "Score: " + score;
    }
    
    private void UpdateSpeedText(float speed)
    {
        speedText.text = "Speed: " + speed.ToString("F2");
    }
}
