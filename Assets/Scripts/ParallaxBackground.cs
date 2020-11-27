using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    public Transform player;
    [Range(0, 1)] public float ratio = .5f;

    private void Update()
    {
        transform.position = new Vector3(player.position.x * ratio, player.position.y * ratio, transform.position.z);
    }
}
