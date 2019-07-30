using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blink : MonoBehaviour
{

    public float BlinkSpeed = 0.05f;
    // Update is called once per frame
    void Update()
    {
        Color c = GetComponent<SpriteRenderer>().color;
        c.a = Mathf.Abs(Mathf.Sin(Time.frameCount * BlinkSpeed));
        GetComponent<SpriteRenderer>().color = c;
    }
}
