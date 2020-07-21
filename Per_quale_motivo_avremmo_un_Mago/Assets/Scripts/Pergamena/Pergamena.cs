using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pergamena : MonoBehaviour
{
    public bool play;
    public bool newGame;
    public bool exit;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hand"))
        {
            if (play)
            {
                StartCoroutine(Play());
            }
            else if (newGame)
            {
                StartCoroutine(NewGame());
            }
            else if (exit)
            {
                StartCoroutine(Quit());
            }      
        }
    }

    IEnumerator NewGame()
    {
        //Instanzia l introduzione
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene(1);
    }

    IEnumerator Play()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(1);
    }

    IEnumerator Quit()
    {

        yield return new WaitForSeconds(2f);
        Application.Quit();
    }

}
