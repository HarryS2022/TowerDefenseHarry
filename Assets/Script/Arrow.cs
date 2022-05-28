using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public Rigidbody2D rb;
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
        //add in some randomness and adjust for range
        if(transform.position.x > targetPos.x)
            rb.velocity = (new Vector2(-1, 1)).normalized * 5;
        else
            rb.velocity = (new Vector2(1, 1)).normalized * 5;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Destroy(rb);
            transform.parent = collision.transform;
        }
    }
}
