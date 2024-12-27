using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public void SpawnAnimal(Vector3 position)
    {
        var currentLevelSelection = LevelSelection.Instance._currentLevelSelect;

        var objectSpawn = Instantiate(currentLevelSelection.animal, position, Quaternion.identity);

        objectSpawn.transform.localScale = objectSpawn.transform.localScale / 10;
    }
}
