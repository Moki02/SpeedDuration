using System.Collections;
using UnityEngine;

public class Collectibles : MonoBehaviour
{
    public bool isCoins, isSpeedUp;
    public int speedDuration;
    public float speedIncreaseAmount;
    private GameController gameController;

    private void Start()
    {
        gameController = FindObjectOfType<GameController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerControls pc = other.GetComponent<PlayerControls>();
            if (pc != null)
            {
                if (isCoins)
                {
                    gameController.AddScore(1);
                    Destroy(gameObject);
                }
                else if (isSpeedUp)
                {
                    pc.ApplySpeedBoost(speedIncreaseAmount, speedDuration);
                    Destroy(gameObject);
                }
            }
        }
    }
}
