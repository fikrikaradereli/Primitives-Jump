using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Transform target;
    private readonly float cameraSpeed = 0.05f;
    private readonly float offsetY = 6.5f;

    void LateUpdate()
    {
        if (GameManager.Instance.CurrentGameState == GameState.RUNNING)
        {
            transform.position = Vector3.Slerp(transform.position, new Vector3(transform.position.x, target.position.y + offsetY, transform.position.z), cameraSpeed);
        }
    }
}
