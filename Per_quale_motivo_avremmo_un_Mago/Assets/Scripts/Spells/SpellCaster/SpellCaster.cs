using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellCaster : MonoBehaviour
{
    Spell[] actualSpells;
    public SpellPoint[] allSpellPoints;
    public List<SpellPoint> actualSpellPointsSequence = new List<SpellPoint>();

    void Start()
    {
        actualSpells = GetComponents<Spell>();
        StartCoroutine(CeckForSpells());

        for (int i = 0; i < allSpellPoints.Length; i++)
        {
            allSpellPoints[i].connectionToSpellCaster = this;
        }
    }

    void Update()
    {

    }

    public void AddSpellPointToSequence(SpellPoint spellPointToAdd)
    {
        actualSpellPointsSequence.Add(spellPointToAdd);
    }

    IEnumerator CeckForSpells()
    {
        while (true)
        {
            for (int i = 0; i < actualSpells.Length; i++)
            {
                bool tempSpellCasted = true;
                for (int pointIndex = 0; pointIndex < actualSpells[i].spellPointsSequence.Length; pointIndex++)
                {
                    if (actualSpells[i].spellPointsSequence.Length == actualSpellPointsSequence.Count)
                    {
                        if (actualSpells[i].spellPointsSequence[pointIndex] == actualSpellPointsSequence[pointIndex])
                        {

                        }
                        else
                        {
                            tempSpellCasted = false;
                        }
                    }
                    else
                    {
                        tempSpellCasted = false;
                    }
                }
                if (tempSpellCasted == true)
                {
                    actualSpells[i].transform.position = transform.position;
                    actualSpells[i].SpellCasted = true;                
                    actualSpells[i].CastTheSpell();
                    InputManager.Singleton.oneTime = 0;
                    Destroy(gameObject);
                }
            }          
            yield return new WaitForFixedUpdate();
        }
    }
}
