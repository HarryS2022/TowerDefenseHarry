using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Green_Enemy : EnemyCharacter
{
    public int health = 100;

    protected float findrange = 4;
    protected float attackrange = 2;

    public float speed = 1;

    public bool Firing;

    protected override float enemyspeed()
    {
        return speed;
    }

    protected override float attackRange()
    {
        return attackrange;
    }

    protected override GameObject findPlayerInRange()
    {  
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        GameObject closestPlayer = null;
        foreach (GameObject player in players)
        {
            if (Vector2.Distance(player.transform.position, transform.position) <= findrange)
            {
                if (!closestPlayer || Vector2.Distance(player.transform.position, transform.position) < Vector2.Distance(closestPlayer.transform.position, transform.position))
                {
                    closestPlayer = player;
                }
            }
        }
        if (!closestPlayer) return null;
        return closestPlayer;
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
            GetComponent<Animator>().SetBool("dead", true);
        }
    }
}
