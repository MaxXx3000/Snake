using TMPro;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.Tilemaps;

public class SnakeMovement : MonoBehaviour
{
    private float ForwardSpeed;
    public float Sensitivity;

    public int Length;
    public GameManager GameManager;
    public Game Game;
    public SnakeTail SnakeTail;
    public ObstacleObject ObstacleObject;

    public TextMeshPro PointsText;

    private Camera mainCamera;
    private Rigidbody componentRigidbody;
    private SnakeTail componentSnakeTail;
    public AudioSource stone;
    public ParticleSystem stoneCrush;
    public ParticleSystem foodCrush;

    private Vector3 touchLastPos;
    private float sidewaysSpeed;

    public Text TextScore;
    public Player Player; 
    public Text TextScoreInGame;

    public Rigidbody Rigidbody;
    public GameObject currentBlock;
    public int currentBlockhp;

    public int damage = 0;
    public int grow = 0;
    int i = 0;
    private float timer;

    private void Awake()
    {
        sidewaysSpeed = 0;
    }
    private void Start()
    {
        
        mainCamera = Camera.main;
        componentRigidbody = GetComponent<Rigidbody>();
        componentSnakeTail = GetComponent<SnakeTail>();
        if (Game.LevelIndex == 1)
        {
            Length = 4;
            ForwardSpeed = 2;
        }
        else
        {
            Length = Game.SnakeLenght;
            ForwardSpeed = 2 + ((Game.LevelIndex % 9) / 2);
            Debug.Log("ForwardSpeed= " + ForwardSpeed);
        }
        for (int i = 0; i < Length; i++) componentSnakeTail.AddStartCircle();

        PointsText.SetText(Length.ToString());        
    }
    
    private void Update()
    {
        if (mainCamera == null)
            mainCamera = Camera.main;
        if (Input.GetMouseButtonDown(0))
        {
            touchLastPos = mainCamera.ScreenToViewportPoint(Input.mousePosition);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            sidewaysSpeed = 0;
        }
        else if (Input.GetMouseButton(0))
        {
            Vector3 delta = (Vector3) mainCamera.ScreenToViewportPoint(Input.mousePosition) - touchLastPos;
            sidewaysSpeed += delta.x * Sensitivity;
            touchLastPos = mainCamera.ScreenToViewportPoint(Input.mousePosition);
        }
        GameManager.TextScore.text = (Game.Score + CurrentScore).ToString();
        TextScoreInGame.text = GameManager.TextScore.text;
    }

    public int CurrentScore;
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Food")
        {
            grow = collision.gameObject.GetComponent<ObstacleObject>().hp;
            //Debug.Log("Food");
            foodCrush.Play();
            Destroy(collision.gameObject);
            Length += grow;
            CurrentScore += grow;
            componentSnakeTail.AddCircle();
            PointsText.SetText(Length.ToString());
        }
        else if (collision.gameObject.tag == "Block")
        {
            currentBlock = collision.gameObject;
            damage = collision.gameObject.GetComponent<ObstacleObject>().hp;
            ObstacleObject.hp = damage;            
            //Debug.Log("Block");
            timer += Time.deltaTime;
            
            if (timer > 0.1f)
            {
                Damage();
            }
        }
        else if (collision.gameObject.tag == "Finish")
        {
            Player.ReachFinish();
        }        
    }

    public void Damage()
    {
        if (Length > 0)
        {
            ObstacleObject.ChangeBlock();
            currentBlock.gameObject.GetComponent<ObstacleObject>().hp--;
            Length--;
            componentSnakeTail.RemoveCircle();
            currentBlock.GetComponent<ObstacleObject>()._text.text = ObstacleObject.hp.ToString();
            stone.Play();
            stoneCrush.Play();
            PointsText.SetText(Length.ToString());
            timer = 0;
            if (ObstacleObject.floatHP > 0)
            {
                currentBlock.GetComponent<Renderer>().material.SetFloat("_FloatHP", ObstacleObject.floatHP);
            }
        }

        if (Length < 1)
        {
            Rigidbody.velocity = Vector3.zero;
            Destroy(Game.scorePanelObject);
            Game.looseObject.gameObject.SetActive(true);
            Game.statObject.gameObject.SetActive(true);
            //Game.scorePanelObject.gameObject.SetActive(false);
            Time.timeScale = 0f;
            //Player.Die();
        }

        if (ObstacleObject.hp == 1)
        {
            Destroy(currentBlock);
        }
    }

    private void FixedUpdate()
    {
        if (Mathf.Abs(sidewaysSpeed) > 4) sidewaysSpeed = 4 * Mathf.Sign(sidewaysSpeed);
        componentRigidbody.velocity = new Vector3(sidewaysSpeed * 5, 0, ForwardSpeed);

        sidewaysSpeed = 0;
    }    
}

