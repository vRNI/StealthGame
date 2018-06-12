using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class TransitionScript : MonoBehaviour {

    //[Tooltip("How long to take until screen is fully faded in.")]
    //public float mFadeInTime = 20;

    bool mIsFading = false;
    public int mSceneTransitionIdx;

    UnityAction mFadeInListener = null, mFadeOutListener = null, mTransitionSceneListener = null;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;

        mFadeInListener = new UnityAction(ManualFadeIn);
        mFadeOutListener = new UnityAction(ManualFadeOut);
        mTransitionSceneListener = new UnityAction(TransitionScene);

        EventManager.StartListening("Event_Manual_Fade_In", mFadeInListener);
        EventManager.StartListening("Event_Manual_Fade_Out", mFadeOutListener);
        EventManager.StartListening("Event_Transition_Scene", mTransitionSceneListener);
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;

        EventManager.StopListening("Event_Manual_Fade_In", mFadeInListener);
        EventManager.StopListening("Event_Manual_Fade_Out", mFadeOutListener);
        EventManager.StopListening("Event_Transition_Scene", mTransitionSceneListener);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        var lBlitScript = GetComponent<BlitScript>();
        var lMaterial = lBlitScript.mMat;

        lMaterial.SetFloat("_FadeValue", 1);
        lMaterial.SetFloat("_TransitionAmount", 1);

        float lValueToSubtract = 1f / 3;
        mIsFading = true;

        StopAllCoroutines();
        StartCoroutine(FadeInScene(lValueToSubtract, lMaterial));
    }

    public void TransitionScene()
    {
        // fade is currently going on
        if (mIsFading) return;

        var lBlitScript = GetComponent<BlitScript>();
        var lMaterial = lBlitScript.mMat;

        lMaterial.SetFloat("_FadeValue", 0);
        lMaterial.SetFloat("_TransitionAmount", 1);

        float lValueToAdd = 1f / 2;
        mIsFading = true;

        StopAllCoroutines();
        StartCoroutine(FadeOutScene(lValueToAdd, lMaterial, true));
    }

    IEnumerator FadeInScene(float rValueToSubtract, Material rMaterial)
    {
        var lFadeValue = 1f;

        while (lFadeValue >= 0)
        {
            lFadeValue -= rValueToSubtract * Time.deltaTime;
            if (lFadeValue <= 0)
                rMaterial.SetFloat("_FadeValue", 0);
            else
                rMaterial.SetFloat("_FadeValue", lFadeValue);
            yield return null;
        }

        mIsFading = false;
        EventManager.TriggerEvent("Event_Play_Tutorial_Panning");
    }

    IEnumerator FadeOutScene(float rValueToAdd, Material rMaterial, bool rSwitchScene)
    {
    
        var lFadeValue = 0f;

        while (lFadeValue <= 1)
        {
            lFadeValue += rValueToAdd * Time.deltaTime;
            if (lFadeValue >= 1)
                rMaterial.SetFloat("_FadeValue", 1);
            else
                rMaterial.SetFloat("_FadeValue", lFadeValue);
            yield return null;
        }

        mIsFading = false;
        if (rSwitchScene)
            SceneManager.LoadScene(mSceneTransitionIdx, LoadSceneMode.Single);

        EventManager.TriggerEvent("Event_Enemies_Stop_Pursuit");

        EventManager.TriggerEvent("Event_Enable_Character_Camera");
        EventManager.TriggerEvent("Event_Reset_Player_Position");



    }

    void ManualFadeIn()
    {
        var lBlitScript = GetComponent<BlitScript>();
        var lMaterial = lBlitScript.mMat;

        lMaterial.SetFloat("_FadeValue", 1);
        lMaterial.SetFloat("_TransitionAmount", 1);

        float lValueToSubtract = 1f / 2;
        mIsFading = true;

        StopAllCoroutines();
        StartCoroutine(FadeInScene(lValueToSubtract, lMaterial));
    }

    void ManualFadeOut()
    {
        // fade is currently going on
        if (mIsFading) return;

        var lBlitScript = GetComponent<BlitScript>();
        var lMaterial = lBlitScript.mMat;

        lMaterial.SetFloat("_FadeValue", 0);
        lMaterial.SetFloat("_TransitionAmount", 1);

        float lValueToAdd = 1f / 2;
        mIsFading = true;

        StopAllCoroutines();
        StartCoroutine(FadeOutScene(lValueToAdd, lMaterial, false));
    }
}