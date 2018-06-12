using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnableDisableManager : MonoBehaviour {

    UnityAction mEnableButtonWrapper = null;
    UnityAction mEnableCharacterCamera = null;

    public GameObject mButtonWrapper = null;
    public GameObject mCamera = null;
    public GameObject mPlayerCamera = null;
    public GameObject mPlayer = null;

    private void OnEnable()
    {
        mEnableButtonWrapper = new UnityAction(EnableButtonWrapper);
        EventManager.StartListening("Event_Enable_ButtonWrapper", mEnableButtonWrapper);

        mEnableCharacterCamera = new UnityAction(EnableCharacterCamera);
        EventManager.StartListening("Event_Enable_Character_Camera", mEnableCharacterCamera);
    }

    private void OnDisable()
    {
        EventManager.StopListening("Event_Enable_ButtonWrapper", mEnableButtonWrapper);
        EventManager.StopListening("Event_Enable_Character_Camera", mEnableCharacterCamera);
    }

    void EnableButtonWrapper()
    {
        mButtonWrapper.SetActive(true);
    }

    void EnableCharacterCamera()
    {
        mCamera.SetActive(false);
        mPlayerCamera.SetActive(true);
        mPlayer.SetActive(true);

        EventManager.TriggerEvent("Event_Manual_Fade_In");
    }
}
