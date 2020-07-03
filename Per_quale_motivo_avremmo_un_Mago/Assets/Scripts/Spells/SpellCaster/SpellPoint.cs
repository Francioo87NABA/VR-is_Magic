using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellPoint : MonoBehaviour
{
    public SpellCaster connectionToSpellCaster;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hand"))
        {
            connectionToSpellCaster.AddSpellPointToSequence(this);
        }
    }
}
