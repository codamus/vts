using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnObjects : MonoBehaviour
{

    public Transform spawnPosition;

    public GameObject prefabNote;
    public GameObject prefabSpaceOne;
    public GameObject prefabSpaceTwo;
    public GameObject prefabSpaceThree;

    public GameObject parent;

  

    public void spawnSphere()
    {
       GameObject newSphere =  Instantiate(prefabNote, parent.transform);
        newSphere.transform.position = new Vector3(spawnPosition.position.x + Random.value / 2, spawnPosition.position.y + Random.value / 2, spawnPosition.position.z + Random.value / 2);
    }

    public void spawnSpaceOne()
    {
        GameObject newSphere = Instantiate(prefabSpaceOne, parent.transform);
        newSphere.transform.position = new Vector3(spawnPosition.position.x + Random.value / 2, spawnPosition.position.y + Random.value / 2, spawnPosition.position.z + Random.value / 2);
    }

    public void spawnSpaceTwo()
    {
        GameObject newSphere = Instantiate(prefabSpaceTwo, parent.transform);
        newSphere.transform.position = new Vector3(spawnPosition.position.x + Random.value / 2, spawnPosition.position.y + Random.value / 2, spawnPosition.position.z + Random.value / 2);
    }

    public void spawnSpaceThree()
    {
        GameObject newSphere = Instantiate(prefabSpaceThree, parent.transform);
        newSphere.transform.position = new Vector3(spawnPosition.position.x + Random.value / 2, spawnPosition.position.y + Random.value / 2, spawnPosition.position.z + Random.value / 2);
    }
}
