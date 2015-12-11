using UnityEngine;
using System.Collections;

public class LerpPosition : MonoBehaviour {

    Vector3 endPosition;
    Vector3 startPosition;

    public float speed = 1.0f;

    float startTime, dist = 1;

	void Awake() {
        startPosition = transform.position;
        startTime = Time.time;
        endPosition = new Vector3(5, 0, 0);

	}
		
	void Update () {
        Vector3 posA = new Vector3(5, 0, 0);
        Vector3 posB = new Vector3(-5, 0, 0);

        if (Input.GetKeyDown(KeyCode.A))
        {
            startPosition = transform.position;
            endPosition = posA;
            dist = Vector3.Distance(startPosition, endPosition);
            startTime = Time.time;
        }
        
        if (Input.GetKeyDown(KeyCode.S))
        {
            startPosition = transform.position;
            endPosition = posB;
            dist = Vector3.Distance(startPosition, endPosition);
            startTime = Time.time;
        }

        //float delta = (Time.time - startTime) / dist;
        //Debug.Log(delta);
        //transform.position = Vector3.Lerp(startPosition, endPosition, delta);

        //Debug.Log(delta);
        if (transform.position != endPosition)
        {
            float delta = (Time.time - startTime) / dist * speed;
            transform.position = Vector3.Lerp(startPosition, endPosition, delta);
        }
        
	}
}
