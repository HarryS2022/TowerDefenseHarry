using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public Rigidbody2D rb;
    private float verticalvelocity;
    private float horizontalvelocity;

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
        horizontalvelocity = Mathf.Abs(transform.position.x - targetPos.x) / 2 * Mathf.Sqrt((float)(2.5 /0.5));
        verticalvelocity = 10 / Mathf.Sqrt((float)(2.5 /0.5));
        if (transform.position.x > targetPos.x)
            rb.velocity = (new Vector2(-1, 1)).normalized * Mathf.Sqrt((Mathf.Pow(horizontalvelocity, 2) + Mathf.Pow(verticalvelocity, 2)));
        else
            rb.velocity = (new Vector2(1, 1)).normalized * Mathf.Sqrt((Mathf.Pow(horizontalvelocity, 2) + Mathf.Pow(verticalvelocity, 2)));
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
