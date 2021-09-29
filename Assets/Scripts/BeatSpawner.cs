using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BeatSpawner : MonoBehaviour
{
    public GameObject[] cubes;
    public Transform[] spawnPoints;
    public SoundManager soundManager;
    public AudioClip drumTrack;
    float nextBeatToSpawnNote = 0.0f;
    
    float difficulty;

    private void Start()
    {
        difficulty = MainMenu.difficulty;
    }

    // Update is called once per frame
    void Update()
    {
        if(soundManager.songPositionInBeats >= nextBeatToSpawnNote && soundManager.songLengthInBeats >= soundManager.songPositionInBeats && !PauseMenu.paused)
        {
            if ((int)(soundManager.songPositionInBeats) % 2 == 1)
            {
                SpawnOnBeat();
                //soundManager.GetComponent<AudioSource>().PlayOneShot(drumTrack);
                GameObject.Find("Drum").GetComponent<AudioSource>().PlayOneShot(drumTrack);
            } else
            {
                SpawnOffBeat();
            }
            nextBeatToSpawnNote += 1.0f;
            
        }
    }

    void SpawnOnBeat()
    {
        Instantiate(cubes[Random.Range(0, cubes.Length)], spawnPoints[Random.Range(0, spawnPoints.Length)]);
    }

    void SpawnOffBeat()
    {
        if (Random.Range(0.0f, 1.0f) < difficulty) {
            Instantiate(cubes[Random.Range(0, cubes.Length)], spawnPoints[Random.Range(0, spawnPoints.Length)]);
        }
    }
}
