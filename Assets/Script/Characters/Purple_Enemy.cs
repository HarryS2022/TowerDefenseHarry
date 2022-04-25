using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Purple_Enemy : MonoBehaviour
{
    public int health= 100;
    public float speed = 1;
    public float xTarget;

    public bool Firing;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!Firing)
        {
            //anim.SetTrigger("idol");
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(xTarget, transform.position.y), speed * Time.deltaTime);
        }
        else
        {
            //anim.SetTrigger("fire");
        }

    }

    public bool dead()
    {
        if (health <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void checkdead()
    {
        if (dead())
        {
            GetComponent<Animator>().SetBool("Dead", true);
        }
    }
}
