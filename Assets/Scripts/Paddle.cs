using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    public float speed; //Geschwindkeit als units pro Sekunde
    public Transform floor; //Verlinkung Paddle auf Floor
    public Ball ball; 
    public bool poweredupSU = false; //aktueller Boost-Status SU
    public bool poweredupSD = false; //aktueller Boost-Status SD

    // Start is called before the first frame update
    void Start()
    {
        
    }

    //Paddle trifft Powerup
    private void OnTriggerEnter(Collider other)
    {
        //Powerup(Sizeup)
        if (other.transform.CompareTag("PowerupSU"))
        {
            GameManager.Instance.PowerupSU.gameObject.SetActive(false); //Powerup unsichtbar
            gameObject.transform.localScale = new Vector3(3f,0.5f,0.25f);
            poweredupSU = true;
            GameManager.Instance.timer = 0f; //set Timer auf 0
        }
        //Powerup(Slowdown)
        if (other.transform.CompareTag("PowerupSD"))
        {
            GameManager.Instance.PowerupSD.gameObject.SetActive(false); //Powerup unsichtbar
            ball.maxX = 2; //Ball Geschwindigkeit runtersetzen
            ball.maxZ = 2;
            poweredupSD = true;
            GameManager.Instance.timer = 0f; //set Timer auf 0
        }


    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.alive)
        {
            float dir = Input.GetAxis("Horizontal"); //x-Achsen-Position
                                                     //Grenzen Paddlebewegung festlegen
            float maxX = floor.localScale.x * 10f * 0.5f - transform.localScale.x * 0.5f;
            float posX = transform.position.x + dir * speed * Time.deltaTime;
            float clampedX = Mathf.Clamp(posX, -maxX, maxX);
            transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);

            //Position Paddle
            transform.position += new Vector3(dir * speed * Time.deltaTime, 0, 0);

            if (GameManager.Instance.timer >= 10f) //wenn Timer Zeit überschreitet
            {
                if(poweredupSU)
                {
                    gameObject.transform.localScale = new Vector3(2f, 0.5f, 0.25f); //Größe zurücksetzen
                    poweredupSU = false;
                }
                else if (poweredupSD)
                {
                    ball.maxX = 4; //Geschwindigkeit zurücksetzen
                    ball.maxZ = 4;
                    poweredupSD = false;
                }
            }
        }
    }
}
