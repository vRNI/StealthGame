using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ManiputlateOutline : MonoBehaviour {

    UnityAction mEnableListener = null, mDisableListener = null;
    UnityAction mColorChangeAlert = null, mColorChangeDormant = null, mColorChangeCaution = null;

    Outline mOutline = null;

    private void Awake()
    {
        mOutline = GetComponent<Outline>();
    }

    private void OnEnable()
    {
        mEnableListener = new UnityAction(EnableOutline);
        mDisableListener = new UnityAction(DisableOutline);
        mColorChangeAlert = new UnityAction(ChangeColorToAlert);
        mColorChangeDormant = new UnityAction(ChangeColorToDormant);
        mColorChangeCaution = new UnityAction(ChangeColorToCaution);

        EventManager.StartListening("Event_Enable_Drone_Outline", mEnableListener);
        EventManager.StartListening("Event_Disable_Drone_Outline", mDisableListener);
        EventManager.StartListening("Event_Change_Drone_Outline_Color_Alert", mColorChangeAlert);
        EventManager.StartListening("Event_Change_Drone_Outline_Color_Dormant", mColorChangeDormant);
        EventManager.StartListening("Event_Change_Drone_Outline_Color_Caution", mColorChangeCaution);
    }

    private void OnDisable()
    {
        EventManager.StopListening("Event_Enable_Drone_Outline", mEnableListener);
        EventManager.StopListening("Event_Disable_Drone_Outline", mDisableListener);
        EventManager.StopListening("Event_Change_Drone_Outline_Color_Alert", mColorChangeAlert);
        EventManager.StopListening("Event_Change_Drone_Outline_Color_Dormant", mColorChangeDormant);
        EventManager.StopListening("Event_Change_Drone_Outline_Color_Caution", mColorChangeCaution);
    }

    void EnableOutline()
    {
        mOutline.enabled = true;
    }

    void DisableOutline()
    {
        mOutline.enabled = false;
    }

    void ChangeColorToAlert()
    {
        mOutline.OutlineColor = new Color(255, 0, 0, 255);
    }

    void ChangeColorToDormant()
    {
        mOutline.OutlineColor = new Color(255, 255, 0, 255);
    }

    void ChangeColorToCaution()
    {
        mOutline.OutlineColor = new Color(255, 0, 0, 255);
    }
}
