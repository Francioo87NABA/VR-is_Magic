using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellManager : MonoBehaviour
{
    public static SpellManager Singleton;

    private void OnEnable()
    {
        Singleton = this;
    }

    public void CastTheSpell(Spell spellToCast)
    {
        print(spellToCast.spellName);
    }
}
