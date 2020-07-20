using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Celestial : MonoBehaviour
{
    public float actualTimeScale = 1f;
    public float durataCelestial = 8f;
    public float rallentamentoDelTempo = 0.8f;
    public bool celestial;

    void Update()
    {
        Time.timeScale = actualTimeScale;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;

        if (SpellManager.Singleton.celestialIsCasted == true)
        {
            StartCoroutine(DurataCelestial());
        }
     
    }

    IEnumerator DurataCelestial ()
    {
        while (SpellManager.Singleton.celestialIsCasted == true)
        {
            SpellManager.Singleton.celestialIsCasted = false;
            actualTimeScale = actualTimeScale - rallentamentoDelTempo;
            yield return new WaitForSecondsRealtime(durataCelestial);
            actualTimeScale = actualTimeScale + rallentamentoDelTempo;
        }    
    }
}
