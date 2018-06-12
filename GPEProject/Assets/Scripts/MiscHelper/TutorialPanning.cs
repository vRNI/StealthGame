using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TutorialPanning : MonoBehaviour {

    UnityAction mAnimationListener = null, mDisableListener = null;
    Animator mAnim = null;

    private void Awake()
    {
        mAnim = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        mAnimationListener = new UnityAction(TriggerAnimation);
        mDisableListener = new UnityAction(DisableCamera);

        EventManager.StartListening("Event_Play_Tutorial_Panning", mAnimationListener);
        EventManager.StartListening("Event_Disable_Tutorial_Camera", mAnimationListener);
    }

    private void OnDisable()
    {
        EventManager.StopListening("Event_Play_Tutorial_Panning", mAnimationListener);
        EventManager.StopListening("Event_Disable_Tutorial_Camera", mAnimationListener);
    }

    void TriggerAnimation()
    {
        mAnim.SetTrigger("aPan");
    }

    void DisableCamera()
    {
        gameObject.SetActive(false);
    }
}
