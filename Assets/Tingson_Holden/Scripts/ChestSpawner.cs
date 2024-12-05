using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _item;

    public void DropItems()
    {
            Instantiate(_item, transform.position, Quaternion.identity);
    }
}
