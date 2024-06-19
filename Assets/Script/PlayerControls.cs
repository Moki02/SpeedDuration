using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    public float moveSpeed;
    public float rotSpeed;
    public Rigidbody rigidbody;
    public float jumpForce;
    public int score;
    private float originalSpeed;
    private bool isSpeedBoosted = false;
    public float boostAmount;
    public float duration;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        originalSpeed = moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        float translation = Input.GetAxis("Vertical") * moveSpeed;
        float rotation = Input.GetAxis("Horizontal") * rotSpeed;
        translation *= Time.deltaTime;
        rotation *= Time.deltaTime;
        transform.Translate(0,0,translation);
        transform.Rotate(0, rotation, 0);
        
        if(Input.GetButtonDown("Jump"))
        {
            rigidbody.AddForce(new Vector3 (0, jumpForce, 0), ForceMode.Impulse);
        }
    }
    public void ApplySpeedBoost(float boostAmount, float duration)
    {
        if (!isSpeedBoosted)
        {
            isSpeedBoosted = true;
            moveSpeed += boostAmount;
            StartCoroutine(ResetSpeedAfterDelay(boostAmount, duration));
        }
    }

    private IEnumerator ResetSpeedAfterDelay(float boostAmount, float duration)
    {
        yield return new WaitForSeconds(duration);

        if (isSpeedBoosted && Mathf.Approximately(moveSpeed, originalSpeed + boostAmount))
        {
            moveSpeed = originalSpeed;
            isSpeedBoosted = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SpeedBoost"))
        {
            Collectibles collectible = other.GetComponent<Collectibles>();
            if (collectible != null && collectible.isSpeedUp)
            {
                ApplySpeedBoost(2f, collectible.speedDuration);
                Destroy(other.gameObject);
            }
        }
    }

}
