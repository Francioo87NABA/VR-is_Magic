using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour
{
    public string spellName;
    public SpellPoint[] spellPointsSequence;
    public bool SpellCasted;

    public void CastTheSpell()
    {
        if (SpellManager.Singleton != null)
        {
            SpellManager.Singleton.CastTheSpell(this);           
        }
    }
}
