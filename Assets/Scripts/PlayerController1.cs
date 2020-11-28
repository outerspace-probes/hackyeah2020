using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class PlayerController1 : MonoBehaviour
{

    [SerializeField] int lives = 3;
    [SerializeField] float throwBallPower = 100f;
    [SerializeField] float throwPowerCost = 30f;
    [SerializeField] float throwPowerRegenerationSpeed = 150f;
    [SerializeField] Text UILivesField = default; 
    [SerializeField] RectTransform UIballPowerBar = default; 

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
    private bool isLeftDir = false;
    private float maxBallPower;

    private void Awake()
    {
        Application.targetFrameRate = 60;

        initPos = transform.position;
        maxBallPower = throwBallPower;
    }

    private void Update()
    {
        ProcessInput();
        animator.SetInteger("state", (int)state);
        RegenerateBallPower();
    }

    private void UpdateBallPower(float power)
    {
        throwBallPower = Mathf.Clamp(power, 0, maxBallPower);
        UIballPowerBar.localScale = new Vector3(throwBallPower / maxBallPower, 1f, 1f);
    }

    private void RegenerateBallPower()
    {
        UpdateBallPower(throwBallPower + (throwPowerRegenerationSpeed * Time.deltaTime));
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
        if(throwBallPower < throwPowerCost) { return; }

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
        UpdateBallPower(throwBallPower -= throwPowerCost);
    }

    private bool IsGrounded()
    {
        return coll.IsTouchingLayers(groundLayer);
    }
}