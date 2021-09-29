using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBeat : MonoBehaviour
{

    //Pops Beat
    public void Pop()
    {
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<BoxCollider>().enabled = false;
        GetComponent<AudioSource>().Play();
        GetComponent<ParticleSystem>().Play();
        Destroy(transform.parent.gameObject, 0.7f);
    }
}
