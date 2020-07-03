using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Singleton;

    [Header("Grab Management")]
    public Grabbable leftHandSelectedObject;
    public Grabbable rightHandSelectedObject;
    [Space(20)]
    public List<Grabbable> leftHandGrabbedObjects;
    public List<Grabbable> rightHandGrabbedObjects;

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
        
    }
}
