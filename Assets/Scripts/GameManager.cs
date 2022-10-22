using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public ObstacleObject ObstacleObject;

    [Header("Score")]
    [Space(10)]
    public Text TextLevel;
    public Text TextCurrLevel;
    public Text TextNextLevel;
    public Text TextBestScore;
    public Text TextMaxLevel;
    public Text TextScore;

    [Header("Builder")]
    [Space(10)]

    public GameObject tileNaturePrefad;
    public GameObject tileDesertPrefad;
    public GameObject tileSnowPrefad;

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
    public GameObject snowBlockPrefab;
    public GameObject wallPrefab;
    public GameObject finishPrefab;
    public Transform resourceHolder;
    public Transform wallHolder;
    public GameObject[] NaturePrefabs;
    public GameObject[] DesertPrefabs;
    public GameObject[] SnowPrefabs;
    public GameObject[] FencePrefabs;
    public SnakeMovement SnakeMovement;
    public Game Game;

    private int randomBlockPrefab;
    private int randomFoodPrefab;
    private int randomWallPrefab;
    private int randomNaturePrefab;
    private int randomDesertPrefab;
    private int randomSnowPrefab;
    private int randomFencePrefab;

    [Range(0, 1)]
    public float obstacleChance = 0.2f;
    public float foodChance = 0.2f;
    public float obstacleNature = 0.3f;
    public float wallChance = 0.2f;
    bool isBlock = false;
    public float finishZ;
    public int LevelLong;
    public int blockHP;
    public int randomBiome;

    private void Start()
    {
        if(Game.LevelIndex == 1)
        {
            randomBiome = 0;
        }
        else
        {
            //randomBiome = Random.Range(0, 2);
            // 0 - зелень; 1 - пустыня; 2 - зима
            randomBiome = Game.LevelIndex % 3;
        }

        randomFencePrefab = Random.Range(0, 12);
        CreatLevel();
        EdgeLevel();
        GameObject tmpTile = Instantiate(finishPrefab);
        tmpTile.transform.position = new Vector3(-0.5f, 1, LevelLength - 1);
        finishZ = tmpTile.transform.position.z;
        TextLevel.text = (Game.LevelIndex).ToString();
        TextBestScore.text = (Game.BestScore).ToString();
        TextMaxLevel.text = (Game.MaxLevel).ToString();
        TextScore.text = (Game.Score + SnakeMovement.CurrentScore).ToString();
        TextCurrLevel.text = (Game.LevelIndex).ToString();
        TextNextLevel.text = (Game.LevelIndex + 1).ToString();
    }
    //Создание уровня по ширине и длине
    public void CreatLevel()
    {
        LevelLength += Game.LevelIndex * 10;
        List<TileObject> visualGrid = new List<TileObject>();

        for (int z = -5; z < LevelLength; z++)
        {
            for (int x = -LevelWidth * 2; x < LevelWidth * 2; x++)
            {
                if (x <= -LevelWidth / 2 - 1 || x >= LevelWidth / 2)
                {
                    int y = 0;
                    SpawnTile(x, y, z);

                    if (x != -LevelWidth / 2 - 1 || x != LevelWidth / 2 && z > 2)
                    {
                        bool spawnNature = Random.value <= obstacleNature;

                        if (spawnNature)
                        {
                            y = 2;
                            SpawnNature(x, y, z);
                        }
                    }

                }
                else
                {
                    int y = 0;
                    SpawnTile(x, y, z);
                }

                if (x == -LevelWidth / 2 - 1 || x == LevelWidth / 2 - 1)
                {
                    SpawnWall(x, z);
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
                    if (SnakeMovement.Length > 50)
                    {
                        foodChance = 0.1f;
                    }

                    bool spawnObstacle = Random.value <= foodChance;

                    if (spawnObstacle)
                    {
                        int y = 0;
                        isBlock = false;
                        SpawnObstacle(x, y, z);
                    }
                }
                //Создание стен
                if (z > 5 && z < LevelLength - 5 && x!= LevelWidth / 2 - 1)
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

    public void EdgeLevel()
    {
        for (int x = -LevelWidth / 2; x < LevelWidth / 2; x++)
        {
            int z = LevelLength;
            SpawnEdgeWall(x, z);
        }

        for (int z = LevelLength; z < LevelLength + 20; z++)
        {
            for (int x = -LevelWidth * 2; x < LevelWidth * 2; x++)
            {
                int y = 0;
                SpawnTile(x, y, z);
                bool spawnNature = Random.value <= obstacleNature;
                if (spawnNature)
                {
                    y = 2;
                    SpawnNature(x, y, z);
                }
            }
        }
    }

    public void SpawnTile(float xPos, float yPos, float zPos)
    {
        GameObject sawnedTile = null;
        if (randomBiome == 0)
        {
            sawnedTile = Instantiate(tileNaturePrefad);
        }
        if (randomBiome == 1)
        {
            sawnedTile = Instantiate(tileDesertPrefad);
        }
        if (randomBiome == 2)
        {
            sawnedTile = Instantiate(tileSnowPrefad);
        }

        sawnedTile.name = "Tile " + xPos + " - " + zPos;
        sawnedTile.transform.position = new Vector3(xPos, yPos, zPos);
        sawnedTile.transform.SetParent(tileHolder);
    }

    public void SpawnObstacle(float xPos, float yPos, float zPos)
    {
        GameObject spawnedObstacle = null;

        if (isBlock)
        {
            if(Game.LevelIndex == 1)
            {
                blockPrefab.GetComponent<ObstacleObject>().hp = Random.Range(1, 5);
            }
            else
            {
                if(randomBiome == 2)
                {
                    snowBlockPrefab.GetComponent<ObstacleObject>().hp = Random.Range(1, 20);
                }
                else
                {
                    blockPrefab.GetComponent<ObstacleObject>().hp = Random.Range(1, 20);
                }
                
            }

            if(SnakeMovement.Length > 50)
            {
                if (randomBiome == 2)
                {
                    snowBlockPrefab.GetComponent<ObstacleObject>().hp = Random.Range(10, 20);
                }
                else
                {
                    blockPrefab.GetComponent<ObstacleObject>().hp = Random.Range(10, 20);
                }
            }
            Vector3 rotate = transform.eulerAngles;
            rotate.y = 0;
            transform.rotation = Quaternion.Euler(rotate);
            if (randomBiome == 2)
            {
                spawnedObstacle = Instantiate(snowBlockPrefab, transform);
            }
            else
            {
                spawnedObstacle = Instantiate(blockPrefab, transform);
            }
            //spawnedObstacle = Instantiate(blockPrefab, transform);
            spawnedObstacle.name = "Block " + xPos + " - " + zPos;
            spawnedObstacle.transform.position = new Vector3(xPos, 1, zPos);
        }
        else
        {
            Vector3 rotate = transform.eulerAngles;
            rotate.y = 0;
            transform.rotation = Quaternion.Euler(rotate);
            foodPrefab.GetComponent<ObstacleObject>().hp = Random.Range(1, 5);
            spawnedObstacle = Instantiate(foodPrefab, transform);
            spawnedObstacle.name = "Food " + xPos + " - " + zPos;
            spawnedObstacle.transform.position = new Vector3(xPos, 0.75f, zPos);
        }

        spawnedObstacle.transform.SetParent(resourceHolder);

    }
    
    public void SpawnWall(float xPos, float zPos)
    {
        GameObject spawnedWall = null;

        //spawnedWall = Instantiate(wallPrefab);
        spawnedWall = Instantiate(FencePrefabs[randomFencePrefab], transform);
        Vector3 rotate = transform.eulerAngles;
        rotate.y = 90;
        transform.rotation = Quaternion.Euler(rotate);
        spawnedWall.name = "Wall " + xPos + " - " + zPos;
        spawnedWall.transform.position = new Vector3(xPos + 0.5f, 0.4f, zPos - 0.5f);
        spawnedWall.transform.SetParent(wallHolder);

    }

    public void SpawnEdgeWall(float xPos, float zPos)
    {
        GameObject spawnedWall = null;

        //spawnedWall = Instantiate(wallPrefab);
        spawnedWall = Instantiate(FencePrefabs[randomFencePrefab], transform);
        Vector3 rotate = transform.eulerAngles;
        rotate.y = 0;
        transform.rotation = Quaternion.Euler(rotate);
        spawnedWall.name = "Wall " + xPos + " - " + zPos;
        spawnedWall.transform.position = new Vector3(xPos + 0.5f, 0.4f, zPos - 0.5f);
        spawnedWall.transform.SetParent(wallHolder);

    }

    public void SpawnNature(float xNature, float yNature, float zNature)
    {
        GameObject sawnedNature = null;
        Vector3 rotate = transform.eulerAngles;
        rotate.y = Random.Range(0, 360);
        transform.rotation = Quaternion.Euler(rotate);
        if(randomBiome == 0)
        {
            randomNaturePrefab = Random.Range(0, 22);
            sawnedNature = Instantiate(NaturePrefabs[randomNaturePrefab], transform);
        }
        if(randomBiome == 1)
        {
            randomDesertPrefab = Random.Range(0, 15);
            sawnedNature = Instantiate(DesertPrefabs[randomDesertPrefab], transform);
        }
        if (randomBiome == 2)
        {
            randomSnowPrefab = Random.Range(0, 11);
            sawnedNature = Instantiate(SnowPrefabs[randomSnowPrefab], transform);
        }

        sawnedNature.name = "Nature " + xNature + " - " + zNature;
        sawnedNature.transform.position = new Vector3(xNature, 0.5f, zNature);
        sawnedNature.transform.SetParent(natureHolder);

    }


}
