using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCrack : MonoBehaviour
{
    [SerializeField] GameObject initSprite = default;
    [SerializeField] GameObject damagedSprite = default;
    [SerializeField] DestructableWall parentWall = default;

    public bool isDamaged = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("PlayerThrowable") && !isDamaged)
        {
            initSprite.SetActive(false);
            damagedSprite.SetActive(true);
            parentWall.ProcessCrackHit();
        }
    }
}