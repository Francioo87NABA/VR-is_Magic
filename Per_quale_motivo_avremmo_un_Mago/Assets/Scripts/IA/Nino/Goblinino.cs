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
    }

    // Update is called once per frame
    void Update()
    {
        myAgent.SetDestination(AiTarget.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Portone"))
        {
            if (myAnimator != null)
            {
                myAnimator.SetBool("Attack", true);
            }
            else
            {
                Debug.LogError("The animator is NULL");
            }
        }

        if (other.CompareTag("Magia"))
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
