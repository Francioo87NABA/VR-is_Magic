using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabBehaviour : MonoBehaviour
{
    List<Grabbable> grabbableInHand = new List<Grabbable>();

    SphereCollider myCollider;
    public float closeGrabDistance = 0.1f;

    public Transform wandTransform;
    public Transform wandGrabOrientation;
    public Transform wandReleasOrientation;
    public float soulAttractionForce;

    Vector3 currentHand3dSpeed;

    Vector3 transformDirection;

    public bool isRightHand;

    Grabbable actualGrabbableObject;

    [HideInInspector]
    public Vector3 currentHandRotation;
    [HideInInspector]
    public Vector3 objectVelocityToSet;



    // Start is called before the first frame update
    void Start()
    {
        SetUpTrigger();


        StartCoroutine(CalculateTransformSpeedEnumerator());
        StartCoroutine(CalculateTransformRotationEnumerator());

        oldDistance = Vector3.Distance(transform.position, InputManager.Singleton.head.position);

        InputManager.Singleton.grabEvent.AddListener(GrabObjectInHand);
        InputManager.Singleton.grabReleasEvent.AddListener(ReleasObjectInHand);
        InputManager.Singleton.pullEvent.AddListener(PullObject);
    }

    // Update is called once per frame
    void Update()
    {
        

    }

    private void FixedUpdate()
    {
        UpdateObjectVelocity();

        RayCastManagement();
    }

    #region Inputs
    void GrabObjectInHand()
    {
        for (int i = 0; i < grabbableInHand.Count; i++)
        {
            grabbableInHand[i].myRigidbody.isKinematic = true;
            grabbableInHand[i].transform.parent = transform;

            if (grabbableInHand[i].rotateToOriginWhenGrabbed)
            {
                grabbableInHand[i].transform.localPosition = Vector3.zero;
                grabbableInHand[i].transform.localRotation = Quaternion.identity;

            }
            if (isRightHand)
            {
                if (!GameManager.Singleton.rightHandGrabbedObjects.Contains(grabbableInHand[i]))
                {
                    GameManager.Singleton.rightHandGrabbedObjects.Add(grabbableInHand[i]);
                }
            }
            else
            {
                if (!GameManager.Singleton.leftHandGrabbedObjects.Contains(grabbableInHand[i]))
                {
                    GameManager.Singleton.leftHandGrabbedObjects.Add(grabbableInHand[i]);
                }
            }
        }
    }

    void ReleasObjectInHand()
    {
        Grabbable[] grabbablesGrabbed = new Grabbable[0];
        if (transform.childCount > 0)
        {
            grabbablesGrabbed = transform.GetComponentsInChildren<Grabbable>();
        }

        for (int i = 0; i < grabbableInHand.Count; i++)
        {
            grabbableInHand[i].transform.parent = null;
            grabbableInHand[i].myRigidbody.isKinematic = false;
            grabbableInHand[i].myRigidbody.velocity = currentHand3dSpeed;
            grabbableInHand[i].myRigidbody.AddTorque(currentHandRotation);

            if (isRightHand)
            {
                if (!GameManager.Singleton.rightHandGrabbedObjects.Contains(grabbableInHand[i]))
                {
                    GameManager.Singleton.rightHandGrabbedObjects.Remove(grabbableInHand[i]);
                }
            }
            else
            {
                if (!GameManager.Singleton.leftHandGrabbedObjects.Contains(grabbableInHand[i]))
                {
                    GameManager.Singleton.leftHandGrabbedObjects.Remove(grabbableInHand[i]);
                }
            }
        }
    }

    void PullObject()
    {
        actualGrabbableObject.SetConnectedGrabBehaviour(this);
    }
    #endregion

    #region UpdateObjectVelocity 

    void UpdateObjectVelocity()
    {
        if (CheckIfMovementIsTowardsMe())
        {
            objectVelocityToSet = currentHand3dSpeed * soulAttractionForce;
        }
        else
        {
            objectVelocityToSet = Vector3.zero;
        }
    }

    float oldDistance;

    bool CheckIfMovementIsTowardsMe()
    {
        if (Vector3.Distance(transform.position, InputManager.Singleton.head.position) >= oldDistance)
        {
            oldDistance = Vector3.Distance(transform.position, InputManager.Singleton.head.position);
            return false;
        }
        else
        {
            oldDistance = Vector3.Distance(transform.position, InputManager.Singleton.head.position);
            return true;
        }
    }
    #endregion

    #region RayCastManagement
    private void RayCastManagement()
    {
        RaycastHit hit;

        if (Physics.Raycast(wandTransform.position, wandTransform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
        {
            Grabbable tempGrabbable = hit.collider.gameObject.GetComponent<Grabbable>();
            if (tempGrabbable != null)
            {
                Debug.DrawRay(wandTransform.position, wandTransform.TransformDirection(Vector3.forward) * hit.distance, Color.green);
                actualGrabbableObject = tempGrabbable;

                if (isRightHand)
                {
                    GameManager.Singleton.rightHandSelectedObject = actualGrabbableObject;
                }
                else
                {
                    GameManager.Singleton.leftHandSelectedObject = actualGrabbableObject;
                }
            }
            else
            {
                actualGrabbableObject = null;
                if (isRightHand)
                {
                    GameManager.Singleton.rightHandSelectedObject = null;
                }
                else
                {
                    GameManager.Singleton.leftHandSelectedObject = null;
                }
                Debug.DrawRay(wandTransform.position, wandTransform.TransformDirection(Vector3.forward) * hit.distance, Color.red);
            }
        }
        else
        {
            actualGrabbableObject = null;
            if (isRightHand)
            {
                GameManager.Singleton.rightHandSelectedObject = null;
            }
            else
            {
                GameManager.Singleton.leftHandSelectedObject = null;
            }
            Debug.DrawRay(wandTransform.position, wandTransform.TransformDirection(Vector3.forward) * 1000, Color.white);
        }
    }
    #endregion

    IEnumerator CalculateTransformSpeedEnumerator()
    {
        Vector3 oldPosition = transform.position;

        while (true)
        {
            currentHand3dSpeed = (transform.position - oldPosition) * 60f;
            oldPosition = transform.position;
            yield return new WaitForSecondsRealtime(1f / 60f);
        }
    }

    IEnumerator CalculateTransformRotationEnumerator()
    {
        Vector3 oldRotation = transform.eulerAngles;

        while (true)
        {
            currentHandRotation = (transform.eulerAngles - oldRotation) * 60f;
            oldRotation = transform.eulerAngles;
            yield return new WaitForSecondsRealtime(1f / 60f);
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

    private void OnTriggerExit(Collider collidedObject)
    {
        Grabbable tempGrabbable = collidedObject.gameObject.GetComponent<Grabbable>();

        if (tempGrabbable != null && grabbableInHand.Contains(tempGrabbable))
        {
            grabbableInHand.Remove(tempGrabbable);
        }
    }

    void SetUpTrigger()
    {
        myCollider = GetComponent<SphereCollider>();

        if (myCollider == null)
        {
            myCollider = gameObject.AddComponent<SphereCollider>();
        }

        myCollider.radius = closeGrabDistance;
        myCollider.isTrigger = true;
    }
}
