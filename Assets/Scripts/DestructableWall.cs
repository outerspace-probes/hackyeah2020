using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructableWall : MonoBehaviour
{
    [SerializeField] WallCrack[] childCracks = default;
    [SerializeField] AnimalController[] animalsBehind = default;
    [SerializeField] GameObject wallSprite = default;
    [SerializeField] GameObject explo = default;
    [SerializeField] bool isLastWall = false;

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
        foreach (AnimalController animal in animalsBehind)
        {
            animal.StartWalk();
            GameManager.instance.AddAnimal();
        }
        explo.SetActive(true);

        if (isLastWall)
        {
            GameManager.instance.ProcessWin();
        }
    }
}
