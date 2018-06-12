// Author: Arnold Holler
// Date: 2018.05.29

using UnityEngine;

public class PropellerSpin : MonoBehaviour
{
    [SerializeField]
    public float speed = 2000f;
	
	void Update () {
        gameObject.transform.Rotate(Vector3.forward, speed * Time.deltaTime);	
	}
}
