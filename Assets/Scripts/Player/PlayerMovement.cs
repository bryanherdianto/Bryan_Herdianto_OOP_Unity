using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Vector2 maxSpeed;
    [SerializeField] private Vector2 timeToFullSpeed;
    [SerializeField] private Vector2 timeToStop;
    [SerializeField] private Vector2 stopClamp;

    private Vector2 moveDirection;
    private Vector2 moveVelocity;
    private Vector2 moveFriction;
    private Vector2 stopFriction;

    public Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        moveVelocity = 2 * maxSpeed / timeToFullSpeed;
        moveFriction = -2 * maxSpeed / (timeToFullSpeed * timeToFullSpeed);
        stopFriction = -2 * maxSpeed / (timeToStop * timeToStop);
    }

    public void Move()
    {
        Vector2 newVelocity = Vector2.zero;

        moveDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        if (moveDirection.x != 0)
        {
            newVelocity.x = moveDirection.x * moveVelocity.x * 0.5f + GetFriction().x * 0.01f;
        }
        else
        {
            newVelocity.x = moveDirection.x * moveVelocity.x * 0.5f;

            if (Mathf.Abs(newVelocity.x) < stopClamp.x)
            {
                newVelocity.x = 0;
            }
        }

        if (moveDirection.y != 0)
        {
            newVelocity.y = moveDirection.y * moveVelocity.y * 0.5f + GetFriction().y * 0.01f;
        }
        else
        {
            newVelocity.y = moveDirection.y * moveVelocity.y * 0.5f;

            if (Mathf.Abs(newVelocity.y) < stopClamp.y)
            {
                newVelocity.y = 0;
            }
        }

        newVelocity.x = Mathf.Clamp(newVelocity.x, -maxSpeed.x, maxSpeed.x);
        newVelocity.y = Mathf.Clamp(newVelocity.y, -maxSpeed.y, maxSpeed.y);

        rb.velocity = new Vector2(newVelocity.x, newVelocity.y);
    }

    public void MoveBound()
    {

    }

    public bool IsMoving()
    {
        return rb.velocity != Vector2.zero;
    }

    public Vector2 GetFriction()
    {
        if (moveDirection != Vector2.zero)
        {
            return moveFriction;
        }
        else
        {
            return stopFriction;
        }
    }
}
