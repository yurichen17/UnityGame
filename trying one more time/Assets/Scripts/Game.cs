using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    //public Transform clue1;
    //gonna try and have it be 6 by 6, but the first row and col will actually be just numbers?
    public Tile[,] grid = new Tile[6, 6];
    //CHANGE NUM BOMBS HERE
    public int bombs = 3;
    //Text bombN;
    // Start is called before the first frame update
    void Start()
    {
        //huilguilghu
        //bombN = GetComponent<Text>();
        sizeOfGrid = 6;
        //CHANGE NUM TWOS AND THREES HERE
        twos = 3;
        threes = 2;

        totalTwosThrees = twos + threes;
        toNextLevel = 0;

        BombScript.bombNum = bombs;

        for (int i= 0; i < bombs; i++)
        {
            PlaceMines();
        }
        
        for (int i=0; i<twos; i++)
        {
            PlaceTwos();
        }
        for (int i = 0; i < threes; i++)
        {
            PlaceThrees();
        }
        PlaceOnes();
        PlaceClues();
        //Instantiate(clue1, new Vector3(0, 0, 0), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        CheckInput();
        
    }
    private void CheckInput()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            int x = Mathf.RoundToInt(mousePosition.x);
            int y = Mathf.RoundToInt(mousePosition.y);
            Tile tile = grid[x, y];
            if (tile.tileState == Tile.TileState.Normal)
            {
                if (tile.isCovered)
                {
                    //Debug.Log(tile.tileKind);
                    if (tile.tileKind == Tile.TileKind.Mine)
                    {
                        //Debug.Log("Mine Clicked"+ tile.tileKind);
                        GameOver(tile);
                    }
                    else
                    {
                        if (tile.tileKind == Tile.TileKind.Two)
                        {
                            if (ScoreScript.scoreValue == 0)
                            {
                                ScoreScript.scoreValue = ScoreScript.scoreValue + 2;
                                toNextLevel++;
                                goToNextLevel();
                            }
                            else
                            {
                                ScoreScript.scoreValue = ScoreScript.scoreValue * 2;
                                toNextLevel++;
                                goToNextLevel();
                            }
                            
                        }
                        if (tile.tileKind == Tile.TileKind.Three)
                        {
                            if (ScoreScript.scoreValue == 0)
                            {
                                ScoreScript.scoreValue = ScoreScript.scoreValue + 3;
                                toNextLevel++;
                                goToNextLevel();
                            }
                            else
                            {
                                ScoreScript.scoreValue = ScoreScript.scoreValue * 3;
                                toNextLevel++;
                                goToNextLevel();
                            }
                        }
                        tile.SetIsCovered(false);
                    }
                }
            }
            else
            {
                Debug.Log("youre clicking a flag");
            }
        }
    }
    public void goToNextLevel()
    {
        if (toNextLevel == totalTwosThrees)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
    private void GameOver(Tile tile)
    {
        tile.SetClickedMine();

        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Mine");

        foreach(GameObject go in gameObjects)
        {
            Tile t = go.GetComponent<Tile>();
            if (t != tile)
            {
                if (t.tileState == Tile.TileState.Normal)
                {
                    t.SetIsCovered(false);
                }
            }
            
        }
        for (int y= 1; y< sizeOfGrid; y++)
        {
            for(int x= 1; x< sizeOfGrid; x++)
            {
                Tile t = grid[x, y];
                if (t.tileState==Tile.TileState.Flagged)
                {
                    if(t.tileKind != Tile.TileKind.Mine)
                    {
                        //If then player hass flagged a tile that is not a mine, then we want to show the player
                        //that this is so when the player clicks a mine and loses the game
                        t.SetNotAMineFlagged();
                    }
                }
            }
        }
    }


    void PlaceMines()
    {   //numbers 0 through 8
        
        int x = UnityEngine.Random.Range(1, sizeOfGrid);
        int y = UnityEngine.Random.Range(1, sizeOfGrid);
        if (grid[x, y] == null)
        {
            Tile mineTile = Instantiate (Resources.Load("Prefabs/bomb", typeof(Tile)), new Vector3(x,y,0), Quaternion.identity) as Tile;
            grid[x, y] = mineTile;
            mineTile.tileKind = Tile.TileKind.Mine;
            //Debug.Log("(" + x + "," + y + ")");

        }
        else
        {
            PlaceMines();
        }
    }
    void PlaceTwos()
    {
        int x = UnityEngine.Random.Range(1, sizeOfGrid);
        int y = UnityEngine.Random.Range(1, sizeOfGrid);
        if (grid[x, y] == null)
        {
            Tile twoTile = Instantiate(Resources.Load("Prefabs/2", typeof(Tile)), new Vector3(x, y, 0), Quaternion.identity) as Tile;
            grid[x, y] = twoTile;
            twoTile.tileKind = Tile.TileKind.Two;
            //Debug.Log("(" + x + "," + y + ")");

        }
        else
        {
            PlaceTwos();
        }

    }
    void PlaceThrees()
    {
        int x = UnityEngine.Random.Range(1, sizeOfGrid);
        int y = UnityEngine.Random.Range(1, sizeOfGrid);
        if (grid[x, y] == null)
        {
            Tile threeTile = Instantiate(Resources.Load("Prefabs/3", typeof(Tile)), new Vector3(x, y, 0), Quaternion.identity) as Tile;
            grid[x, y] = threeTile;
            threeTile.tileKind = Tile.TileKind.Three;
            //Debug.Log("(" + x + "," + y + ")");

        }
        else
        {
            PlaceThrees();
        }

    }
    void PlaceOnes()
    {
        for(int i=1; i<sizeOfGrid; i++)
        {
            for(int j=1; j<sizeOfGrid; j++)
            {
                if (grid[i, j] == null)
                {
                    Tile oneTile = Instantiate(Resources.Load("Prefabs/1", typeof(Tile)), new Vector3(i, j, 0), Quaternion.identity) as Tile;
                    grid[i, j] = oneTile;
                    oneTile.tileKind = Tile.TileKind.One;
                }
            }
        }

    }
    void PlaceClues()
    {
        //going across each row
        for(int i=1; i<sizeOfGrid; i++)
        {
            int sum = 0;
            for (int j=1; j<sizeOfGrid; j++)
            {
                if (grid[i, j].tileKind == Tile.TileKind.Mine)
                {
                    sum += 1;
                }
            }
            //can use this Tile oneTile = Instantiate(Resources.Load("Prefabs/"+numMines, typeof(Tile)), new Vector3(i, j, 0), Quaternion.identity) as Tile;
            //can make the tiles "uncovered" so that u cant flag on top of the clues
            //Debug.Log("sum:" + sum);
            
            Instantiate(Resources.Load((string)("Prefabs/clue" + sum)), new Vector3(i, sizeOfGrid, 0), Quaternion.identity);

        }
        //going across each col
        for (int i = 1; i < sizeOfGrid; i++)
        {
            int sum = 0;
            for (int j = 1; j < sizeOfGrid; j++)
            {
                if (grid[j, i].tileKind == Tile.TileKind.Mine)
                {
                    sum += 1;
                }
            }
            //this goes bottom up
            //Debug.Log("sum:" + sum);
            Instantiate(Resources.Load("Prefabs/clue" + sum), new Vector3(0, i, 0), Quaternion.identity);
        }
    }
    private
    int twos;
    int threes;
    int sizeOfGrid;
    
    int totalTwosThrees;
    int toNextLevel;
}
