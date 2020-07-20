using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellManager : MonoBehaviour
{
    public static SpellManager Singleton;


    public int mana = 3;

    public Transform rightHandInstantiationTransform;
    public Transform leftHandInstantiationTransform;

    [Header("Maggie")]
    public bool celestialIsCasted;
    public GameObject fireBall;
    public GameObject gigaFireBall;
    public Transform gigaFiraballInstantiation;
    public GameObject fulmine;
    public Transform fulmineInstantiation;
    public GameObject muroEtereo;
    public Transform muroEtereoInstantiationTransform;
    public bool metamorfosi;
    public GameObject proiettile;
    public Transform wandRay;
    public GameObject tornado;
    public Transform tornadoInstantiation;

    //public Transform spellManager;

    private void OnEnable()
    {
        Singleton = this;
    }

    public void CastTheSpell(Spell spellToCast)
    {
        print(spellToCast.spellName);

        if (spellToCast.spellName == "Celestial")
        {
            celestialIsCasted = true;
            mana -= 1;
        }

        if (spellToCast.spellName == "FireBall")
        {        
            Instantiate(fireBall, spellToCast.transform.position, spellToCast.transform.rotation);             
        }

        if (spellToCast.spellName == "GigaFireBall")
        {
            Instantiate(gigaFireBall, spellToCast.transform.position, spellToCast.transform.rotation);
        }

        if (spellToCast.spellName == "MuroEtereo")
        {
            Instantiate(muroEtereo, muroEtereoInstantiationTransform.position, muroEtereoInstantiationTransform.rotation);
            Debug.Log("unavolta");  
        }

        if (spellToCast.spellName == "Fulmine")
        {
            Instantiate(fulmine, fulmineInstantiation.position, fulmineInstantiation.rotation);
        }

        if (spellToCast.spellName == "Metamorfosi")
        {
            StartCoroutine(Metamorfosi());
        }

        if (spellToCast.spellName == "Proiettili")
        {
            StartCoroutine(Proiettili());
        }

        if (spellToCast.spellName == "Tornado")
        {
            StartCoroutine(Tornado());
        }


    }

    IEnumerator Metamorfosi()
    {
        metamorfosi = true;
        yield return new WaitForSecondsRealtime(3f);
        metamorfosi = false;
    }

    IEnumerator Proiettili()
    {
        for (int i = 0; i < 20; i++)
        {
            Instantiate(proiettile, wandRay.position, wandRay.rotation);
            yield return new WaitForSecondsRealtime(0.5f);
        }
    }

    IEnumerator Tornado()
    {
        Instantiate(tornado, tornadoInstantiation.position, tornadoInstantiation.rotation);
        yield return new WaitForSecondsRealtime(5f);
        Destroy(tornado);
    }
}
