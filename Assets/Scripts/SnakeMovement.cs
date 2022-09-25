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
    public SnakeMovement CurrentBlock;
    public Player Player;

    public Rigidbody Rigidbody;

    public int damage = 0;
    public int grow = 0;

    private void Awake()
    {
        sidewaysSpeed = 0;
    }
    private void Start()
    {        
        mainCamera = Camera.main;
        componentRigidbody = GetComponent<Rigidbody>();
        componentSnakeTail = GetComponent<SnakeTail>();
        if(Game.LevelIndex == 1)
        {
            Length = 1;
            ForwardSpeed = 2;
        }
        else
        {
            Length = Game.SnakeLenght;
            ForwardSpeed += Game.LevelIndex;
            if(ForwardSpeed > 6)
            {
                ForwardSpeed = 2;
            }
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
        GameManager.TextScore.text = "Score " + (Game.Score + CurrentScore).ToString();
    }

    public int CurrentScore;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Food")
        {
            grow = collision.gameObject.GetComponent<ObstacleObject>().hp;
            Debug.Log("Food");
            foodCrush.Play();
            Destroy(collision.gameObject);
            Length += grow;
            CurrentScore += grow;
            componentSnakeTail.AddCircle();
            PointsText.SetText(Length.ToString());
        }
        else if (collision.gameObject.tag == "Block")
        {
            damage = collision.gameObject.GetComponent<ObstacleObject>().hp;
            stone.Play();
            stoneCrush.Play();
            Debug.Log("Block");
            Length -= damage;                        
            PointsText.SetText(Length.ToString());
            if(Length < 1)
            {
                Rigidbody.velocity = Vector3.zero;
                Player.Die();
            }
            else
            {
                componentSnakeTail.RemoveCircle();
                Destroy(collision.gameObject, 0.3f);
            }

        }
        else if (collision.gameObject.tag == "Finish")
        {
            Player.ReachFinish();
        }        
    }
    private void FixedUpdate()
    {
        if (Mathf.Abs(sidewaysSpeed) > 4) sidewaysSpeed = 4 * Mathf.Sign(sidewaysSpeed);
        componentRigidbody.velocity = new Vector3(sidewaysSpeed * 5, 0, ForwardSpeed);

        sidewaysSpeed = 0;
    }    
}

