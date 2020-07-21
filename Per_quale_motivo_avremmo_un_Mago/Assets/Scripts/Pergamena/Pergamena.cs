using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pergamena : MonoBehaviour
{
    public bool play;
    public bool newGame;
    public bool exit;

    public GameObject player;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hand"))
        {
            if (play && MenùManager.Singleton.oneTime == false)
            {
                MenùManager.Singleton.oneTime = true;
                StartCoroutine(Play());
            }
            else if (newGame && MenùManager.Singleton.oneTime == false)
            {
                MenùManager.Singleton.oneTime = true;
                MenùManager.Singleton.newGameee = true;
            }
            else if (exit && MenùManager.Singleton.oneTime == false)
            {
                MenùManager.Singleton.oneTime = true;
                StartCoroutine(Quit());
            }      
        }
    }


    IEnumerator Play()
    {
        yield return new WaitForSeconds(2f);
        Destroy(player);
        SceneManager.LoadScene(1);
    }

    IEnumerator Quit()
    {

        yield return new WaitForSeconds(2f);
        Destroy(player);
        Application.Quit();
    }

}
