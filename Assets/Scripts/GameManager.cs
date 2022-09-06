using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Score")]
    [Space(10)]
    public Text TextLevel;
    public Text TextBestScore;
    public Text TextMaxLevel;
    public Text TextScore;

    public Game Game;

    [Header("Builder")]
    [Space(10)]

    public GameObject tilePrefad;

    public int LevelWidth;
    public int LevelLength;
    public Transform tileHolder;
    public float tileSize = 1;
    public float tileEndHeight = 1;

    [Space(10)]

    public TileObject[,] tileGrid = new TileObject[0, 0];

    [Header("Resource")]
    [Space(10)]
    [SerializeField]
    public GameObject foodPrefab;
    public GameObject blockPrefab;
    public GameObject wallPrefab;
    public GameObject finishPrefab;
    public Transform resourceHolder;
    public Transform wallHolder;
    public GameObject[] BlockPrefabs;
    public GameObject[] FoodPrefabs;
    public GameObject[] WallPrefabs;
    public SnakeMovement SnakeMovement;

    private int randomBlockPrefab;
    private int randomFoodPrefab;
    private int randomWallPrefab;

    [Range(0, 1)]
    public float obstacleChance = 0.3f;
    public float wallChance = 0.3f;
    bool isBlock = false;
    public float finishZ;
    public int LevelLong;

    private void Start()
    {
        CreatLevel();
        GameObject tmpTile = Instantiate(finishPrefab);
        tmpTile.transform.position = new Vector3(0, 1, LevelLength - 1);
        finishZ = tmpTile.transform.position.z;
        LevelLong = LevelLength + (Game.LevelIndex) * 10;
        TextLevel.text = "Level " + (Game.LevelIndex).ToString();
        TextBestScore.text = "Best Score " + (Game.BestScore).ToString();
        TextMaxLevel.text = "Max Level " + (Game.MaxLevel).ToString();
        TextScore.text = "Score " + (Game.Score + SnakeMovement.CurrentScore).ToString();
    }
    //Создание уровня по ширине и длине
    public void CreatLevel()
    {
        List<TileObject> visualGrid = new List<TileObject>();

        for (int z = 0; z < LevelLength; z++)
        {
            for (int x = -LevelWidth / 2 - 1; x < LevelWidth / 2 + 1; x++)
            {
                if (x == -LevelWidth / 2 - 1 || x == LevelWidth / 2)
                { 
                    for (int y = 0; y < 2; y++)
                    {
                        SpawnWallTile(x, y, z);
                        TileObject spawnedTile = SpawnWallTile(x, y, z);
                    }
                }
                else
                {
                    SpawnTile(x, z);
                    TileObject spawnedTile = SpawnTile(x, z);
                }
                
            }

            for (int x = -LevelWidth / 2; x < LevelWidth / 2; x++)
            {
                //Создание блоков
                if (z % 5 == 0 && z > 5 && z < LevelLength - 5)
                {
                    isBlock = true;
                    SpawnObstacle(x, z);
                }
                //Создание еды
                if (z % 5 != 0 && z > 5 && z < LevelLength - 5)
                {
                    bool spawnObstacle = Random.value <= obstacleChance;

                    if (spawnObstacle)
                    {
                        isBlock = false;
                        SpawnObstacle(x, z);
                    }
                }
                //Создание стен
                if (z > 5 && z < LevelLength - 5)
                {
                    bool spawnWall = Random.value <= wallChance;
                    if (spawnWall)
                    {
                        isBlock = false;
                        SpawnWall(x, z);
                    }
                }                
            }
        }
    }
    TileObject SpawnTile(float xPos, float zPos)
    {
        GameObject tmpTile = Instantiate(tilePrefad);
        tmpTile.transform.position = new Vector3(xPos, 0, zPos);
        tmpTile.transform.SetParent(tileHolder);
        tmpTile.name = "Tile " + xPos + " - " + zPos;
        return tmpTile.GetComponent<TileObject>();
    }
    TileObject SpawnWallTile(float xWall, float yWall,  float zWall)
    {
        GameObject tmpTile = Instantiate(tilePrefad);
        tmpTile.transform.position = new Vector3(xWall, yWall, zWall);
        tmpTile.transform.SetParent(tileHolder);
        tmpTile.name = "Tile " + xWall + " - " + zWall;
        return tmpTile.GetComponent<TileObject>();
    }
    public void SpawnObstacle(float xPos, float zPos)
    {
        GameObject spawnedObstacle = null;

        if (isBlock)
        {
            if(Game.LevelIndex == 1)
            {
                randomBlockPrefab = Random.Range(0, 4);
            }
            else
            {
                randomBlockPrefab = Random.Range(0, 9);
            }
            
            GameObject block = Instantiate(BlockPrefabs[randomBlockPrefab], transform);
            spawnedObstacle = Instantiate(block);
            spawnedObstacle.name = "Block " + xPos + " - " + zPos;
        }
        else
        {
            randomFoodPrefab = Random.Range(0, 4);
            GameObject food = Instantiate(FoodPrefabs[randomFoodPrefab], transform);
            spawnedObstacle = Instantiate(food);
            spawnedObstacle.name = "Food " + xPos + " - " + zPos;
        }

        spawnedObstacle.transform.position = new Vector3(xPos, 1, zPos);
        spawnedObstacle.transform.SetParent(resourceHolder);
    }
    
    public void SpawnWall(float xPos, float zPos)
    {
        GameObject spawnedWall = null;

        spawnedWall = Instantiate(wallPrefab);
        spawnedWall.name = "Wall " + xPos + " - " + zPos;
        spawnedWall.transform.position = new Vector3(xPos + 0.5f, 1, zPos);
        spawnedWall.transform.SetParent(wallHolder);

    }
}
