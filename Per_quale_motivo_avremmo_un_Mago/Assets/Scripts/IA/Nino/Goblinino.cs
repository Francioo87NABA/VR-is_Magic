﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Goblinino : MonoBehaviour
{
    public Transform AiTarget;
    public Transform deviAspettareQui;

    string[] attackTriggers = { "BackAttack", "DownAttack", "HorizontalAttack", "ComboAttack2", "ComboAttack", "KickAttack" };
    string[] tauntTriggers = { "Taunt", "Taunt2" };

    float minTimer = 5;
    float maxTimer = 20;
    float actualTimer = 1;

    bool seiInIdle;
    bool aspettateSoloVoi;

    public GameObject portone;

    public Animator myAnimator;
    NavMeshAgent myAgent;

    public GameObject animale;
    public bool metamorfosi;


    void Start()
    {
        AiTarget = gameObject.GetComponentInParent<EnemyContainer>().destinazioneAI;
        portone = gameObject.GetComponentInParent<EnemyContainer>().portoneDaSfondare;
        //deviAspettareQui = gameObject.GetComponentInParent<EnemyContainer>().deviAspettareProprioQui;
        myAgent = GetComponent<NavMeshAgent>();
        myAgent.SetDestination(AiTarget.position);
    }


    private void Update()
    {
        //if (ControlloDellaMente)
        //{
        //    myAgent.SetDestination(portone.transform.position);

        //    myAnimator.SetBool("Attack", true);

        //    int RandomAttack = Random.Range(0, attackTriggers.Length);
        //    string attackTrigger = attackTriggers[RandomAttack];
        //    myAnimator.SetTrigger(attackTrigger);
        //}

        if (portone == null)
        {
            myAnimator.SetBool("Attack", false);
            myAgent.SetDestination(AiTarget.position);
        }

        if (actualTimer <= 0 && seiInIdle)
        {
            int RandomTaunt = Random.Range(0, tauntTriggers.Length);
            string tauntTrigger = tauntTriggers[RandomTaunt];
            myAnimator.SetTrigger(tauntTrigger);

            actualTimer = Random.Range(minTimer, maxTimer);
        }
        else if (actualTimer > 0 && seiInIdle)
        {
            actualTimer -= Time.deltaTime;
        }

        if (metamorfosi)
        {
            GameObject newAnimal = Instantiate(animale, transform.position, transform.rotation);
            newAnimal.transform.parent = gameObject.GetComponentInParent<EnemyContainer>().transform;
            Destroy(gameObject);
        }
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Portone"))
        {
            Transform portone = other.GetComponent<Transform>();
            myAgent.SetDestination(portone.position);
        }

        //if (other.CompareTag("PuntoDAttesa"))
        //{
        //    if (SpawnManager.Singleton.aspetta && portone != null)
        //    {
        //        myAgent.SetDestination(transform.position);
        //        myAnimator.SetBool("Idle", true);
        //        seiInIdle = true;
        //    }
        //    else if (SpawnManager.Singleton.aspetta == false && portone != null)
        //    {
        //        seiInIdle = false;
        //        myAnimator.SetBool("Idle", false);
        //        myAgent.SetDestination(AiTarget.position);
        //    }
        //    else if (portone == null)
        //    {
        //        seiInIdle = false;
        //        myAnimator.SetBool("Idle", false);
        //        myAgent.SetDestination(AiTarget.position);
        //    }
        //}
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Portone"))
        {
            myAnimator.SetBool("Attack", true);

            int RandomAttack = Random.Range(0, attackTriggers.Length);
            string attackTrigger = attackTriggers[RandomAttack];
            myAnimator.SetTrigger(attackTrigger);
        }

        if (other.CompareTag("Target"))
        {
            myAnimator.SetBool("Idle", true);
            seiInIdle = true;
        }

        if (other.CompareTag("Magia"))
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
