using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionText : MonoBehaviour {
    private bool Is;
	// Use this for initialization
	void Start () {
        Is = true;
	}
	public void boolcheck()
    {
        Is = true;
    }
	// Update is called once per frame
	void Update () {
        if (Is)  {
            AudioManager.instance.PlayAudio("Success");
            Is = false;
        }
    }
}
