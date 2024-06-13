using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnObjects : MonoBehaviour
{
    //left bottom corner of the spawnarea
    //spawn area is a cube
    public Transform spawnPosition;

    //objects that are spawning
    public GameObject prefabNote;
    public GameObject prefabIntervalm3;
    public GameObject prefabIntervalM3;
    public GameObject prefabIntervalP5;

    //spawning objectst and set this gameobject as parent
    public GameObject parent;

  
    public void spawnNote()
    {
       GameObject newSphere =  Instantiate(prefabNote, parent.transform);
        //calculate random position in a range of +0.5 of xyz on the spawnPosition transform
        newSphere.transform.position = new Vector3(spawnPosition.position.x + Random.value / 2, spawnPosition.position.y + Random.value / 2, spawnPosition.position.z + Random.value / 2);
    }

    public void spawnIntervalm3()
    {
        GameObject newSphere = Instantiate(prefabIntervalm3, parent.transform);
        //calculate random position in a range of +0.5 of xyz on the spawnPosition transform
        newSphere.transform.position = new Vector3(spawnPosition.position.x + Random.value / 2, spawnPosition.position.y + Random.value / 2, spawnPosition.position.z + Random.value / 2);
    }

    public void spawnIntervalM3()
    {
        GameObject newSphere = Instantiate(prefabIntervalM3, parent.transform);
        //calculate random position in a range of +0.5 of xyz on the spawnPosition transform
        newSphere.transform.position = new Vector3(spawnPosition.position.x + Random.value / 2, spawnPosition.position.y + Random.value / 2, spawnPosition.position.z + Random.value / 2);
    }

    public void spawnIntervalP5()
    {
        GameObject newSphere = Instantiate(prefabIntervalP5, parent.transform);
        //calculate random position in a range of +0.5 of xyz on the spawnPosition transform
        newSphere.transform.position = new Vector3(spawnPosition.position.x + Random.value / 2, spawnPosition.position.y + Random.value / 2, spawnPosition.position.z + Random.value / 2);
    }
}
