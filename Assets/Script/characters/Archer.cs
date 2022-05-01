using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : PlayerCharacter
{
    protected override float findXTarget()
    {
        return Random.Range(validBounds.bounds.min.x, validBounds.bounds.max.x);
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
