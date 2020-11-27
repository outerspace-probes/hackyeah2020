using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowableItem : MonoBehaviour
{
    [SerializeField] public float initialSpeed = 2f;
    [SerializeField] public int bouncesToExplode = 8;
    [SerializeField] public Rigidbody2D rb = default;
    [SerializeField] BoxCollider2D boxCollider = default;
    [SerializeField] ParticleSystem bounceParticles = default;
    [SerializeField] ParticleSystem exploParticles = default;
    [SerializeField] GameObject spriteObj = default;

    private void Start()
    {
        bounceParticles.Stop();
        exploParticles.Stop();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("DestructableItem"))
        {
            Explode();
        }
        if(bouncesToExplode > 0)
        {
            bounceParticles.Play();
            bouncesToExplode--;
        }
        else
        {
            Explode();
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "DieCollider")
        {
            Destroy(gameObject);
        }
    }

    private void Explode()
    {
        spriteObj.SetActive(false);
        rb.simulated = false;
        boxCollider.enabled = false;
        exploParticles.Play();
        Invoke("SelfDestruct", 3f);
    }

    private void SelfDestruct()
    {
        Destroy(gameObject);
    }
}