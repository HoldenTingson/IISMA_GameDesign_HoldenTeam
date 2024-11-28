using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSpawner : MonoBehaviour
{
    [SerializeField] private GameObject healthGlobe, staminaGlobe;

    public void DropItems()
    {
        int randomNum = Random.Range(1, 5);

        if (randomNum == 1)
        {
            Instantiate(healthGlobe, transform.position, Quaternion.identity);
        }

        if (randomNum == 2)
        {
            Instantiate(staminaGlobe, transform.position, Quaternion.identity);
        }
    }
}
