using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PanningListener : MonoBehaviour {

    UnityAction mPanListener = null, mRevertPanListener = null;
    Animator mAnim = null;

    private void OnEnable()
    {
        mPanListener = new UnityAction(Pan);
        mRevertPanListener = new UnityAction(RevertPan);

        EventManager.StartListening("Event_Pan_Camera", mPanListener);
        EventManager.StartListening("Event_RevertPan_Camera", mRevertPanListener);
    }

    private void OnDisable()
    {
        EventManager.StopListening("Event_Pan_Camera", mPanListener);
        EventManager.StopListening("Event_RevertPan_Camera", mRevertPanListener);
    }

    private void Awake()
    {
        mAnim = GetComponent<Animator>();
    }

    void Pan()
    {
        mAnim.SetTrigger("aPan");
    }

    void RevertPan()
    {
        mAnim.SetTrigger("aRevertPan");
    }
}
