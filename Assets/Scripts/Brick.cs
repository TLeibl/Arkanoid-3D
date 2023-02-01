using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    public int hitpoints; //Hitpoints, die notwendig sind, um Brick zu zerstören (wenn 0 wird Brick zerstört)
    private int wsl1; //Dropwahrscheinlichkeit 
    private int wsl2; //Wahrscheinlichkeit für Powerup-Art
    


    // Start is called before the first frame update
    void Start()
    {
        wsl1 = Random.Range(1, 8); //(1: Drop, 2-7: kein Drop)
        wsl2 = Random.Range(1, 3); //(1: SU, 2: SD)
    }


    // Update is called once per frame
    void Update()
    {
        //wenn Hitpoints 0 erreichen
        if(hitpoints == 0)
        {
            //lässt Brick verschwinden
            gameObject.SetActive(false);
            GameManager.Instance.anzBricks--; //Brick von Anzahl verbleibender Bricks abziehen
            //droppt bei Zerstörung mit einer gewissen Wahrscheinlichkeit (Wert von 1) ein Powerup
            if (wsl1 == 1)
            {
                DropPowerup(); 
            }
        }

    }

    //Powerup-Drop
    private void DropPowerup()
    {
        //wenn Powerup(Sizeup)
        if (wsl2 == 1)
        {
            //an Pos von Brick spawnen
            GameManager.Instance.PowerupSU.gameObject.transform.position = gameObject.transform.position;
            GameManager.Instance.PowerupSU.gameObject.SetActive(true);
            //Bewegung nach unten
            GameManager.Instance.PowerupSU.velocity = new Vector3(0, 0, -(GameManager.Instance.PowerupSU.maxZ));
        }
        //wenn Powerup(Slowdown)
        if (wsl2 == 2)
        {
            //an Pos von Brick spawnen
            GameManager.Instance.PowerupSD.gameObject.transform.position = gameObject.transform.position;
            GameManager.Instance.PowerupSD.gameObject.SetActive(true);
            //Bewegung nach unten
            GameManager.Instance.PowerupSD.velocity = new Vector3(0, 0, -(GameManager.Instance.PowerupSD.maxZ));
        }
    }
}
