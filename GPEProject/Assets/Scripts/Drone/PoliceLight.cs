using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceLight : MonoBehaviour {
    public Light light1;
    public Light light2;
    public float switchspeed;
    bool switchDirection;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (switchDirection)
        {
            light1.intensity -= switchspeed;
            light2.intensity += switchspeed;
            if (light1.intensity <= 0) switchDirection = false;
        }

        else
        {
            light2.intensity -= switchspeed;
            light1.intensity += switchspeed;
            if (light2.intensity <= 0) switchDirection = true;
        }

    }
}
