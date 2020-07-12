using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabbable : MonoBehaviour
{
    public Rigidbody myRigidbody;
    public bool rotateToOriginWhenGrabbed;
    public bool IsThisAWand;

    [HideInInspector]
    public GrabBehaviour connectedGrabBehaviour;
    Coroutine setConnectedGrabBehaviourCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        if (myRigidbody == null)
        {
            myRigidbody = GetComponent<Rigidbody>();
        }

        connectedGrabBehaviour = null;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (connectedGrabBehaviour != null)// aggiungere che deve essere un anima e devi avere la bacchetta
        {
            myRigidbody.AddForce(connectedGrabBehaviour.objectVelocityToSet);
            myRigidbody.AddTorque(connectedGrabBehaviour.currentHandRotation * connectedGrabBehaviour.objectVelocityToSet.magnitude);
        }
    }

    public void SetConnectedGrabBehaviour(GrabBehaviour inHandGrabBehaviour)
    {
        if(setConnectedGrabBehaviourCoroutine != null)
        {
            StopCoroutine(setConnectedGrabBehaviourCoroutine);
        }
        setConnectedGrabBehaviourCoroutine = StartCoroutine(setConnectedGrabBehaviourEnumerator());
        connectedGrabBehaviour = inHandGrabBehaviour;
    }

    IEnumerator setConnectedGrabBehaviourEnumerator()
    {
        yield return new WaitForSeconds(1);  //potrebbe causare errori con il rallentamento del tempo
        connectedGrabBehaviour = null;
        yield return null;
    }
}
