using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    private Animal currentARObejct;
    private Vector3 originScale;

    [SerializeField] private ARRaycastHitFollower aRRaycastHitFollower;

    public void SpawnAnimal(Vector3 position)
    {
        var currentLevelSelection = LevelSelection.Instance._currentLevelSelect;

        var objectSpawn = currentARObejct = Instantiate(currentLevelSelection.animal, position, Quaternion.identity);

        originScale = objectSpawn.transform.localScale;
        objectSpawn.GetComponent<AnimalRotation>().TurnOnOffReturnBack(false);

        objectSpawn.transform.localScale = objectSpawn.transform.localScale / 10;
    }

    public void SetScale(float scale)
    {
        currentARObejct.transform.localScale = originScale * scale;
    }

    public void DeleteCurrentObject()
    {
        if(currentARObejct != null)
        {
            Destroy(currentARObejct.gameObject);
        }
        originScale = Vector3.zero;
        aRRaycastHitFollower.ActiveIndicator();
    }
}
