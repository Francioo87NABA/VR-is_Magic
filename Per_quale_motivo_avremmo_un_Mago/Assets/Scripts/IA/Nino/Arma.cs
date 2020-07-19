using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arma : MonoBehaviour
{
    public int dannoArma;
    public bool controllaMente;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
  
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") && controllaMente)
        {
            Destroy(other.gameObject);
        }
        
        if (other.CompareTag("Arma") && controllaMente)
        {
            Destroy(gameObject);
        }
    }
}
