using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicWand : MonoBehaviour
{
    SoulGrabbable distanceGrabbableSoul;
    Vector3 currentHand3dSpeed;
    Vector3 currentHandRotation;
    public float soulAttractionForce = 50f;
    Vector3 objectVelocityToSet;
    Vector3 transformDirection;

    SoulGrabbable actualGrabbableObject;

    // Start is called before the first frame update
    //void Start()
    //{
    //    InputManager.Singleton.pullEvent.AddListener();
    //    InputManager.Singleton.pullEvent.AddListener();

    //    StartCoroutine(CalculateTransformSpeedEnumerator());
    //    StartCoroutine(CalculateTransformRotationEnumerator());

    //    oldDistance = Vector3.Distance(transform.position, InputManager.Singleton.head.position);

    //}

    // Update is called once per frame
    //void FixedUpdate()
    //{
    //    if (distanceGrabbableSoul != null && InputManager.Singleton.SoulGrabBool == true)
    //    {
    //        distanceGrabbableSoul.myRigidbody.AddForce(currentHand3dSpeed * soulAttractionForce);
    //    }

    //    UpdateObjectVelocity();

    //    RayCastManagement();
    //}

    private void RayCastManagement()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
        {
            SoulGrabbable tempGrabbable = hit.collider.gameObject.GetComponent<SoulGrabbable>();
            if (tempGrabbable != null)
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.red);
                actualGrabbableObject = tempGrabbable;

                //if (IsRightHand)
                //{
                //    GameManager.Singleton.rightHandSelectedObject = actualGrabbableObject;
                //}
                //else
                //{
                //    GameManager.Singleton.leftHandSelectedObject = actualGrabbableObject;
                //}
            }
        }
        //else
        //{
        //    actualGrabbableObject = null;
        //    if (IsRightHand)
        //    {
        //        GameManager.Singleton.rightHandSelectedObject = null;
        //    }
        //    else
        //    {
        //        GameManager.Singleton.leftHandSelectedObject = null;
        //    }
        //    Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.blue);
        //}
    }

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
}
