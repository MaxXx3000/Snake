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

    [Header("Builder")]
    [Space(10)]

    public GameObject tilePrefad;

    public int LevelWidth;
    public int LevelLength;
    public Transform tileHolder;
    public Transform natureHolder;
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
    public GameObject naturePrefab;
    public GameObject finishPrefab;
    public Transform resourceHolder;
    public Transform wallHolder;
    public GameObject[] BlockPrefabs;
    public GameObject[] FoodPrefabs;
    public GameObject[] NaturePrefabs;
    public GameObject[] WallPrefabs;
    public SnakeMovement SnakeMovement;
    public Game Game;

    private int randomBlockPrefab;
    private int randomFoodPrefab;
    private int randomWallPrefab;
    private int randomNaturePrefab;

    [Range(0, 1)]
    public float obstacleChance = 0.2f;
    public float obstacleNature = 0.3f;
    public float wallChance = 0.2f;
    bool isBlock = false;
    public float finishZ;
    public int LevelLong;

    private void Start()
    {
        CreatLevel();
        GameObject tmpTile = Instantiate(finishPrefab);
        tmpTile.transform.position = new Vector3(0, 1, LevelLength - 1);
        finishZ = tmpTile.transform.position.z;
        TextLevel.text = "Level " + (Game.LevelIndex).ToString();
        TextBestScore.text = "Best Score " + (Game.BestScore).ToString();
        TextMaxLevel.text = "Max Level " + (Game.MaxLevel).ToString();
        TextScore.text = "Score " + (Game.Score + SnakeMovement.CurrentScore).ToString();
    }
    //Создание уровня по ширине и длине
    public void CreatLevel()
    {
        LevelLength += Game.LevelIndex * 10;
        List<TileObject> visualGrid = new List<TileObject>();

        for (int z = -5; z < LevelLength; z++)
        {
            for (int x = -LevelWidth * 4; x < LevelWidth * 4; x++)
            {
                if (x <= -LevelWidth / 2 - 1 || x >= LevelWidth / 2)
                { 
                    for (int y = 0; y < 2; y++)
                    {
                        SpawnTile(x, y, z);
                    }

                    if(x != -LevelWidth / 2 - 1 || x != LevelWidth / 2 && z > 2)
                    {
                        bool spawnNature = Random.value <= obstacleNature;

                        if (spawnNature)
                        {
                            int y = 2;
                            SpawnNature(x, y, z);
                        }
                    }

                }
                else
                {
                    int y = 0;
                    SpawnTile(x, y, z);
                    TileObject spawnedTile = SpawnTile(x, y, z);
                }
                
            }

            for (int x = -LevelWidth / 2; x < LevelWidth / 2; x++)
            {
                //Создание блоков
                if (z % 9 == 0 && z > 5 && z < LevelLength - 5)
                {
                    int y = 0;
                    isBlock = true;
                    SpawnObstacle(x, y, z);
                }
                //Создание еды
                if (z % 9 != 0 && z > 5 && z < LevelLength - 5)
                {
                    bool spawnObstacle = Random.value <= obstacleChance;

                    if (spawnObstacle)
                    {
                        int y = 0;
                        isBlock = false;
                        SpawnObstacle(x, y, z);
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
    TileObject SpawnTile(float xPos, float yPos, float zPos)
    {
        GameObject tmpTile = Instantiate(tilePrefad);
        tmpTile.transform.position = new Vector3(xPos, yPos, zPos);
        tmpTile.transform.SetParent(tileHolder);
        tmpTile.name = "Tile " + xPos + " - " + zPos;
        return tmpTile.GetComponent<TileObject>();
    }
    public void SpawnObstacle(float xPos, float yPos, float zPos)
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

    public void SpawnNature(float xNature, float yNature, float zNature)
    {
        GameObject sawnedNature = null;

        randomNaturePrefab = Random.Range(0, 22);
        GameObject nature = Instantiate(NaturePrefabs[randomNaturePrefab], transform);
        sawnedNature = Instantiate(nature);
        sawnedNature.name = "Nature " + xNature + " - " + zNature;
        sawnedNature.transform.position = new Vector3(xNature, 1.5f, zNature);
        sawnedNature.transform.SetParent(natureHolder);

    }


}
