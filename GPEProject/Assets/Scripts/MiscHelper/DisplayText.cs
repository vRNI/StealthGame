using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class DisplayText : MonoBehaviour {

    UnityAction mDisableListener = null, mTextDisplayListener = null;

    [TextArea]
    public string mInputText;
    public float mDisplaySpeed = 0;
    public Animator mAnim = null;
    public GameObject mGameObject = null;
    public GameObject mInputBoard = null;

    Text mText = null;

    public void SetText(int rIdx)
    {
        var lInput = mInputBoard.GetComponent<TextboardInputManager>();
        mInputText = lInput.GetTextItem(rIdx);
    }

    private void Awake()
    {
        mText = GetComponent<Text>();
    }

    private void OnEnable()
    {
        mDisableListener = new UnityAction(SetDisabled);
        EventManager.StartListening("Event_Disable_TextBoard", mDisableListener);
        mTextDisplayListener = new UnityAction(SuccessivelyDisplayText);
        EventManager.StartListening("Event_Display_Text", mTextDisplayListener);
    }

    void OnDisable()
    {
        mText.text = string.Empty;

        EventManager.StopListening("Event_Disable_TextBoard", mDisableListener);
        EventManager.StopListening("Event_Display_Text", mTextDisplayListener);
    }
    
    // https://answers.unity.com/questions/362629/how-can-i-check-if-an-animation-is-being-played-or.html
    bool AnimatorIsPlaying(float rOffset)
    {
        return mAnim.GetCurrentAnimatorStateInfo(0).length - rOffset >
               mAnim.GetCurrentAnimatorStateInfo(0).normalizedTime;
    }

    IEnumerator displayText()
    {
        // wait until the animation has finished playing
        while (AnimatorIsPlaying(1))
        {
            yield return null;
        }

        // creating the illusion of successively displaying text by yielding after concating each character
        foreach (var lChar in mInputText)
        {
            mText.text += lChar;
            yield return new WaitForSeconds(mDisplaySpeed);
        }
    }

    void SetDisabled()
    {
        mGameObject.SetActive(false);
    }

    void SuccessivelyDisplayText()
    {
        StopAllCoroutines();
        StartCoroutine(displayText());
    }
}
