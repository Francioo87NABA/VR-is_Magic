using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenùManager : MonoBehaviour
{
    public static MenùManager Singleton;

    public GameObject dialogo;
    public GameObject musica;

    public GameObject player;

    public bool newGameee;

    public bool oneTime;

    private void Awake()
    {
        Singleton = this;
    }

    private void Start()
    {
        musica.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (newGameee)
        {
            StartCoroutine(NewGame());
        }
    }

    IEnumerator NewGame()
    {
        musica.SetActive(false);
        dialogo.SetActive(true);
        yield return new WaitForSeconds(45f);
        Destroy(player);
        SceneManager.LoadScene(1);
    }
}
