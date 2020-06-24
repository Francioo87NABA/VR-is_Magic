using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTeleporter : MonoBehaviour
{
    public Transform enemyTransform;
    public Transform reciever;

    private bool playerIsOverlapping;

    // Update is called once per frame
    void Update()
    {
        if (playerIsOverlapping == true)
        {
            Vector3 portalToPlayer = enemyTransform.position - transform.position;
            float dotProduct = Vector3.Dot(transform.up, portalToPlayer);

            if (dotProduct < 0f)
            {
                //PER fare un portale attraversabile e viceversa togliere dal commeno queste righe e mettere questo script sul secondo reciver

                //float rotationDiff = -Quaternion.Angle(transform.rotation, reciever.rotation);
                //rotationDiff += 180;
                //player.Rotate(Vector3.up, rotationDiff);

                //e mettere nella riga sotto Quaternion.Euler( 0f, rotationdiff, 0f)
                Vector3 positionOffset = Quaternion.identity * portalToPlayer;
                enemyTransform.position = reciever.position + positionOffset;

                playerIsOverlapping = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemyTransform = other.GetComponent<Transform>();
            playerIsOverlapping = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            playerIsOverlapping = false;
        }
    }
}
