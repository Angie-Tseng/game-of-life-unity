using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Map : MonoBehaviour
{
    public GameObject cell;
    public GameObject block;
    public GameObject mapPanel;
    public TextAsset[] image = new TextAsset[4];
    public GameObject[,] bg;
    public static int W = 35;
    public static int H = 19;
    public static int cellSize = 5;

    private GameObject[,] map;
    private int[,] neighbor;
    private int[] chooseCell;

    // Use this for initialization
    void Awake () {
        //Create map
        map = new GameObject[H, W];
        bg = new GameObject[H, W];

        for (int i = 0; i < H; ++i)
        {
            for (int j = 0; j < W; ++j)
            {
                //background
                bg[i, j] = GameObject.Instantiate(block);
                bg[i, j].transform.parent = mapPanel.transform;
                bg[i, j].transform.position = new Vector3(
                    (-1f - W / 2 + j) * cellSize,
                    (H / 2 - 0.5f - i) * cellSize,
                     0
                    );

                //cell
                map[i, j] = GameObject.Instantiate(cell);
                map[i, j].SetActive(false);
                map[i, j].transform.parent = bg[i, j].transform;
                map[i, j].transform.localPosition = new Vector3(0, 0, 0);
            }
        }
        
        //Define neighbor
        neighbor = new int[,]{
            { -1, -1}, { -1, 0}, { -1, 1},
            {  0, -1},           {  0, 1},
            {  1, -1}, {  1, 0}, {  1, 1}
        };
    }
	
    //Check the cell at (w,h) is alive or not
    public bool IsAlive(int h, int w)
    {
        if (map[h, w].activeSelf == true)
        {
            return true;
        }
        return false;
    }

    //Initialize the map
    public void Init(int p = 1)
    {
        //Reset the map
        for (int i = 0; i < H; ++i)
        {
            for (int j = 0; j < W; ++j)
            {
                map[i, j].SetActive(false);
            }
        }

        //for randomized map
        if (p > 3)
        {
            RandomMap(p);
            return;
        }

        //for p = 1,2,3, read the certain map
        //the length of the images are: 3, 5, 13
        int centerImg;
        switch (p)
        {
            case 0:
                return;
            case 1:
                centerImg = 1;
                break;
            case 2:
                centerImg = 2;
                break;
            default:
                centerImg = 6;
                break;
        }

        string[] lines = image[p].text.Split('\n');
        for (int i = 0; i < lines.Length; ++i)
        {
            string[] s = lines[i].Split(',');
            for (int j = 0; j < s.Length; ++j)
            {
                bool set = false;
                if (int.Parse(s[j]) == 1)
                {
                    set = true;
                }
                map[Mathf.FloorToInt(H / 2) - centerImg + i,
                       Mathf.FloorToInt(W / 2) - centerImg + j].SetActive(set);
            }
        }
        
    }

    //Follow the rule of life
    public bool Rule(int h, int w)
    {
        //Calculate the alive neighbor
        int sum = 0;
        for (int i = 0; i < 8; ++i)
        {
            try
            {
                if (IsAlive(h + neighbor[i, 0], w + neighbor[i, 1])) ++sum;
            }
            catch {
                continue;
            }
        }

        //If this is alive:
        if (IsAlive(h, w))
        {
            if (sum == 2 || sum == 3) return true;
        }else if (sum == 3) return true;

        return false;
    }

    //Costumized map
    public void SetActive(int h, int w, bool isActive)
    {
        map[h, w].SetActive(isActive);
    }

    //Randomized map
    private void RandomMap(int p)
    {
        //choose p% cells
        int n = Mathf.FloorToInt(W * H * p / 100f);
        for (int i = 0; i < n; ++i)
        {
            int index;
            do
            {
                index = Random.Range(0, W * H);
            } while (IsAlive(Mathf.CeilToInt(index / W), index % W));

            SetActive(Mathf.CeilToInt(index / W), index % W, true);
        }
    }
}
