using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

public class LoadSnapshot : MonoBehaviour {
	public AudioMixerSnapshot inCombat;
	public AudioSource bgmSrc;


	// Use this for initialization
	void Start () {
		inCombat.TransitionTo (0);
		bgmSrc.Play ();
	}
}
