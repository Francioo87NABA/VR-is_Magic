using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellManager : MonoBehaviour
{
    public static SpellManager Singleton;

    public int mana = 3;

    [Header("Maggie")]
    public bool celestialIsCasted;
    public GameObject fireBall;
    public Transform spellCasterCenter;

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
            for (int i = 0; i < 1; i++)
            {
                Instantiate(fireBall, spellCasterCenter.position, spellCasterCenter.rotation);
            }
                
        }
    }
}
