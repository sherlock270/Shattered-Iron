using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayClip : MonoBehaviour {
    private AudioSource source;
    public AudioClip clip;

	// Use this for initialization
	void Awake () {
  	source = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	private void Start(){
		source.PlayOneShot(clip);
	}
}
