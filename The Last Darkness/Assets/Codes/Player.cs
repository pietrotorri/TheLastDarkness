using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float maxHealth, maxThirst, maxHunger;
    public float thirstIncreaseRate, hungerIncreaseRate;
    private float health, thirst, hunger;
    public bool dead;

    public float damage;

    public static bool triggeringWithAI;
    public static GameObject triggeringAI;

    public void Start()
    {
        health = maxHealth;
    }

    public void Update()
    {
        // thirst and hunger Increase
        if (!dead)
        {
            hunger += hungerIncreaseRate * Time.deltaTime;
            thirst += thirstIncreaseRate * Time.deltaTime;
        }

        if (thirst >= maxThirst)
            Die();
        if (hunger >= maxHunger)
            Die();

        // detecting and killing AI
        if (triggeringWithAI == true && triggeringAI)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Attack(triggeringAI);
            }
        }
        if (!triggeringAI)
            triggeringWithAI = false;
    }

    public void Attack(GameObject target)
    {
        if(target.tag == "Animal")
        {
            Animal animal = target.GetComponent<Animal>();
            animal.health -= damage;
        }
    }

    public void Die()
    {
        dead = true;
        print("YOU DIED");
    }

    public void Drink(float decreaseRate)
    {
        thirst -= decreaseRate;
    }
    public void Eat(float decreaseRate)
    {
        hunger -= decreaseRate;
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Animal")
        {
            triggeringAI = other.gameObject;
            triggeringWithAI = true;
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if(other.tag == "Animal")
        {
            triggeringAI = null;
            triggeringWithAI = false;
        }
    }
}
