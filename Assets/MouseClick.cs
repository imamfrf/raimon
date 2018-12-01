using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseClick : MonoBehaviour {
    public AudioClip klik;
    public float vol;
    AudioSource audio;
    // Use this for initialization
    void Start () {
        audio = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            audio.PlayOneShot(klik,vol);
            Debug.Log("Pressed left click.");
        }
            
    }
}
