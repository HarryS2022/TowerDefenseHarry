using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lancer : PlayerCharacter
{
    
    protected override float findXTarget()
    {
        if(playerState == PlayerStates.random)
        {
            return Random.Range(validBounds.bounds.min.x, validBounds.bounds.max.x);
        }else if(playerState == PlayerStates.following)
        {
            return enemyTarget.transform.position.x;
        }
        else
        {
            return 0;
        }
        
    }


    public void LancerDead()
    {
        if (!dead)
        {
            dead = true;
            GetComponent<Animator>().SetBool("dead", true);
        }
    }

    protected override GameObject findEnemyInRange()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject closestEnemy = null;
        foreach(GameObject enemy in enemies)
        {
            if(enemy.transform.position.x >= validBounds.bounds.min.x && enemy.transform.position.x <= validBounds.bounds.max.x &&
               enemy.transform.position.y >= validBounds.bounds.min.y && enemy.transform.position.y <= validBounds.bounds.max.y)
            {
                if(!closestEnemy || Vector2.Distance(enemy.transform.position, transform.position) < Vector2.Distance(closestEnemy.transform.position, transform.position))
                {
                    closestEnemy = enemy;
                }

            }
        }
        return closestEnemy;
    }
}
