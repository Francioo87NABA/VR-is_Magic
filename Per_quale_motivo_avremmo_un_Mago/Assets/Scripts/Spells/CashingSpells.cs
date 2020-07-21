using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CashingSpells : MonoBehaviour
{
    public bool isThisFireBall;
    public bool isThisGigaFireBall;
    public bool isThisFulmine;

    private void Start()
    {
        if (isThisFireBall)
        {
            SpellManager.Singleton.cashedFireBall = this.gameObject;
        }

        if (isThisGigaFireBall)
        {
            SpellManager.Singleton.cashedGigaFireBall = this.gameObject;
        }

        if (isThisFulmine)
        {
            SpellManager.Singleton.cashedFireBall = this.gameObject;
        }
    }
}
