using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SensorScript : MonoBehaviour
{
    RaycastHit hitData;
    public float fullAngle = 360f;
    public float distanceOfObject = 5f;
    public int numberOfGizmos = 6;
    public float distanceOfTarget = 15f;

    public RawImage sensorUP;
    public RawImage sensorDOWN;
    public RawImage sensorRIGHT;
    public RawImage sensorLEFT;


    void Start()
    {
        sensorUP = GetComponent<RawImage>();
        sensorDOWN = GetComponent<RawImage>();
        sensorLEFT = GetComponent<RawImage>();
        sensorRIGHT = GetComponent<RawImage>();

        sensorUP.enabled = false;
        sensorDOWN.enabled = false;
        sensorLEFT.enabled = false;
        sensorRIGHT.enabled = false;

    }
    void Update()
    {
        float[] distances = new float[numberOfGizmos];
        for (int i = 0; i < numberOfGizmos; ++i)
        {
            var rotation = this.transform.rotation;
            var rotationMod = Quaternion.AngleAxis((i / (float)numberOfGizmos ) * fullAngle , this.transform.up);
            var direction = rotation * rotationMod * Vector3.forward;

            var ray = new Ray(this.transform.position, direction);

            if(Physics.Raycast(ray, out hitData, distanceOfObject))
            {
                //Debug.Log(hitData.distance + " = Uderzyl!");
                distances[i] = hitData.distance;
            }
 
        }
        if (distances[0] > 1)
        {
            Debug.Log("1");
            sensorUP.color = Color.red;
        }
        if (distances[1] > 1)
        {
            Debug.Log("2");
        }
        if (distances[2] > 1)
        {
            Debug.Log("3");
        }
        if (distances[3] > 1)
        {
            Debug.Log("4");
        }
        if (distances[4] > 1)
        {
            Debug.Log("5");
        }

    }

    void OnDrawGizmos()
    {
        for (int i = 0; i < numberOfGizmos; ++i)
        {
            var rotation = this.transform.rotation;
            var rotationMod = Quaternion.AngleAxis((i / (float)numberOfGizmos) * fullAngle , this.transform.up);
            var direction = rotation * rotationMod * Vector3.forward;
            Gizmos.DrawRay(this.transform.position, direction);
        }
    }
}
