using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCrack : MonoBehaviour
{
    [SerializeField] GameObject initSprite = default;
    [SerializeField] GameObject damagedSprite = default;
    [SerializeField] DestructableWall parentWall = default;
    [SerializeField] BoxCollider2D coll = default;

    public bool isDamaged = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("PlayerThrowable") && !isDamaged)
        {
            initSprite.SetActive(false);
            damagedSprite.SetActive(true);
            isDamaged = true;           
            parentWall.ProcessCrackHit();
            coll.enabled = false;
        }
    }
}