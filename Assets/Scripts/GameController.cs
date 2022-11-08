using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using Zenject;
using TMPro;

public class GameController : MonoBehaviour
{
    public int score;
    [SerializeField] TextMeshProUGUI scoreDisp;

    private Vector2 startPos;
    public int distToDetect = 20;
    private bool fingerDown;

    [SerializeField] private GameObject destroyPrefab;
    [SerializeField] private GameObject fillPrefab;

    public static int timer;

    public Cell[] allCells;
    public static Action<string> slide;

    [Inject] private DiContainer container;


    int isGameOver;
    [SerializeField] GameObject gameOverPanel;

    public Color[] fillColors;

    //[Inject]
    //public Fill.Factory _fillFactory;

    //[Inject]
    //public GameController(Fill.Factory fillFactory)
    //{
    //    _fillFactory = fillFactory;
    //}

    private void Update()
    {
        //TEST

        //if(Input.GetKeyDown(KeyCode.W))
        //{
        //    isGameOver = 0;
        //    timer = 0;
        //    slide("w");

        //}
        //if (Input.GetKeyDown(KeyCode.S))
        //{
        //    isGameOver = 0;
        //    timer = 0;
        //    slide("s");

        //}
        //if (Input.GetKeyDown(KeyCode.D))
        //{
        //    isGameOver = 0;
        //    timer = 0;
        //    slide("d");

        //}
        //if (Input.GetKeyDown(KeyCode.A))
        //{
        //    isGameOver = 0;
        //    timer = 0;
        //    slide("a");

        //}



        if (fingerDown == false && Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            startPos = Input.touches[0].position;
            fingerDown = true;
        }

        if (fingerDown)
        {
            if (Input.touches[0].position.y >= startPos.y + distToDetect)
            {
                fingerDown = false;
                isGameOver = 0;
                timer = 0;
                slide("w");

            }
            else if (Input.touches[0].position.x <= startPos.x - distToDetect)
            {
                fingerDown = false;
                isGameOver = 0;
                timer = 0;
                slide("a");

            }
            else if (Input.touches[0].position.x >= startPos.x + distToDetect)
            {
                fingerDown = false;
                isGameOver = 0;
                timer = 0;
                slide("d");

            }
            else if (Input.touches[0].position.y <= startPos.y - distToDetect)
            {
                fingerDown = false;
                isGameOver = 0;
                timer = 0;
                slide("s");
            }
        }
    }


    private void Start()
    {
        StartSpawnFill();
        StartSpawnFill();
        Destroy(destroyPrefab);
    }
    public void StartSpawnFill() 
    {
        bool isFull = true;
        for (int i = 0; i < allCells.Length; i++)
        {
            if (allCells[i]._fill == null)
            {
                isFull = false;
            }
        }

        if (isFull == true)
        {
            return;
        }

        int whichToSpawn = UnityEngine.Random.Range(0, allCells.Length);
        if (allCells[whichToSpawn].transform.childCount != 0)
        {
            StartSpawnFill();
            return;
        }

        GameObject tempFill = container.InstantiatePrefab(fillPrefab, allCells[whichToSpawn].transform);
        Fill tempFillComp = tempFill.GetComponent<Fill>();
        allCells[whichToSpawn].GetComponent<Cell>()._fill = tempFillComp;
        tempFillComp.FillValueUpdate(2);

        //Fill tempFill = _fillFactory.Create();
        //tempFill.transform.SetParent(allCells[whichToSpawn]);
        //Fill tempFillComp = tempFill.GetComponent<Fill>();
        //allCells[whichToSpawn].GetComponent<Cell>()._fill = tempFillComp;
        //tempFillComp.FillValueUpdate(2);
    }

    public void ScoreUpdate(int scoreIn)
    {
        score += scoreIn;
        scoreDisp.text = score.ToString();
    }

    public void GameOverCheck()
    {
        isGameOver++;
        if(isGameOver >= 9)
        {
            gameOverPanel.SetActive(true);
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }
}
