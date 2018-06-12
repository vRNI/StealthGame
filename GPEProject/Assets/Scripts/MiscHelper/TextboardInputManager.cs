using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

public class TextboardInputManager : MonoBehaviour {

    [Tooltip("Used to display text when choosing a menu category apart from start.")]
    [TextArea(10, 15)]
    [SerializeField]
    List<string> mTextItems = null;

    public string GetTextItem(int rIdx)
    {
        if (rIdx > mTextItems.Count || rIdx < 0)
        {
            Debug.LogError("Text doesn't exist.");
            return "";
        }
        return mTextItems[rIdx].ToString();
    }
}
