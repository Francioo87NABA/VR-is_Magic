using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wand : MonoBehaviour
{
    public bool isLerping;

    public Transform wandReleasOrientation;
    public float timeStartedLerping;
    public float lerpTime;

    // Start is called before the first frame update
    void Start()
    {
        StartLerping();
    }

    private void StartLerping()
    {
        timeStartedLerping = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (isLerping)
        {
            transform.position = Lerp(transform.position, wandReleasOrientation.position, timeStartedLerping, lerpTime);
        }
    }

    public Vector3 Lerp(Vector3 start, Vector3 end, float timeStartedLerping, float lerpTime = 1)
    {
        float timeSinceStarted = Time.time - timeStartedLerping;

        float persentageComplete = timeSinceStarted / lerpTime;

        var result = Vector3.Lerp(start, end, persentageComplete);

        return result;
    }
}
