using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalSpawner : MonoBehaviour
{
    [SerializeField] private Animal _animal;

    public void SetAnimal(Animal animal)
    {
        _animal = animal;
    }

    public void SpawnAnimal()
    {
        Instantiate(_animal, transform);
    }
}
