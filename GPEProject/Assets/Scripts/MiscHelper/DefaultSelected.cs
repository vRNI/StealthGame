using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DefaultSelected : MonoBehaviour {

    public GameObject mPreSelectedButton;
    public Button[] mButtons;

    public void PreSelectButton()
    {
        foreach (var lButtons in mButtons)
        {
            lButtons.image.color = Color.white;
            lButtons.GetComponent<Animator>().SetTrigger("Normal");
        }
        EventSystem.current.SetSelectedGameObject(mPreSelectedButton);
    }

}
