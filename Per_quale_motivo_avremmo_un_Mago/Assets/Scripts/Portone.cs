using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portone : MonoBehaviour
{
    public int vita = 100;

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
            Vector3 ofset = new Vector3(0, 0, 100);
            padre.transform.position = padre.transform.position - ofset;
            transform.position = transform.position - ofset;
            SpawnManager.Singleton.stopSpawning = true;
            //InputManager.Singleton.gameOver = true;
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
