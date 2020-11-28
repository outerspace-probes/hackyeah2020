using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalController : MonoBehaviour
{
    private enum State { idle, run }
    [SerializeField] private State state = State.idle;
    [SerializeField] Animator animator = default;

    float runSpeed;

    private void Awake()
    {
        runSpeed = Random.Range(6.5f, 7.5f);
    }

    private void Start()
    {
        animator.SetInteger("state", (int)state);
    }

    private void Update()
    {
        if(state == State.run)
        {
            transform.position = new Vector3(transform.position.x - runSpeed * Time.deltaTime, transform.position.y, transform.position.z);
        }
    }

    public void StartWalk()
    {
        state = State.run;
        animator.SetInteger("state", (int)state);
        Invoke("SelfDestruct", 9f);
    }

    private void SelfDestruct()
    {
        Destroy(gameObject);
    }
}
