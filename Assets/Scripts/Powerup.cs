using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    public Vector3 velocity;
    public float maxZ;  //max Geschwindigkeit z-Richtung

    // Start is called before the first frame update
    void Start()
    {
    }

    //Powerup trifft ins Aus
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Out"))
        {
            gameObject.SetActive(false);
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
