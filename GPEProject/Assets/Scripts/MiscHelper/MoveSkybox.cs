using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSkybox : MonoBehaviour {

    [Tooltip("Speed for rotating the skybox")]
    public float mRotationSpeed;

    float mRotationAmount = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        mRotationAmount += (Time.deltaTime * mRotationSpeed) % 360;
        RenderSettings.skybox.SetFloat("_Rotation", mRotationAmount);
	}
}
