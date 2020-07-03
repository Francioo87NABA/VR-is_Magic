using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    public GameObject fireBall;
    public float explosionRadius, explosionPower, explosionUpwards;
    Ball diopuro;

    // Start is called before the first frame update
    void Start()
    {
        diopuro = GameObject.Find("FireBall").GetComponent<Ball>();
    }

    // Update is called once per frame
    void Update()
    {
        if (diopuro.esplodifiglioditroia == true)
        {
            Explosion();
            Debug.Log("rizz");
        }
    }


    private void Explosion()
    {
        Vector3 explosionPosition = fireBall.transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPosition, explosionRadius);

        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.AddExplosionForce(explosionPower, explosionPosition, explosionRadius, explosionUpwards, ForceMode.Impulse);
              
              
            }

        }
    }
}
