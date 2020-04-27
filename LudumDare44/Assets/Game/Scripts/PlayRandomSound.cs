using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayRandomSound : MonoBehaviour
{
	public AudioClip[] sounds;

	protected AudioSource _audioSource;

	private void Start()
	{
		_audioSource = GetComponent<AudioSource>();
		_audioSource.loop = false;
	}

	public void play(int index = -1)
	{
		if (_audioSource.isPlaying)
			_audioSource.Stop();

		if (index < 0 || index >= sounds.Length)
			index = Random.Range(0, sounds.Length - 1);

		_audioSource.clip = sounds[index];
		_audioSource.Play();
	}
}
