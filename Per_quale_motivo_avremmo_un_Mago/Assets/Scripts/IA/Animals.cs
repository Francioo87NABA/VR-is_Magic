using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Animals : MonoBehaviour
{
    public Transform AiTarget;

    public Animator myAnimator;
    public NavMeshAgent myAgent;

    // Start is called before the first frame update
    void Start()
    {
        AiTarget = gameObject.GetComponentInParent<EnemyContainer>().destinazioneAIAnimali;
        myAgent.SetDestination(AiTarget.position);
    }

}
