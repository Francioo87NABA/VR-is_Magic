using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teletrasporto : MonoBehaviour
{
    public Transform teleportPoint;
    public bool segnaletica;
    public GameObject segnaleticaTorre;

    private void Update()
    {
        if (segnaletica)
        {
            StartCoroutine(Segnalazione());
        }
    }

    IEnumerator Segnalazione()
    {
        segnaleticaTorre.SetActive(true);
        yield return new WaitForSecondsRealtime(1f);
        segnaleticaTorre.SetActive(false);
        segnaletica = false;
    }
}
