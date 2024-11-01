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
        Debug.Log(moveDirection);

        if (moveDirection != Vector2.zero)
        {
            newVelocity = rb.velocity + moveDirection * moveVelocity * Time.fixedDeltaTime;
        }
        else
        {
            if (Mathf.Abs(rb.velocity.x) < stopClamp.x)
            {
                newVelocity.x = 0;
            }

            if (Mathf.Abs(rb.velocity.y) < stopClamp.y)
            {
                newVelocity.y = 0;
            }
        }

        if (moveDirection.x == 0)
        {
            if (newVelocity.x > 0)
            {
                newVelocity.x = Mathf.MoveTowards(newVelocity.x, 0, GetFriction().x * Time.fixedDeltaTime);
            }
            else if (newVelocity.x < 0)
            {
                newVelocity.x = Mathf.MoveTowards(newVelocity.x, 0, GetFriction().x * Time.fixedDeltaTime);
            }
        }

        if (moveDirection.y == 0)
        {
            if (newVelocity.y > 0)
            {
                newVelocity.y = Mathf.MoveTowards(newVelocity.y, 0, GetFriction().y * Time.fixedDeltaTime);
            }
            else if (newVelocity.y < 0)
            {
                newVelocity.y = Mathf.MoveTowards(newVelocity.y, 0, GetFriction().y * Time.fixedDeltaTime);
            }
        }

        newVelocity.x = Mathf.Clamp(newVelocity.x, -maxSpeed.x, maxSpeed.x);
        newVelocity.y = Mathf.Clamp(newVelocity.y, -maxSpeed.y, maxSpeed.y);

        rb.velocity = newVelocity;
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
