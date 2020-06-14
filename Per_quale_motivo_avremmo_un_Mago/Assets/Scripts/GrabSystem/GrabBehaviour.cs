using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabBehaviour : MonoBehaviour
{
    List<Grabbable> grabbableInHand = new List<Grabbable>();

    // Start is called before the first frame update
    void Start()
    {
        InputManager.Singleton.grabEvent.AddListener(GrabObjectInHand);
        InputManager.Singleton.grabReleasEvent.AddListener(ReleasObjectInHand);
    }

    // Update is called once per frame
    void Update()
    {
        

    }

    void GrabObjectInHand()
    {
        for (int i = 0; i < grabbableInHand.Count; i++)
        {
            grabbableInHand[i].myRigidbody.isKinematic = true;
            grabbableInHand[i].transform.parent = transform;
        }
    }

    void ReleasObjectInHand()
    {
        for (int i = 0; i < grabbableInHand.Count; i++)
        {

            grabbableInHand[i].transform.parent = null;
            grabbableInHand[i].myRigidbody.isKinematic = false;
        }
    }

    private void OnTriggerEnter(Collider collidedObject)
    {
        Grabbable tempGrabbable = collidedObject.gameObject.GetComponent<Grabbable>();

        if (tempGrabbable != null && !grabbableInHand.Contains(tempGrabbable))
        {
            grabbableInHand.Add(tempGrabbable);
        }
    }

    private void OnTriggerExxit(Collider collidedObject)
    {
        Grabbable tempGrabbable = collidedObject.gameObject.GetComponent<Grabbable>();

        if (tempGrabbable != null && grabbableInHand.Contains(tempGrabbable))
        {
            grabbableInHand.Remove(tempGrabbable);
        }
    }
}
