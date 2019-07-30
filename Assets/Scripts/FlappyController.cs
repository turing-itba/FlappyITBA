using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlappyController : MonoBehaviour {

    public enum FlappyState { START, PLAYING, DEAD}

    private GameController gc;

    public GameObject Title;


    public float Strength = 0.3f;
    public float maxVelocity = 2f;

    public GameObject deathEffect;

    [HideInInspector]
    public Vector2 startPos;
    private float waitingSpeed = 0.06f;
    private float waitingAmplitude = 0.25f;

    //SET FLAPPYSPRITES
    public Sprite[] FlappySprites;

    //TODO SET KEY
    public KeyCode key;

    Rigidbody2D rb;

    FlappyState state = FlappyState.START;

	void Start () {
        rb = GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezeAll;

    }
	
	void FixedUpdate () {
        switch (state) {

            case FlappyState.START:
                transform.position = startPos + new Vector2(0, Mathf.Sin(Time.frameCount * waitingSpeed)) * waitingAmplitude;
                Animate();
                if (Input.GetKeyDown(key))
                    StartGame();
                break;


            case FlappyState.PLAYING:
                updateRotation();

                if (rb.velocity.y < -maxVelocity)
                    rb.AddForce(-Physics2D.gravity);

                Animate();

                if (Input.GetKeyDown(key) && state == FlappyState.PLAYING)
                {
                    Flap();
                }
                break;
        }
        
	}

    private void StartGame()
    {
        state = FlappyState.PLAYING;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation & RigidbodyConstraints2D.FreezePositionX;
        Destroy(transform.GetChild(0).gameObject);
        Flap();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Killer" && state != FlappyState.DEAD)
        {
            state = FlappyState.DEAD;
            Instantiate(deathEffect).transform.position = transform.position;
            gc.KillBird(gameObject);
            Destroy(gameObject);
        }
        

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Pass" && state == FlappyState.PLAYING)
        {
            gc.PassPipe(collision.gameObject.GetComponent<PipeScript>().pipeId);
        }
    }

    void updateRotation()
    {
        float vY = Mathf.Clamp(rb.velocity.y * 10, -60, 60);
        transform.rotation = Quaternion.Euler(0, 0, vY);
    }

    public void Flap()
    {
        rb.velocity = new Vector2(0, Strength);
    }

    private int currentSprite = 0;

    public void Animate()
    {
        if(Time.frameCount % 10 == 0)
            GetComponent<SpriteRenderer>().sprite = FlappySprites[currentSprite++ % FlappySprites.Length];
    }

    public void SetGamecontroller(GameController gc)
    {
        this.gc = gc;
    }

}
