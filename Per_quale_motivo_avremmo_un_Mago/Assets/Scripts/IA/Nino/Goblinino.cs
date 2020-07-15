using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Goblinino : MonoBehaviour
{
    public Transform AiTarget;

    string[] attackTriggers = { "BackAttack", "DownAttack", "HorizontalAttack", "ComboAttack2", "ComboAttack", "KickAttack" };
    string[] tauntTriggers = { "Taunt", "Taunt2" };

    float minTimer = 7;
    float maxTimer = 20;
    float actualTimer = 5;

    public GameObject portone;

    public Animator myAnimator;
    NavMeshAgent myAgent;

  
    // Start is called before the first frame update
    void Start()
    {
        myAgent = GetComponent<NavMeshAgent>();
        myAgent.SetDestination(AiTarget.position);
    }

    private void Update()
    {
        if (portone == null)
        {
            myAnimator.SetBool("Attack", false);
            myAgent.SetDestination(AiTarget.position);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Portone"))
        {

            Transform portone = other.GetComponent<Transform>();
            myAgent.SetDestination(portone.position);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Portone"))
        {
            myAgent.SetDestination(transform.position);

            myAnimator.SetBool("Attack", true);

            int RandomAttack = Random.Range(0, attackTriggers.Length);
            string attackTrigger = attackTriggers[RandomAttack];
            myAnimator.SetTrigger(attackTrigger);

        }

        if (other.CompareTag("Target"))
        {
            myAnimator.SetBool("Idle", true);

            if (actualTimer <= 0)
            {
                int RandomTaunt = Random.Range(0, tauntTriggers.Length);
                string tauntTrigger = tauntTriggers[RandomTaunt];
                myAnimator.SetTrigger(tauntTrigger);

                actualTimer = Random.Range(minTimer, maxTimer);
            }
            else
            {
                actualTimer -= Time.deltaTime;
            }
            
        }

        if (other.CompareTag("Magia"))
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //if (other.CompareTag("Portone"))
        //{
        //    myAnimator.SetBool("Attack", false);
        //    myAgent.SetDestination(AiTarget.position);
        //}
    }
}
