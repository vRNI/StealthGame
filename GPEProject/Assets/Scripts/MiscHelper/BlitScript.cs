using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class BlitScript : MonoBehaviour {

    [Tooltip("Material that stores the transition parameters.")]
    public Material mMat = null;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (mMat == null)
        {
            Debug.LogError("Material must be assigned to support transitions.");
            return;
        }
        Graphics.Blit(source, destination, mMat);
    }
}
