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
    public bool weaponEquipped;

    public static bool triggeringWithAI;
    public static GameObject triggeringAI;

    public static bool triggeringWithTree;
    public static GameObject treeObject;

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

        // tree chopping
        if(triggeringWithTree == true && treeObject)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Attack(treeObject);
            }
        }
    }

    public void Attack(GameObject target)
    {
        if(target.tag == "Animal" && weaponEquipped)
        {
            Animal animal = target.GetComponent<Animal>();
            animal.health -= damage;
        }

        if(target.tag == "Tree" && weaponEquipped)
        {
            Tree tree = target.GetComponent<Tree>();
            tree.health -= damage;
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

    public void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Tree"))
        {
            triggeringWithTree = true;
            treeObject = other.gameObject;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Animal"))
        {
            triggeringAI = other.gameObject;
            triggeringWithAI = true;
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Animal"))
        {
            triggeringAI = null;
            triggeringWithAI = false;
        }
    }
}
