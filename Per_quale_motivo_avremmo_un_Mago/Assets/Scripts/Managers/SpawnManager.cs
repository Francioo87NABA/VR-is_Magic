using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager Singleton;

    public Transform[] spawnPointsTorre1;
    public Transform[] spawnPointsTorre2;
    public Transform[] spawnPointsTorre3;

    [Header("Enemy")]
    public GameObject enemyContainerTorre1;
    public GameObject enemyContainerTorre2;
    public GameObject enemyContainerTorre3;
    public GameObject nino;
    public bool aspetta;

    public bool stopSpawning;

    public GameObject player;

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
        if (stopSpawning)
        {
            StartCoroutine(HaiVinto());
        }
    }

    IEnumerator EnemySpawningPeriod()
    {
        yield return new WaitForSecondsRealtime(3f);

        while (stopSpawning == false)
        {
            //1* ondata torre1
            for (int i = 0; i < 10; i++)
            {
                int where = Random.Range(0, spawnPointsTorre1.Length);

                GameObject newEnemy = Instantiate(nino, spawnPointsTorre1[where].position, Quaternion.identity);

                newEnemy.transform.parent = enemyContainerTorre1.transform;

                yield return new WaitForSecondsRealtime(10f);
            }

            //2* ondata torre2
            for (int i = 0; i < 10; i++)
            {
                int where = Random.Range(0, spawnPointsTorre2.Length);

                GameObject newEnemy = Instantiate(nino, spawnPointsTorre2[where].position, Quaternion.identity);

                newEnemy.transform.parent = enemyContainerTorre2.transform;

                yield return new WaitForSecondsRealtime(8f);
            }

            //3* ondata torre3
            for (int i = 0; i < 10; i++)
            {
                int where = Random.Range(0, spawnPointsTorre3.Length);

                GameObject newEnemy = Instantiate(nino, spawnPointsTorre3[where].position, Quaternion.identity);

                newEnemy.transform.parent = enemyContainerTorre3.transform;

                yield return new WaitForSecondsRealtime(6f);
            }

            Debug.Log("haivinto");

            stopSpawning = true;

            yield return new WaitForSecondsRealtime(1f);
        }
    }

    IEnumerator HaiVinto()
    {
        //Fai qualcosa che simboleggia la vittoria
        yield return new WaitForSecondsRealtime(5f);
        Destroy(player);
        SceneManager.LoadScene(0);
    }
}
