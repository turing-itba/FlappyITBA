using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeScript : MonoBehaviour {

    public int pipeId;

    float Speed = 2.5f;

    Renderer render;
    bool inCamera = false;

	// Use this for initialization
	void Start () {
        render = GetComponentInChildren<Renderer>();
	}
	
	// Update is called once per frame
	void Update () {
        movePipes();
        destroyOnLeave();
	}

    void movePipes()
    {
        transform.position -= new Vector3(Speed * Time.deltaTime,0,0);
    }

    public void setSpeed(float speed)
    {
        this.Speed = speed;
    }

    void destroyOnLeave()
    {
        if (render.isVisible)
        {
            inCamera = true;
        }
        else
        {
            if (inCamera)
            {
                Destroy(gameObject);
            }
        }
    }
}
