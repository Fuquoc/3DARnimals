using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalSpawner : MonoBehaviour
{
    [SerializeField] private Animal _animal;

    private void OnEnable() 
    {
        PuzzleUIBoard.OnFinishPuzzleGame += SpawnAnimal;    
    }

    private void OnDisable() 
    {
        PuzzleUIBoard.OnFinishPuzzleGame -= SpawnAnimal;    
    }

    public void SetAnimal(Animal animal)
    {
        _animal = animal;
    }

    public void SpawnAnimal()
    {
        Instantiate(_animal, transform);
    }
}
