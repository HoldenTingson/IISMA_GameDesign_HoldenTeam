using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _healthGlobe, _staminaGlobe;

    public void DropItems()
    {
        int randomNum = Random.Range(1, 5);

        if (randomNum == 3)
        {
            Instantiate(_healthGlobe, transform.position, Quaternion.identity);
        }

        if (randomNum == 2)
        {
            Instantiate(_staminaGlobe, transform.position, Quaternion.identity);
        }
    }
}
