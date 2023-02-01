using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text playerScore; //Punktzahl des Spielers
    public Text GO; //Game Over Text
    public Text YW; //You Won Text
    public GameObject Background; //Background for Texts
    public int countScore; //Punktzahl Counter
    public int currentLife; //the current left life (3, 2, 1 or 0 (game end))
    public bool alive = true; //wenn Spieler noch lebt
    public int anzBricks; //Anzahl verbleibende Bricks
    public float timer; //Timer für die Powerup-Zeit
    public Powerup PowerupSU;
    public Powerup PowerupSD;
    private static GameManager instance;

    public static GameManager Instance
    {
        get
        {
            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        //Messages not visible
        GO.enabled = false;
        YW.enabled = false;
        Background.gameObject.SetActive(false);
        //Powerups not visible
        PowerupSU.gameObject.SetActive(false);
        PowerupSD.gameObject.SetActive(false);
    }

    void Start()
    {
        //set Player Score
        playerScore.text = countScore.ToString();

    }

    void Update()
    {
        //update Player Score
        playerScore.text = countScore.ToString();
        //update timer
        timer += Time.deltaTime;

        //wenn alle Bricks zerstört - gewonnen!
        if (anzBricks == 0)
        {
            //Bewegungen Ball + Paddle aus
            GameManager.Instance.alive = false;
            //Nachricht You won!
            YW.enabled = true;
            Background.gameObject.SetActive(true);
        }
    }
}