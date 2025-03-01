﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuroEtereo : MonoBehaviour
{
    public int vita = 30;

    private int danno;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (vita <= 0)
        {
            Vector3 ofset = new Vector3(0, 100, 0);
            transform.position = transform.position - ofset;
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
