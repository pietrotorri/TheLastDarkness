using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float maxHealth, maxThirst, maxHunger;
    public float thirstIncreaseRate, hungerIncreaseRate;
    public float health, thirst, hunger;

    public bool dead;

    public void Start()
    {
        health = maxHealth;
    }

    public void Update()
    {
        if (!dead)
        {
            hunger += hungerIncreaseRate * Time.deltaTime;
            thirst += thirstIncreaseRate * Time.deltaTime;
        }

        if (thirst >= maxThirst)
            Die();
        if (hunger >= maxHunger)
            Die();
            
    }

    public void Die()
    {
        dead = true;
        print("YOU DIED");
    }
}
