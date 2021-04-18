using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField]
    private Transform target;
    private float cameraSpeed = 0.05f;
    private float offsetY = 6.5f;

    // Update is called once per frame
    void LateUpdate()
    {
        if (GameManager.GetInstance().gameState == GameManager.GameState.Playing)
        {
            transform.position = Vector3.Slerp(transform.position, new Vector3(transform.position.x, target.position.y + offsetY, transform.position.z), cameraSpeed);
        }
    }
}
