using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public Rigidbody2D rb;
    private float verticalvelocity;
    private float horizontalvelocity;
    public float dampening = 0.9f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(rb) transform.right = rb.velocity.normalized;
        if (transform.position.y < -5) Destroy(gameObject);
    }

    public void Fire(Vector2 targetPos)
    {
        //add in some randomness and adjuct for range

        float dx = transform.position.x - targetPos.x;
        float dy = transform.position.y - targetPos.y;

        float Vx = Mathf.Sqrt(Mathf.Abs((dx * dx * (9.81f)) / (Mathf.Abs(dy) + Mathf.Abs(dx))));
        Debug.Log($"dx = {dx} dy = {dy} Vx = {Vx}");

        if (transform.position.x > targetPos.x)
            rb.velocity = (new Vector2(-1, 1)) * Vx * dampening * Random.Range(0.97f, 1.03f);
        else
        {
            rb.velocity = (new Vector2(1, 1)) * Vx * dampening * Random.Range(0.97f, 1.03f);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Destroy(rb);
            transform.parent = collision.transform;
            collision.SendMessage("hit");
        }
    }
}
