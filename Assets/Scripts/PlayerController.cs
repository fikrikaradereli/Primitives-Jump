using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    private readonly float jumpForce = 5f;
    [SerializeField]
    private bool isOnGround = true;

    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && isOnGround && GameManager.Instance.CurrentGameState == GameState.RUNNING)
            {
                playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                isOnGround = false;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space) && isOnGround && GameManager.Instance.CurrentGameState == GameState.RUNNING)
            {
                playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                isOnGround = false;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        isOnGround = true;
    }
}