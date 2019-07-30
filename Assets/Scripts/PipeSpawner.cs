using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeSpawner : MonoBehaviour {

    int pipeCount = 0;

    public GameObject PipesPrefab;
    private GameObject pipesParent;

    public float xSpawn = 20;

    public float MaxHeight;
    public float MinHeight;

    public float SpawnDelay = 1.5f;

    [HideInInspector]
    public bool isSpawning = false;

    public float PipeSpeed = 2.5f;

    float time = 0;

    void Start () {
        pipesParent = new GameObject();
        pipesParent.name = "Pipes Container";

    }

    // Update is called once per frame
    void FixedUpdate () {
        if (isSpawning & time >= SpawnDelay)
        {
            spawnPipe();
            time = 0;
        }
        time += Time.fixedDeltaTime;
	}


    void spawnPipe()
    {
        GameObject pipe;
        pipe = Instantiate(PipesPrefab);
        pipe.transform.position = new Vector3(xSpawn, Random.Range(MinHeight, MaxHeight));
        pipe.transform.parent = pipesParent.transform;
        pipe.GetComponent<PipeScript>().setSpeed(PipeSpeed);
        pipe.GetComponent<PipeScript>().pipeId = pipeCount;
        pipe.name = "Pipe_" + pipeCount;
        pipeCount++;
        PipeSpeed += 0.01f;
    }

    public void ClearWorld()
    {
        pipeCount = 0;
        Destroy(pipesParent);
        pipesParent = new GameObject();
        pipesParent.name = "Pipes Container";
    }
}
