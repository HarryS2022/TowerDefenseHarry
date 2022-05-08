using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : PlayerCharacter
{
    protected override float findXTarget()
    {
        return Random.Range(validBounds.bounds.min.x, validBounds.bounds.max.x);
    }

    protected override GameObject findEnemyInRange()
    {
        throw new System.NotImplementedException();
    }

    public void ArcherDead()
    {
        if (!dead)
        {
            dead = true;
            GetComponent<Animator>().SetBool("dead", true);
        }
    }


}
