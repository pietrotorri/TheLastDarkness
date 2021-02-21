using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    public float health;
    public GameObject[] item;

    public static bool dead;

    void Update()
    {
        if(health <= 0)
        {
            dead = true;
        }

        if(dead == true)
        {
            Die();
        }
    }

    void Die()
    {
        for (int i = 0; i < item.Length; i++)
        {
            if (item[i] == null)
                print("item in the tree named" + this.gameObject.name + "has not been set! Missing ID: " + i);

            GameObject spawnedItem = Instantiate(item[i], transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
