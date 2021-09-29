using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingsSpinner : MonoBehaviour
{
    float speed;
    float secPerBeat;
    public bool outer;

    // Start is called before the first frame update
    void Start()
    {
        secPerBeat = GameObject.Find("SoundManager").GetComponent<SoundManager>().secPerBeat;

        speed = 90f/(secPerBeat*8);
    }

    // Update is called once per frame
    void Update()
    {
        if (!PauseMenu.paused)
        {
            if (!outer)
            {
                transform.Rotate(new Vector3(0, 0, 1), speed * Time.deltaTime);
            }
            else
            {
                transform.Rotate(new Vector3(0, 0, 1), (speed * Time.deltaTime * -0.25f));
            }
        }
    }
}
