using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField]
    public float cameraSpeed;
    [SerializeField]
    public GameObject player;

    private void LateUpdate()
    {
        Vector3 dir = player.transform.position - transform.position;
        Vector3 moveVector = new Vector3(dir.x * cameraSpeed * Time.deltaTime, (dir.y + 0.4f) * cameraSpeed * Time.deltaTime, 0.0f);
        transform.Translate(moveVector);
    }
}
