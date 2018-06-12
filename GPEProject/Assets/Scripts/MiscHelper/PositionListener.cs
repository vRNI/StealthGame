using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using com.ootii.Actors;
using com.ootii.Actors.AnimationControllers;

public class PositionListener : MonoBehaviour {

    UnityAction poslistener = null;
    public Vector3 startposition = Vector3.zero;

    void OnEnable ()
    {
        poslistener = new UnityAction(ResetToStartPos);
        EventManager.StartListening("Event_Reset_Player_Position", poslistener);
    }

    void ResetToStartPos ()
    {
        gameObject.transform.position = startposition;
    }

    void OnDisable()
    {
        EventManager.StopListening("Event_Reset_Player_Position", poslistener);
    }

}
