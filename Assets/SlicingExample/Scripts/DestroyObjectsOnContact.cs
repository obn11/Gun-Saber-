using UnityEngine;

public class DestroyObjectsOnContact : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.attachedRigidbody != null)
        {
            Destroy(other.attachedRigidbody.gameObject);
        }
        else
        {
            Destroy(other.gameObject);
        }
    }
}
