using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SensorScript : MonoBehaviour
{
    RaycastHit hitData;
    public float fullAngle = 360f;
    public float distanceTwoObject = 5f;
    public int numberOfGizmos = 6;
    public float distanceTwoTarget = 5f;

    public RawImage sensorUP;
    public RawImage sensorDOWN;
    public RawImage sensorRIGHT;
    public RawImage sensorLEFT;

    void Start()
    {
        sensorUP.enabled = false;
        sensorDOWN.enabled = false;
        sensorLEFT.enabled = false;
        sensorRIGHT.enabled = false;


    }
    void Update()
    {
        LayerMask mask = LayerMask.GetMask("planes");

        float[] distances = new float[numberOfGizmos];
        for (int i = 0; i < numberOfGizmos; ++i)
        {
            var rotation = this.transform.rotation;
            var rotationMod = Quaternion.AngleAxis((i / (float)numberOfGizmos) * fullAngle, this.transform.up);
            var direction = rotation * rotationMod * Vector3.forward;

            var ray = new Ray(this.transform.position, direction);

            if (Physics.Raycast(ray, out hitData, distanceTwoObject))
            {
                //Debug.Log(hitData.distance + " = Uderzyl!");
                distances[i] = hitData.distance;
                //Debug.Log(distances[i]);
            }

        }
        //ODLEGLOSC KRYTYCZNA
        if (distances[0] < distanceTwoTarget && distances[0] > 1)
        {
            sensorUP.enabled = true;
            Debug.Log("Czujnik UP uruchomiony! Dystans do obiektu: " + distances[0]);
        }
        else if (distances[1] < distanceTwoTarget && distances[1] > 1)
        {
            sensorLEFT.enabled = true;
            Debug.Log("Czujnik RIGHT uruchomiony! Dystans do obiektu: " + distances[1]);
        }
        else if ((distances[2] < distanceTwoTarget && distances[2] > 1) || (distances[3] < distanceTwoTarget && distances[3] > 1))
        {
            sensorDOWN.enabled = true;
            Debug.Log("Czujnik DOWN uruchomiony! Dystans do obiektu: " + distances[2]);

        }
        else if (distances[4] < distanceTwoTarget && distances[4] > 1)
        {
            sensorRIGHT.enabled = true;
            Debug.Log("Czujnik LEFT uruchomiony! Dystans do obiektu: " + distances[4]);
        }
        else if ((distances[0] < distanceTwoTarget && distances[0] > 1) && (distances[1] < distanceTwoTarget && distances[1] > 1))
        {
            sensorUP.enabled = true;
            sensorLEFT.enabled = true;
            Debug.Log("Czujnik UP and RIGHT  uruchomiony! Dystans do obiektu: " + distances[0]);
        }
        else if ((distances[0] < distanceTwoTarget && distances[0] > 1) && (distances[4] < distanceTwoTarget && distances[4] > 1))
        {
            sensorUP.enabled = true;
            sensorRIGHT.enabled = true;
            Debug.Log("Czujnik UP and LEFT  uruchomiony! Dystans do obiektu: " + distances[0]);
        }
        else if ((distances[1] < distanceTwoTarget && distances[1] > 1) && (distances[2] < distanceTwoTarget && distances[2] > 1))
        {
            sensorLEFT.enabled = true;
            sensorDOWN.enabled = true;
            Debug.Log("Czujnik DOWN and RIGHT  uruchomiony! Dystans do obiektu: " + distances[1]);
        }
        else if ((distances[3] < distanceTwoTarget && distances[3] > 1) && (distances[4] < distanceTwoTarget && distances[4] > 1))
        {
            sensorDOWN.enabled = true;
            sensorRIGHT.enabled = true;
            Debug.Log("Czujnik DOWN and LEFT  uruchomiony! Dystans do obiektu: " + distances[3]);
        }
        else
        {
            sensorUP.enabled = false;
            sensorDOWN.enabled = false;
            sensorLEFT.enabled = false;
            sensorRIGHT.enabled = false;
        }
    }
    void OnDrawGizmos()
    {
        for (int i = 0; i < numberOfGizmos; ++i)
        {
            var rotation = this.transform.rotation;
            var rotationMod = Quaternion.AngleAxis((i / (float)numberOfGizmos) * fullAngle, this.transform.up);
            var direction = rotation * rotationMod * Vector3.forward;
            Gizmos.DrawRay(this.transform.position, direction * distanceTwoTarget);
        }
    }
}
