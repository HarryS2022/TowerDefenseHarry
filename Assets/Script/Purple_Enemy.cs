using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Purple_Enemy : MonoBehaviour
{
    public int health= 100;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
