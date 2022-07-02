using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyCharacter : MonoBehaviour
{
    protected float xTarget = 9;
    protected Animator anim;
    public float width;

    public int Greenenemyattackpower = 8;
    public float GAttackWaitTime = 2;
    protected float GAttackWaitStarted = 0;
    protected bool GAttackWaiting = false;

    public enum EnemyStates
    {
        invading,
        firing,
        following,
    }

    public EnemyStates enemyState;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        xTarget = 9;

        if (findPlayerInRange() == null || Mathf.Abs(findPlayerInRange().transform.position.y - transform.position.y) > 1
)
        {
            enemyState = EnemyStates.invading;
        }
        else if (Vector2.Distance(findPlayerInRange().transform.position, transform.position) > attackRange())
        {
            enemyState = EnemyStates.following;
        }
        else
        {
            enemyState = EnemyStates.firing;
        }
        
        if (enemyState == EnemyStates.invading)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(xTarget, transform.position.y), enemyspeed() * Time.deltaTime);
            anim.SetTrigger("walk");
        }
        
        if (enemyState == EnemyStates.following)
        {
            anim.SetTrigger("walk");
            xTarget = findPlayerInRange().transform.position.x;
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(xTarget, transform.position.y), enemyspeed() * Time.deltaTime);
        }

        if (enemyState == EnemyStates.firing)
        {
            if (GAttackWaitStarted + GAttackWaitTime <= Time.time)
            {
                anim.SetTrigger("attack");
                findPlayerInRange().SendMessage("TakeDamage", Greenenemyattackpower);
                GAttackWaitStarted = Time.time;
            }
        }
    }

    protected abstract float enemyspeed();
    protected abstract GameObject findPlayerInRange();
    protected abstract float attackRange();

    public virtual void hit()
    {
        anim.SetTrigger("hit");
    }

    public virtual void youdied(GameObject a)
    {
        GetComponent<Animator>().SetBool("dead", true);
        anim.SetTrigger("hit");
        Destroy(a);
    }

    public virtual void TakeDamageAnim(int health)
    {

    }
}
