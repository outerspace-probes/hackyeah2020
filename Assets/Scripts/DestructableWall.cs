using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructableWall : MonoBehaviour
{
    [SerializeField] WallCrack[] childCracks = default;
    [SerializeField] GameObject wallSprite = default;
    [SerializeField] GameObject explo = default;

    public void ProcessCrackHit()
    {
        foreach(WallCrack crack in childCracks)
        {
            if(!crack.isDamaged) { return; }
        }
        Explode();
    }

    private void Explode()
    {
        wallSprite.SetActive(false);
        foreach (WallCrack crack in childCracks)
        {
            crack.gameObject.SetActive(false);
        }
        explo.SetActive(true);
    }
}
