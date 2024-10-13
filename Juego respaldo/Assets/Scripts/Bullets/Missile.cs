using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : Bullet
{
    public Vector2 direction;
    void Update()
    {
        Movement();
    }

    public override void Movement()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision != null)
        {
            Debug.Log("Collided with: " + collision.gameObject.name);
            if (collision.gameObject.CompareTag("Enemy"))
            {
                gameManager.AddScore(3);
                Destroy(collision.gameObject);
                Destroy(this.gameObject);
            }
        }
    }
}
