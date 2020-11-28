using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    float yOffset = 0;

    private void Start()
    {
        yOffset = player.transform.position.y - transform.position.y;
    }

    private void Update()
    {
        transform.position = new Vector3(player.position.x, player.position.y - yOffset, transform.position.z);
    }
}