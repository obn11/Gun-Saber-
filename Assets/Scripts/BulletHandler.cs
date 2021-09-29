using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHandler : MonoBehaviour
{
    void Start()
    {
        Destroy(gameObject, 3f);
    }

    //If this bullet hit a collider, check if the collided object is of layer 9 (intertactable)
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Destructable")
        {
            DestroyBeat(collision);
            
            Destroy(gameObject);
        }
    }

    //Removes target
    void DestroyBeat(Collision collision)
    {
        DestroyBeat b = collision.gameObject.GetComponent<DestroyBeat>();
        if (b != null)
        {
            b.Pop();
        }
    }
}
