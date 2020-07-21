using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistruggiMagie : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ground"))
        {
            Destroy(gameObject, 0.5f);
        }
    }
}
