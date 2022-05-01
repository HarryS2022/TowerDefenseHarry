using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerCharacter : MonoBehaviour
{
    protected bool dead = false;
    public float speed = 1;
    public float xTarget;

    public bool Firing;
    protected Animator anim;

    public float waitTime = 1;
    protected float waitStarted;
    protected bool waiting = false;
    public Collider2D validBounds;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        xTarget = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (!Firing)
        {
            if (waiting && waitStarted + waitTime < Time.time)
            {
                xTarget = findXTarget();
                waiting = false;
            }
            if (!waiting && Mathf.Abs(xTarget - transform.position.x) < speed * Time.deltaTime)
            {
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(xTarget, transform.position.y), speed * Time.deltaTime);
                waitStarted = Time.time;
                waiting = true;
            }
            else if (!waiting)
            {
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(xTarget, transform.position.y), speed * Time.deltaTime);
            }

        }
        else
        {
            //anim.SetTrigger("fire");
        }

    }

    protected abstract float findXTarget();
}
