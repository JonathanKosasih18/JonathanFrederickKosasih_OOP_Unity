using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float rotateSpeed;
    Vector2 newPosition;
    private Vector2 velocity = new Vector2(2f, 2f);
    private float minX = -8f;
    private float maxX = 8f;
    private float minY = -4f;
    private float maxY = 4f;
    void Start()
    {
        Move();
        ChangePosition();
    }

    void Update()
    {
        Move();
        transform.Rotate(Vector3.forward * rotateSpeed * Time.deltaTime);
        Player player = FindObjectOfType<Player>();
        Weapon playerWeapon = player.GetComponentInChildren<Weapon>();

        if (playerWeapon == null)
        {
            gameObject.SetActive(false);
            GetComponent<Collider2D>().enabled = false;
        }
        else
        {
            gameObject.SetActive(true);
            GetComponent<Collider2D>().enabled = true;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.LevelManager.LoadScene("Main");
        }
    }

    void ChangePosition()
    {
        newPosition = new Vector2(Random.Range(-8, 8), Random.Range(-4, 4));
    }

    private void Move()
    {
        Vector2 position = transform.position;
        position += velocity * Time.deltaTime;
        transform.position = position;
        MoveBound();
    }

    private void MoveBound()
    {
        Vector2 position = transform.position;

        if (position.x < minX || position.x > maxX)
        {
            velocity.x = -velocity.x;
            position.x = Mathf.Clamp(position.x, minX, maxX);
        }

        if (position.y < minY || position.y > maxY)
        {
            velocity.y = -velocity.y;
            position.y = Mathf.Clamp(position.y, minY, maxY);
        }

        transform.position = position;
    }
}
