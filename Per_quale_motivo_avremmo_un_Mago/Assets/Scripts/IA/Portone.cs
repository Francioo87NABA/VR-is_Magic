using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portone : MonoBehaviour
{
    public int vita;

    private int danno;

    public GameObject padre;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (vita <= 0)
        {
            SpawnManager.Singleton.stopSpawning = true;
            InputManager.Singleton.gameOver = true;
            Destroy(padre);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Arma"))
        {
            danno = other.gameObject.GetComponent<Arma>().dannoArma;
            vita = vita - danno;
        }
    }
}
