using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    public Vector2 startPosition = new Vector2(0, 8.5f);
    public GameObject MainScreen;
    public GameObject Winner;
    public TextMeshProUGUI text;
    public Sprite Nadie;

    enum State { TITLE, PLAYING, WON}

    State state = State.TITLE;

    int playerCount = 0;

    public GameObject[] players;

    public GameObject[] flappyPrefabs;

    int currentPipe = -1;
    public int points = 0;

    // Start is called before the first frame update
    void Start()
    {
        players = new GameObject[3];
        spawnPlayer();
        spawnPlayer();
        spawnPlayer(); 
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
            ResetLevel();


        switch (state)
        {
            case State.TITLE:
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    state = State.PLAYING;
                    MainScreen.SetActive(false);
                    GetComponent<PipeSpawner>().isSpawning = true;
                }
                    break;

            case State.PLAYING:
                if (playerCount <= 1)
                {
                    if (points >= 1)
                    {
                        Winner.GetComponent<SpriteRenderer>().color = players[0].GetComponent<FlappyController>().Title.GetComponent<SpriteRenderer>().color;
                        Winner.GetComponent<SpriteRenderer>().sprite = players[0].GetComponent<FlappyController>().Title.GetComponent<SpriteRenderer>().sprite;
                        Winner.SetActive(true);
                        state = State.WON;

                    }
                    else if(playerCount <= 0 && points <= 0)
                    {
                        Winner.GetComponent<SpriteRenderer>().color = new Color(255,255,255,1);
                        Winner.GetComponent<SpriteRenderer>().sprite = Nadie;
                        Winner.SetActive(true);
                        state = State.WON;
                    }
                }
                break;
        }
        text.SetText("Puntos: " + points);
    }

    void spawnPlayer()
    {
        players[playerCount] = Instantiate(flappyPrefabs[playerCount]);
        players[playerCount].GetComponent<FlappyController>().startPos = startPosition - new Vector2(0, playerCount);
        players[playerCount].GetComponent<FlappyController>().SetGamecontroller(this);
        playerCount++;
    }

    public void KillBird(GameObject bird)
    {
        GameObject[] temp = players;
        for (int i = 0; i < playerCount; i++)
        {
            if (bird == players[i])
            {
                temp = new GameObject[playerCount - 1];
                int k = 0;
                for (int j = 0; j < playerCount; j++)
                {
                    if (j != i)
                        temp[k++] = players[j];
                }
            }

        }
        players = temp;
        playerCount--;
    }


    public void PassPipe(int pipeId)
    {
        if (pipeId != currentPipe)
        {
            currentPipe = pipeId;
            points++;
        }
    }

    public void ResetLevel()
    {
        state = State.TITLE;
        foreach (GameObject player in players)
            Destroy(player);
        playerCount = 0;
        players = new GameObject[3];
        for (int i = 0; i < 3; i++)
            spawnPlayer();
        Winner.GetComponent<SpriteRenderer>().sprite = null;
        MainScreen.SetActive(true);
        GetComponent<PipeSpawner>().isSpawning = false;
        int c = GameObject.Find("Pipes Container").transform.childCount;
        for (int i = 0; i < c; i++)
        {
            Destroy(GameObject.Find("Pipes Container").transform.GetChild(i).gameObject);
        }
        GetComponent<PipeSpawner>().PipeSpeed = 2.5f;
        points = 0;
    }
}
