using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager Singleton;

    [Header("SteamVR References")]
    public Transform head;
    public Hand leftHand;
    public Hand rightHand;
    

    [Space(20)]
    public SteamVR_Action_Boolean grabAction = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("GrabGrip");
    public SteamVR_Action_Boolean pullAction = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("GrabPinch");

    [Header("Input Events")]
    public UnityEvent grabEvent;
    public UnityEvent grabReleasEvent;
    public UnityEvent pullEvent;

    //[Header("Input Bools")]
    //public bool SoulGrabBool;


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
            Debug.Log("eureca");
            grabEvent.Invoke();
            
        }
        
        if (grabAction.GetStateUp(rightHand.handType))
        {   
            grabReleasEvent.Invoke();
        }

        if (pullAction.GetStateDown(rightHand.handType))
        {
            pullEvent.Invoke();
        }
    }
}
