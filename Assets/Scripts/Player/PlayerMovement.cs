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
    Vector2 minScreenBounds;
    Vector2 maxScreenBounds;
    float objectWidth;
    float objectHeight;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = transform.Find("Ship").GetComponent<SpriteRenderer>();

        rb = GetComponent<Rigidbody2D>();
        moveVelocity = 2 * maxSpeed / timeToFullSpeed;
        moveFriction = -2 * maxSpeed / (timeToFullSpeed * timeToFullSpeed);
        stopFriction = -2 * maxSpeed / (timeToStop * timeToStop);

        Camera mainCamera = Camera.main;
        float cameraHeight = 2f * mainCamera.orthographicSize;
        float cameraWidth = cameraHeight * mainCamera.aspect;

        minScreenBounds = mainCamera.transform.position - new Vector3(cameraWidth / 2, cameraHeight / 2);
        maxScreenBounds = mainCamera.transform.position + new Vector3(cameraWidth / 2, cameraHeight / 2);

        objectWidth = spriteRenderer.bounds.size.x;
        objectHeight = spriteRenderer.bounds.size.y;
    }

    public void Move()
    {
        moveDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        moveVelocity -= GetFriction() * Time.deltaTime;

        moveVelocity.x = Mathf.Clamp(moveDirection.x * maxSpeed.x, -maxSpeed.x, maxSpeed.x);
        moveVelocity.y = Mathf.Clamp(moveDirection.y * maxSpeed.y, -maxSpeed.y, maxSpeed.y);

        if (moveVelocity.magnitude < stopClamp.magnitude)
        {
            rb.velocity = Vector2.zero;
        }

        rb.velocity = moveVelocity;
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

    public void MoveBound()
    {
        Vector2 currentPosition = transform.position;
        float clampedX = Mathf.Clamp(currentPosition.x, minScreenBounds.x + objectWidth, maxScreenBounds.x - objectWidth);
        float clampedY = Mathf.Clamp(currentPosition.y, minScreenBounds.y + objectHeight, maxScreenBounds.y - objectHeight);
        transform.position = new Vector2(clampedX, clampedY);
    }

    public bool IsMoving()
    {
        return rb.velocity != Vector2.zero;
    }
}