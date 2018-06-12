using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedAnimations : MonoBehaviour {

    [Tooltip("The amount of animations that are being played while idling.")]
    public int mAnimationAmount = 5;
    [Tooltip("The amount of time to pass to play another animation.")]
    public float mTimeToPass;

    Animator mAnim = null;
    float mTimePassed = 0;

	// Use this for initialization
	void Start () {
        mAnim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		if (mAnim.GetCurrentAnimatorStateInfo(0).IsName("oh_idle"))
        {
            if (mTimePassed >= mTimeToPass)
            {
                mTimePassed = 0;
                PlayRandomAnimation();
            }
            else
            {
                mTimePassed += Time.deltaTime;
            }
        }
	}

    void PlayRandomAnimation()
    {
        var lAnimationIdx = Random.Range(1, mAnimationAmount);
        mAnim.SetTrigger("aAnim" + lAnimationIdx.ToString());
    }
}
