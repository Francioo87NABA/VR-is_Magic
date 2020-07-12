using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public Transform[] spawnPoints;

    public GameObject nino;

    public GameObject enemyContainer;

    public bool stopSpawning;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(EnemySpawningPeriod());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator EnemySpawningPeriod()
    {
        yield return new WaitForSecondsRealtime(3f);

        while (stopSpawning == false)
        {
            int where = Random.Range(0, spawnPoints.Length);

            GameObject newEnemy = Instantiate(nino, spawnPoints[where].position, Quaternion.identity);

            newEnemy.transform.parent = enemyContainer.transform;

            yield return new WaitForSecondsRealtime(3f);
        }
    }
}
