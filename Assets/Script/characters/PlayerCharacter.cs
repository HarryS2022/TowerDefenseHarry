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

    public float AttackWaitTime = 2;
    protected float AttackWaitStarted = 0;
    protected bool AttackWaiting = false;

    [SerializeField] protected float findEnemyYBuffer = 0.5f;

    public int attackPower = 5;

    public enum PlayerStates
    {
        random,
        following,
        firing
    }
    public PlayerStates playerState;


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

        if (playerState == PlayerStates.following)
        {
            enemyTarget = findEnemyInRange();
            if (enemyTarget && Mathf.Abs(enemyTarget.transform.position.x - transform.position.x) <= Attackrange())
            {
                Debug.Log(gameObject.name + "set to playerState = PlayerStates.firing");
                playerState = PlayerStates.firing;
            }else if (enemyTarget)
            {
                Debug.Log(gameObject.name + " findEnemyInRange");
            }
        }

        if ((playerState == PlayerStates.firing || playerState == PlayerStates.following) && !findEnemyInRange())
        {
            playerState = PlayerStates.random;
        }

        if (playerState == PlayerStates.random)
        {
            anim.SetTrigger("idol");
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
        else if (playerState == PlayerStates.following)
        {
            anim.SetTrigger("idol");
            xTarget = findXTarget();
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(xTarget, transform.position.y), speed * Time.deltaTime);
        }
        else if (playerState == PlayerStates.firing)
        {
            if (AttackWaitStarted + AttackWaitTime <= Time.time)
            {
                anim.SetTrigger("fire");
                enemyTarget.SendMessage("TakeDamage", attackPower);
                AttackWaitStarted = Time.time;
            }
        }

        if (xTarget > transform.position.x) transform.localScale = new Vector3(-1, 1, 1);
        else if(xTarget < transform.position.x) transform.localScale = new Vector3(1, 1, 1);
    }

    protected abstract float findXTarget();
    protected abstract GameObject findEnemyInRange();
    protected abstract float Attackrange();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //take damage when hit by enemy attack
    }

    public virtual void TakeDamageAnim(int health)
    {
        
    }
}
