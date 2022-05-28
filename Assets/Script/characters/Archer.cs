using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : PlayerCharacter
{
    public Arrow arrowPrefab;
    protected float AttackRange = 4;
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
            if (Vector2.Distance(enemyTarget.transform.position, transform.position) <= AttackRange)
            {
                return transform.position.x;
            }
            else if (transform.position.x > enemyTarget.transform.position.x)
            {
                return Mathf.Clamp(enemyTarget.transform.position.x + AttackRange, validBounds.bounds.min.x, validBounds.bounds.max.x);
            }
            else
            {
                return Mathf.Clamp(enemyTarget.transform.position.x - AttackRange, validBounds.bounds.min.x, validBounds.bounds.max.x);
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
            if (Mathf.Abs(enemy.transform.position.x-transform.position.x)<=7)
            {
                if (!closestEnemy || Vector2.Distance(enemy.transform.position, transform.position) < Vector2.Distance(closestEnemy.transform.position, transform.position))
                {
                    closestEnemy = enemy;
                }
            }

        }
        return closestEnemy;
    }

    public void ArcherDead()
    {
        if (!dead)
        {
            dead = true;
            GetComponent<Animator>().SetBool("dead", true);
        }
    }

    public Transform FireLoc;
    public void FireArrow()
    {
        Arrow arrow = Instantiate(arrowPrefab);
        arrow.transform.position = FireLoc.position;
        arrow.Fire(enemyTarget.transform.position);
    }
}
