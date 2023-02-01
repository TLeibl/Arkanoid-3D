using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Vector3 velocity;
    public float maxX;  //max Geschwindigkeit x- und z-Richtung
    public float maxZ;
    public Paddle paddle;

    // Start is called before the first frame update
    void Start()
    {
        velocity = new Vector3(0, 0, maxZ); //Ball anstoßen
    }

    //Ball trifft etwas
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Paddle"))
        {
            //Maximaldistanz (halber Schläger * halbe Kugel)
            float maxDist = 0.5f * other.transform.localScale.x + 0.5f * transform.localScale.x;
            //tatsächliche Distanz
            float dist = transform.position.x - other.transform.position.x;
            //normalisierte Distanz zur Berechnung bei Aufprall Ball
            float nDist = dist / maxDist;
            velocity = new Vector3(nDist * maxX, velocity.y, -velocity.z); //umso weiter außen Ball getroffen, desto größer Ausfallwinkel

            gameObject.GetComponent<AudioSource>().Play(); //spiele Sound
        }
        else if (other.transform.CompareTag("Wall"))
        {
            velocity = new Vector3(-velocity.x, velocity.y, velocity.z);
            gameObject.GetComponent<AudioSource>().Play(); //spiele Sound

        }
        else if (other.transform.CompareTag("WallTop"))
        {
            velocity = new Vector3(velocity.x, velocity.y, -velocity.z);
            gameObject.GetComponent<AudioSource>().Play(); //spiele Sound

        }
        else if (other.transform.CompareTag("Brick"))
        {
            //Maximaldistanz (halber Brick * halbe Kugel)
            float maxDist = 0.5f * other.transform.localScale.x + 0.5f * transform.localScale.x;
            //tatsächliche Distanz
            float dist = transform.position.x - other.transform.position.x;
            //normalisierte Distanz zur Berechnung bei Aufprall Ball
            float nDist = dist / maxDist;
            velocity = new Vector3(nDist * maxX, velocity.y, -velocity.z); //umso weiter außen Ball trifft, desto größer Ausfallwinkel

            gameObject.GetComponent<AudioSource>().Play(); //spiele Sound
            GameManager.Instance.countScore++; //+1 Punkt zum Player-Score
            other.gameObject.GetComponent<Brick>().hitpoints--; //Hitpoints für getroffenes Objekt verringern
        }
        else if (other.transform.CompareTag("Out"))
        {
            //Leben-Objekt zerstören
            if (GameObject.Find("Paddle Life (3)") != null)
            {
                Destroy(GameObject.Find("Paddle Life (3)"));
                GameManager.Instance.currentLife--;
            }
            else if (GameObject.Find("Paddle Life (2)") != null)
            {
                Destroy(GameObject.Find("Paddle Life (2)"));
                GameManager.Instance.currentLife--;
            }
            else if (GameObject.Find("Paddle Life (1)") != null)
            {
                Destroy(GameObject.Find("Paddle Life (1)"));
                GameManager.Instance.currentLife--;
            }
            //wenn kein Leben mehr da: Spiel zu Ende
            if (GameManager.Instance.currentLife == 0)
            {
                //Ball + Paddle wieder auf Anfangsposition 
                gameObject.transform.position = new Vector3(0f, 0.2f, -4.45f);
                paddle.transform.position = new Vector3(0.04f, 0.3f, -4.75f);
                //Bewegungen Ball + Paddle aus
                GameManager.Instance.alive = false;
                //Game-Over-Nachricht ploppt auf 
                GameManager.Instance.GO.enabled = true;
                GameManager.Instance.Background.gameObject.SetActive(true);
            }
            else
            {
                //Ball + Paddle wieder auf Anfangsposition 
                gameObject.transform.position = new Vector3(0f, 0.2f, -4.45f);
                paddle.transform.position = new Vector3(0.04f, 0.3f, -4.75f);
                velocity = new Vector3(0, 0, maxZ);  //Ball anstoßen
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.alive)
        {
            transform.position += velocity * Time.deltaTime;
        }
    }
}
