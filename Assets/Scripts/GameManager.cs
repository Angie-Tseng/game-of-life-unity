using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    
    public GameObject mapGameObject;
    public Image startButton;
    public Sprite startImg;
    public Sprite pauseImg;
    public Slider speedSlider;
    
    private float speed = 0.8f;
    private float timer;

    private Map mapManager;
    private bool isStart;
    private int counter;
    private bool[,] currentMap;
    
    public void SetSpeed()
    {
        switch (Mathf.CeilToInt(speedSlider.value))
        {
            case 1: speed = 1.5f; break;
            case 2: speed = 1f; break;
            case 3: speed = 0.8f; break;
            case 4: speed = 0.5f; break;
            case 5: speed = 0.25f; break;
        }
    }

    public void GameStart()
    {
        isStart = !isStart;
        if (isStart)
        {
            //this.InvokeRepeating("Generation", speed, speed);
            startButton.sprite = pauseImg;
            return;
        }

        startButton.sprite = startImg;
        //this.CancelInvoke();
    }

    private void Update()
    {
        if (!isStart) return;
        if(timer > speed)
        {
            Generation();
            timer = 0;
        }
        timer += Time.deltaTime;
    }

    public void SelectMap(int p = 0)
    {
        mapManager.Init(p);
    }

    void Awake()
    {
        counter = 0;
        mapManager = mapGameObject.GetComponent<Map>();
        currentMap = new bool[Map.H, Map.W];
        startButton.sprite = startImg;
        isStart = false;
        timer = 0;
    }

    void Generation () {
        //If is not game start: do nothing
        if (!isStart) return;

        //Count generation
        ++counter;
        
        for (int i = 0; i < Map.H; ++i)
        {
            for(int j = 0; j < Map.W; ++j)
            {
                currentMap[i, j] = mapManager.Rule(i, j);
            }
        }

        for (int i = 0; i < Map.H; ++i)
        {
            for (int j = 0; j < Map.W; ++j)
            {
                mapManager.SetActive(i, j, currentMap[i, j]);
            }
        }
    } 
}
