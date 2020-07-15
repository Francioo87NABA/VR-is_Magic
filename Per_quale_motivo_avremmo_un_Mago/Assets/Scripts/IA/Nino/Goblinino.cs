using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Goblinino : MonoBehaviour
{
    public Transform AiTarget;
    

    public Animator myAnimator;
    NavMeshAgent myAgent;
  
    // Start is called before the first frame update
    void Start()
    {
        myAgent = GetComponent<NavMeshAgent>();
        myAgent.SetDestination(AiTarget.position);
    }

    // Update is called once per frame
    void Update()
    {
    
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Portone"))
        {
            Transform portone = other.GetComponent<Transform>();
            myAgent.SetDestination(portone.position);
            myAnimator.SetBool("Attack", true);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Target"))
        {
            myAnimator.SetBool("Idle", true);
        }

        if (other.CompareTag("Magia"))
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Portone"))
        {
            myAnimator.SetBool("Attack", false);
            myAgent.SetDestination(AiTarget.position);
        }
    }
}
