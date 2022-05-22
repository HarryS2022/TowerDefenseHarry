using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int health = 100;
    private int maxHealth;
    private float healthBarLength;
    [SerializeField] protected Transform healthBar;

    // Start is called before the first frame update
    void Start()
    {
        maxHealth = health;
        healthBarLength = healthBar.localScale.x;
    }

    public virtual void TakeDamage(int damage)
    {
        health = Mathf.Clamp(health - damage, 0, maxHealth);
        Vector3 localScale = healthBar.localScale;
        healthBar.localScale = new Vector3(healthBarLength * health / maxHealth, localScale.y, localScale.z);
        gameObject.SendMessage("TakeDamageAnim", health);
    }

}
