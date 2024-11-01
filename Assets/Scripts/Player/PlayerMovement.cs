using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Vector2 maxSpeed;
    [SerializeField] Vector2 timeToFullSpeed;
    [SerializeField] Vector2 timeToStop;
    [SerializeField] Vector2 stopClamp;
    Vector2 moveDirection;
    Vector2 moveVelocity;
    Vector2 moveFriction;
    Vector2 stopFriction;
    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        moveVelocity = 2 * maxSpeed / timeToFullSpeed;
        moveFriction = -2 * maxSpeed / (timeToFullSpeed * timeToFullSpeed);
        stopFriction = -2 * maxSpeed / (timeToStop * timeToStop);
    }

    public void Move()
    {
        moveDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        if (moveDirection.magnitude > 1)
        {
            moveDirection.Normalize();
        }
        if (moveDirection.magnitude > 0)
        {
            rb.velocity += moveVelocity * moveDirection * Time.fixedDeltaTime;
        }
        else
        {
            if (rb.velocity.magnitude > stopClamp.magnitude)
            {
                rb.velocity += stopFriction * rb.velocity * Time.fixedDeltaTime;
            }
            else
            {
                rb.velocity = Vector2.zero;
            }
        }
    }

    Vector2 GetFriction()
    {
        if (rb.velocity != Vector2.zero)
        {
            return moveFriction;
        }
        else
        {
            return stopFriction;
        }
    }

    void MoveBound()
    {

    }

    public bool IsMoving()
    {
        return rb.velocity != Vector2.zero;
    }
}