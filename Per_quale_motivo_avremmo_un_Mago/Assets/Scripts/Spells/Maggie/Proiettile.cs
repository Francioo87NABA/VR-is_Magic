using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proiettile : MonoBehaviour
{
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (speed != 0)
        {
            transform.position += transform.forward * (speed * Time.deltaTime);
        }
        else
        {
            Debug.Log("speed is zero");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        speed = 0;

        Destroy(gameObject);
    }
}
