using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerCharacter : MonoBehaviour
//all it does is parent class, not object
{
    public Collider2D validBounds;
    protected bool dead = false;
    public float speed = 1;
    public float xTarget;
    protected GameObject enemyTarget;

    public bool Firing;
    protected Animator anim;//private but accessable by the parent class

    public float waitTime = 1;
    protected float waitStarted;
    protected bool waiting = false;

    public enum PlayerStates
    {
        random,
        following,
        firing
    }
    public PlayerStates playerState;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        xTarget = transform.position.x;
    }

    void Update()
    {   if (playerState != PlayerStates.following || playerState != PlayerStates.firing)
        {
            enemyTarget = findEnemyInRange();
            if (enemyTarget) playerState = PlayerStates.following;
        }

        if (playerState == PlayerStates.random)
        {
            if (waiting && waitStarted + waitTime < Time.time)
            {
                xTarget = findXTarget();
                waiting = false;
            }
            //anim.SetTrigger("idol");
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
        else if (playerState == PlayerStates.following)
        {
            xTarget = findXTarget();
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(xTarget, transform.position.y), speed * Time.deltaTime);
        }
        else if (playerState == PlayerStates.firing)
        {

        }

    }

    protected abstract float findXTarget();
    protected abstract GameObject findEnemyInRange();
}
