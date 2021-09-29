using UnityEngine;

[RequireComponent(typeof(Sliceable))]
public class BurstApartSlices : MonoBehaviour
{
    [SerializeField] private float burstForce = 100f;
    private Sliceable sliceable;
    
    void Awake()
    {
        sliceable = gameObject.GetComponent<Sliceable>();
    }

    protected void OnEnable()
    {
        sliceable.Sliced += Sliceable_Sliced;
    }

    protected void OnDisable()
    {
        sliceable.Sliced -= Sliceable_Sliced;
    }

    void Sliceable_Sliced(object sender, NobleMuffins.TurboSlicer.SliceEventArgs e)
    {
        if (e.Parts.Length == 0)
        {
            Debug.LogWarning("Slice resulted in 0 parts");
            return;
        }
        
        Vector3 sumOfPartPositions = Vector3.zero;
        foreach (GameObject part in e.Parts)
        {
            Rigidbody rb = part.GetComponent<Rigidbody>();
            sumOfPartPositions += rb.centerOfMass;
        }
        
        Vector3 midPointBetweenParts = sumOfPartPositions / e.Parts.Length;

        foreach (GameObject part in e.Parts)
        {
            Rigidbody rb = part.GetComponent<Rigidbody>();
            var forceDirection = rb.centerOfMass - midPointBetweenParts;
            forceDirection.Normalize();
            forceDirection *= burstForce;
            rb.AddForce(forceDirection);
        }
    }
}
