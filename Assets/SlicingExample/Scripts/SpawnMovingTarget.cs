using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMovingTarget : MonoBehaviour
{
    public GameObject targetPrefab;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnNewTarget", 1, 3);
    }

    private void SpawnNewTarget()
    {
        GameObject target = Instantiate(targetPrefab, transform.position, Quaternion.identity);
        target.GetComponent<Rigidbody>().AddForce(0, 0, -1, ForceMode.Impulse);
    }
}
