using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SoundManager : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip musicTrack;
    [SerializeField]
    public float firstBeatOffset;
    public float firstBeat;
    public float songBpm;
    public float songPositionInBeats;
    public float songLengthInSecs = 240;
    public float songLengthInBeats;

    public float secPerBeat;
    float songPosition;
    float dspSongTime;



    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        secPerBeat = 60f / songBpm;
        dspSongTime = (float)AudioSettings.dspTime;
        songLengthInBeats = songLengthInSecs / secPerBeat;
    }

    void Update()
    {
        if (!PauseMenu.paused) {
            songPosition = (float)(AudioSettings.dspTime - dspSongTime - firstBeatOffset);
            songPositionInBeats = songPosition / secPerBeat;
            if (songPositionInBeats == firstBeat)
            {
                PlayMusicTrack();
            }
        }
    }

    //Plays selected track
    void PlayMusicTrack()
    {
        audioSource.PlayOneShot(musicTrack);
    }
}
