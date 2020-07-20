using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tornado : MonoBehaviour
{
    public iTween.EaseType movementEasyType = iTween.EaseType.easeInOutSine;

    public float movementRadius = 30f;
    public float movementSpeed = 0.5f;

    private Vector3 originalPosition;

    // Start is called before the first frame update
    void Start()
    {
        originalPosition = transform.position;
        StartCoroutine(Movement());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Movement()
    {
        var newPos = new Vector3(originalPosition.x + Random.Range(-movementRadius, movementRadius), originalPosition.y, originalPosition.z + Random.Range(-movementRadius, movementRadius));
        var distance = newPos - originalPosition;
        var time = distance.magnitude / movementSpeed;

        iTween.MoveTo(gameObject, iTween.Hash("position", newPos, "easyType", movementEasyType, "time", time));
        yield return new WaitForSeconds(time + 0.1f);
        StartCoroutine(Movement());
    }
}
