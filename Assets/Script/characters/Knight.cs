using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : PlayerCharacter
{
    protected float AttackRange = 0.25F;
    protected override float Attackrange()
    {
        return AttackRange;
    }
    protected override float findXTarget()
    {
        if (playerState == PlayerStates.random)
        {
            return Random.Range(validBounds.bounds.min.x, validBounds.bounds.max.x);
        }
        else if (playerState == PlayerStates.following)
        {
            if (Mathf.Abs(enemyTarget.transform.position.x - transform.position.x) <= AttackRange)
            {
                return transform.position.x;
            }
            else if (transform.position.x > enemyTarget.transform.position.x)
            {
                return enemyTarget.transform.position.x + AttackRange;
            }
            else
            {
                return enemyTarget.transform.position.x - AttackRange;
            }

        }
        else
        {
            return 0;
        }

    }

    protected override GameObject findEnemyInRange()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject closestEnemy = null;
        foreach (GameObject enemy in enemies)
        {
            EnemyCharacter enemyChar = enemy.GetComponent<EnemyCharacter>();
            if (enemy.transform.position.x >= validBounds.bounds.min.x - enemyChar.width / 2 && enemy.transform.position.x <= validBounds.bounds.max.x + enemyChar.width / 2 &&
               enemy.transform.position.y >= validBounds.bounds.min.y - enemyChar.width / 2 && enemy.transform.position.y <= validBounds.bounds.max.y + enemyChar.width / 2)
            {
                if (!closestEnemy || Vector2.Distance(enemy.transform.position, transform.position) < Vector2.Distance(closestEnemy.transform.position, transform.position))
                {
                    closestEnemy = enemy;
                }
            }

        }
        return closestEnemy;
    }
    public void KnightDead()
    {
        if (!dead)
        {
            dead = true;
            GetComponent<Animator>().SetBool("dead", true);
        }
    }
}
