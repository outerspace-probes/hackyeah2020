using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController1 : MonoBehaviour
{
    [SerializeField] [Range(2f, 20f)]  float movingSpeed = 5f;

    [SerializeField] LayerMask groundLayer = default;
    [SerializeField] Rigidbody2D rb = default;
    [SerializeField] Animator animator = default;
    [SerializeField] Collider2D coll = default;
    [SerializeField] Transform throwSpawnPoint = default;
    [SerializeField] GameObject onionWeaponPrefab = default;
    [SerializeField] GameObject spawnedObjsParent = default;

    private enum State { idle, run }
    [SerializeField] private State state = State.idle;

    private Vector3 initPos;
    private GameObject throwObjHandler;
    private Rigidbody2D throwRbHandler;
    private ThrowableItem throwableItemHandler;
    [SerializeField] private bool isLeftDir = false;

    private void Awake()
    {
        Application.targetFrameRate = 60;

        initPos = transform.position;
    }

    private void Update()
    {
        ProcessInput();
        animator.SetInteger("state", (int)state);
    }

    private void ProcessInput()
    {
        float hDirection = Input.GetAxis("Horizontal");

        if (hDirection < 0) // move left
        {
            rb.velocity = new Vector2(-movingSpeed, rb.velocity.y);
            transform.localScale = new Vector2(-1, 1);
            isLeftDir = true;
            state = State.run;
        }

        else if (hDirection > 0) // move right
        {
            rb.velocity = new Vector2(movingSpeed, rb.velocity.y);
            transform.localScale = new Vector2(1, 1);
            isLeftDir = false;
            state = State.run;
        }

        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            state = State.idle;
        }

        if (Input.GetButtonDown("Fire1"))
        {
            Throw();
        }
    }

    public void Throw()
    {
        throwObjHandler = Instantiate(onionWeaponPrefab, throwSpawnPoint.position, Quaternion.identity);
        throwObjHandler.transform.parent = spawnedObjsParent.transform;
        throwableItemHandler = throwObjHandler.GetComponent<ThrowableItem>();
        throwRbHandler = throwableItemHandler.rb;
        Vector2 velocity;

        if(isLeftDir)
        {
            velocity = new Vector2(-throwableItemHandler.initialSpeed, -throwableItemHandler.initialSpeed);
        }
        else
        {
            velocity = new Vector2(throwableItemHandler.initialSpeed, -throwableItemHandler.initialSpeed);
        }
        throwRbHandler.velocity = velocity;
    }

    private bool IsGrounded()
    {
        return coll.IsTouchingLayers(groundLayer);
    }
}