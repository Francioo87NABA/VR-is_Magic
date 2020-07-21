using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
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
    public GameObject cashedSpellCaster;
    public bool wandInHands;
    public bool wandInRightHand;
    public bool wandInLeftHand;
    public int oneTime;

    public Transform teletrasportatiQui;
    public Player player;
    public GameObject PS_teletrasporto;
    public GameObject CashedPS_teletrasporto;

    public GameObject realPlayer;

    public bool gameOver;

    [Header("SteamVR References")]
    public Transform head;
    public Hand leftHand;
    public Hand rightHand;

    [Space(20)]
    public SteamVR_Action_Boolean grabAction = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("GrabGrip");
    public SteamVR_Action_Boolean pullAction = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("GrabPinch");
    public SteamVR_Action_Boolean spellAction = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("RightClick");
    public SteamVR_Action_Boolean teleportAction = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("Teletrasporto");

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


        if (pullAction.GetStateDown(rightHand.handType) && wandInHands)
        {
            pullEvent.Invoke();
        }
        if (pullAction.GetStateDown(leftHand.handType) && wandInHands)
        {
            pullEvent.Invoke();
        }


        if (spellAction.GetStateDown(rightHand.handType) && oneTime == 0)
        {
            if ( handSpellCaster != null)
            {
                if (wandInHands == false)
                {
                    rightHandInstantiationTransform.rotation = rightHandTransform.rotation;

                    Instantiate(handSpellCaster, rightHandInstantiationTransform.position, Quaternion.Euler(0, rightHandInstantiationTransform.eulerAngles.y, 0));

                    oneTime = 1;
                }
                else
                {
                    if (wandInRightHand)
                    {
                        rightHandInstantiationTransform.rotation = rightHandTransform.rotation;

                        Instantiate(wandSpellCaster, rightHandInstantiationTransform.position, Quaternion.Euler(0, rightHandInstantiationTransform.eulerAngles.y, 0));

                        oneTime = 1;
                    }
                    else
                    {
                        rightHandInstantiationTransform.rotation = rightHandTransform.rotation;

                        Instantiate(handSpellCaster, rightHandInstantiationTransform.position, Quaternion.Euler(0, rightHandInstantiationTransform.eulerAngles.y, 0));

                        oneTime = 1;
                    }
                }
            }
            else
            {
                Debug.LogError("HandSpellCasterIsNUll");
            }
        }      

        if (spellAction.GetStateDown(leftHand.handType) && oneTime == 0)
        {
            if (wandInHands == false)
            {
                leftHandInstantiationTransform.rotation = leftHandTransform.rotation;

                Instantiate(handSpellCaster, leftHandInstantiationTransform.position, Quaternion.Euler(0, leftHandInstantiationTransform.eulerAngles.y, 0));

                oneTime = 1;
            }
            else
            {
                if (wandInLeftHand) 
                {
                    leftHandInstantiationTransform.rotation = leftHandTransform.rotation;

                    Instantiate(wandSpellCaster, leftHandInstantiationTransform.position, Quaternion.Euler(0, leftHandInstantiationTransform.eulerAngles.y, 0));

                    oneTime = 1;
                }
                else
                {
                    leftHandInstantiationTransform.rotation = leftHandTransform.rotation;

                    Instantiate(handSpellCaster, leftHandInstantiationTransform.position, Quaternion.Euler(0, leftHandInstantiationTransform.eulerAngles.y, 0));

                    oneTime = 1;
                }
            }
        }

        if (teleportAction.GetStateDown(leftHand.handType) || teleportAction.GetStateDown(rightHand.handType) && teletrasportatiQui != null)
        {
            player.transform.position = teletrasportatiQui.position;
            player.transform.rotation = teletrasportatiQui.rotation;
            CashedPS_teletrasporto = Instantiate(PS_teletrasporto, teletrasportatiQui.position, teletrasportatiQui.rotation);
           
            Destroy(CashedPS_teletrasporto, 6.3f);
        }

        if (gameOver == true)
        {
            StartCoroutine(HaiPerso());
        }
    }

    IEnumerator HaiPerso()
    {
        //Aggiungi qualcosa che faccia capire di aver perso
        yield return new WaitForSecondsRealtime(5f);
        Destroy(realPlayer);
        SceneManager.LoadScene(0);
    }
}
