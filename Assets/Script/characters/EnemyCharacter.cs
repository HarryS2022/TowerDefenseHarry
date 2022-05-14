using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyCharacter : MonoBehaviour
{
    protected float xTarget = 9;
    protected Animator anim;
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

        if (findPlayerInRange() == null)
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
            anim.SetTrigger("attack");
        }
    }

    protected abstract float enemyspeed();
    protected abstract GameObject findPlayerInRange();
    protected abstract float attackRange();
}
