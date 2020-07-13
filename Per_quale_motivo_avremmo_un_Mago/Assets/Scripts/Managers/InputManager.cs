using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager Singleton;

    [Header("Input Events")]
    public UnityEvent grabEvent;
    public UnityEvent grabReleasEvent;
    public UnityEvent pullEvent;
    public UnityEvent spellEvent;

    [Header("SpellCaster")]
    public Transform rightHandTransform;
    public Transform leftHandTransform;
    public Transform rightHandInstantiationTransform;
    public Transform leftHandInstantiationTransform;
    public GameObject handSpellCaster;
    public GameObject wandSpellCaster;
    public bool wandInHand;

    [Header("SteamVR References")]
    public Transform head;
    public Hand leftHand;
    public Hand rightHand;

    [Space(20)]
    public SteamVR_Action_Boolean grabAction = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("GrabGrip");
    public SteamVR_Action_Boolean pullAction = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("GrabPinch");
    public SteamVR_Action_Boolean spellAction = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("RightClick");

    private void OnEnable()
    {
        Singleton = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (grabAction.GetStateDown(rightHand.handType))
        {
            grabEvent.Invoke();   
        }
        if (grabAction.GetStateDown(leftHand.handType))
        {
            grabEvent.Invoke();
        }

        if (grabAction.GetStateUp(rightHand.handType))
        {   
            grabReleasEvent.Invoke();
        }
        if (grabAction.GetStateUp(leftHand.handType))
        {
            grabReleasEvent.Invoke();
        }


        if (pullAction.GetStateDown(rightHand.handType) && wandInHand)
        {
            pullEvent.Invoke();
        }
        if (pullAction.GetStateDown(leftHand.handType) && wandInHand)
        {
            pullEvent.Invoke();
        }


        if (spellAction.GetStateDown(rightHand.handType) && wandInHand == false)
        {
            for (int i = 0; i < 1; i++)
            {
                rightHandInstantiationTransform.rotation = rightHandTransform.rotation;

                Instantiate(handSpellCaster, rightHandInstantiationTransform.position, Quaternion.Euler(0, rightHandInstantiationTransform.eulerAngles.y, 0));
            }
        }
        else if (spellAction.GetStateDown(rightHand.handType) && wandInHand)
        {
            for (int i = 0; i < 1; i++)
            {
                rightHandInstantiationTransform.rotation = rightHandTransform.rotation;

                Instantiate(wandSpellCaster, rightHandInstantiationTransform.position, Quaternion.Euler(0, rightHandInstantiationTransform.eulerAngles.y, 0));
            }
        }

        if (spellAction.GetStateDown(leftHand.handType) && wandInHand == false)
        {
            for (int i = 0; i < 1; i++)
            {
                leftHandInstantiationTransform.rotation = leftHandTransform.rotation;

                Instantiate(handSpellCaster, leftHandInstantiationTransform.position, Quaternion.Euler(0, leftHandInstantiationTransform.eulerAngles.y, 0));
            }
        }
        else if (spellAction.GetStateDown(leftHand.handType) && wandInHand)
        {
            for (int i = 0; i < 1; i++)
            {
                leftHandInstantiationTransform.rotation = leftHandTransform.rotation;

                Instantiate(wandSpellCaster, leftHandInstantiationTransform.position, Quaternion.Euler(0, leftHandInstantiationTransform.eulerAngles.y, 0));
            }
        }
    }
}
