using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using com.ootii.Cameras;
using com.ootii.Actors.AnimationControllers;
using UnityEngine.PostProcessing;

public class StanceSmoothing : MonoBehaviour {

    public float mSneakZoom = 2;
    public float mNonSneakZoom = 2.5f;

    public float mSmoothingFactor = 3;
    public float mSmootingSeconds = 0.02f;

    OrbitRig mRig = null;
    MotionController mController = null;

    PostProcessingBehaviour mPostProcessingBehaviour = null;
    public PostProcessingProfile mXRay = null, mNonXRay = null;

    bool mIsXRayVision = false;

	// Use this for initialization
	void Start () {
        mRig = GetComponent<OrbitRig>();
        mController = GameObject.FindGameObjectWithTag("Player").GetComponent<MotionController>();
        mPostProcessingBehaviour = GetComponent<PostProcessingBehaviour>();
	}
	
	// Update is called once per frame
	void Update () {
        // check if we're currently in sneaking mode -> interpolate values
        // zoom in and zoom out when changing stances
        // improvement: maybe use of a coroutine when activating, deactivating the animation state
        // Debug.Log(mController.GetAnimatorMotionPhase(0));
        // 610 is idle | 620 is sneaking
        if (mController.GetAnimatorMotionPhase(0) == 620 && mRig._Radius != mSneakZoom)
        {
            mIsXRayVision = true;

            StopAllCoroutines();
            StartCoroutine(SmoothZoom(mSneakZoom));
            EventManager.TriggerEvent("Event_Enable_Drone_Outline");
        }
        else if (mController.GetAnimatorMotionPhase(0) == 610 && mRig._Radius != mNonSneakZoom)
        {
            mIsXRayVision = false;

            StopAllCoroutines();
            StartCoroutine(SmoothZoom(mNonSneakZoom));
            EventManager.TriggerEvent("Event_Disable_Drone_Outline");
        }
    }

    const float TOLERANCE = 0.01f;

    IEnumerator SmoothZoom(float rZoomTo)
    {
        if (mIsXRayVision)
        {
            mPostProcessingBehaviour.profile = mXRay;
        }
        else
        {
            mPostProcessingBehaviour.profile = mNonXRay;
        }

        while (Mathf.Abs(mRig._Radius - rZoomTo) > TOLERANCE)
        {
            mRig._Radius = Mathf.Lerp(mRig._Radius, rZoomTo, Time.deltaTime * mSmoothingFactor);
            yield return new WaitForSeconds(mSmootingSeconds);
        }
    }
}
