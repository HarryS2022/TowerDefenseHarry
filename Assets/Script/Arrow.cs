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

        float dx = Mathf.Abs(transform.position.x - targetPos.x);
        float dy = Mathf.Abs(transform.position.y - targetPos.y);
        

        if(dy < 0)
        {

        }
        else
        {
            if (transform.position.x <= targetPos.x && (-dy + Mathf.Sqrt((float)(dy * dy + 2 * 9.81 * dx))) / 2 > 0){
                horizontalvelocity = (-9.81f*dy + Mathf.Sqrt((float)(96.2361f * dy * 96.2361f * dy + 1888.15f * dx))) / 19.62f;
            }
            else
            {
                horizontalvelocity = (-dy - Mathf.Sqrt((float)(dy * dy + 2 * 9.81 * dx))) / 2;
            }
            
            float velocity = Mathf.Sqrt(Mathf.Pow(horizontalvelocity, 2) * 2);
            rb.velocity = (new Vector2(-1, 1)).normalized * velocity;
        }

        //if (transform.position.x > targetPos.x)
            //rb.velocity = (new Vector2(-1, 1)).normalized * velocity;
        //else
        //    rb.velocity = (new Vector2(1, 1)).normalized * Mathf.Sqrt((Mathf.Pow(horizontalvelocity, 2) + Mathf.Pow(verticalvelocity, 2)));
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
