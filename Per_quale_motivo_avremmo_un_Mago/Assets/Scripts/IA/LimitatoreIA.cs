using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitatoreIA : MonoBehaviour
{
    public List<Goblinino> goblininos = new List<Goblinino>();

    void Update()
    {
        if (goblininos.Count >= 3)
        {
            SpawnManager.Singleton.aspetta = true;
        }
        else
        {
            SpawnManager.Singleton.aspetta = false;
        }

        goblininos.RemoveAll(Goblinino => Goblinino == null);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            goblininos.Add(other.GetComponent<Goblinino>());
        }
    }
}
