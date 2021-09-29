using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beat : MonoBehaviour
{
    [SerializeField]
    float speed;
    public ComboDisplay combo;
    public static int deathCounter;
    new AudioSource audio;
    AudioClip error;

    private void Start()
    {
        combo = GameObject.Find("Combo").GetComponent<ComboDisplay>();
        error = GameObject.Find("Gun").GetComponent<GunController>().sounds[4];
        audio = GameObject.Find("Gun").GetComponent<AudioSource>();
        speed = 8 / (4 * GameObject.Find("SoundManager").GetComponent<SoundManager>().secPerBeat);
    }
    // Moves the beat towards the player and checks if it has reached the elemnation zone
    void FixedUpdate()
    {

        if (transform.position.z < -1 && GetComponent<MeshRenderer>().enabled == true)
        {
            GameObject.Find("Miss").GetComponent<AudioSource>().PlayOneShot(error);
            combo.SetCombo(0);
            GetComponent<MeshRenderer>().enabled = false;
            Destroy(transform.parent.gameObject, 0.7f);
            deathCounter++;
            transform.gameObject.SetActive(false);
        }
        else
        {
            transform.position += Time.deltaTime * transform.forward * speed;
        }
        if (audio.time > 0.1f)
        {
            audio.Stop();
        }
        if (deathCounter == 6)
        {
            //End()
        }

    }
}
