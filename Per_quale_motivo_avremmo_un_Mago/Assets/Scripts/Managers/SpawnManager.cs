using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager Singleton;

    public Transform[] spawnPoints;

    [Header("Enemy")]
    public GameObject enemyContainer;
    public GameObject nino;

    public bool stopSpawning;

    private void OnEnable()
    {
        Singleton = this;
    }

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
            //1* ondata
            for (int i = 0; i < 10; i++)
            {
                int where = Random.Range(0, spawnPoints.Length);

                GameObject newEnemy = Instantiate(nino, spawnPoints[where].position, Quaternion.identity);

                newEnemy.transform.parent = enemyContainer.transform;

                yield return new WaitForSecondsRealtime(4f);
            }

            //2* ondata
            for (int i = 0; i < 10; i++)
            {
                int where = Random.Range(0, spawnPoints.Length);

                GameObject newEnemy = Instantiate(nino, spawnPoints[where].position, Quaternion.identity);

                newEnemy.transform.parent = enemyContainer.transform;

                yield return new WaitForSecondsRealtime(2f);
            }

            //3* ondata
            for (int i = 0; i < 10; i++)
            {
                int where = Random.Range(0, spawnPoints.Length);

                GameObject newEnemy = Instantiate(nino, spawnPoints[where].position, Quaternion.identity);

                newEnemy.transform.parent = enemyContainer.transform;

                yield return new WaitForSecondsRealtime(0.5f);
            }

            Debug.Log("haivinto");

            stopSpawning = true;

            yield return new WaitForSecondsRealtime(1f);
        }
    }
}
