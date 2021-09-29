using NobleMuffins.TurboSlicer;
using UnityEngine;
using UnityEngine.Assertions;

/// <summary>
/// Assumptions: 
/// Targets have one convex mesh.
/// Target has a single meshrenderer (No skinned meshes)
/// The slicer "blade" is omnidirectional, it cuts in any direction (think lightsaber rather than metal sword blade). The axis of the blade matches the local Y axis and passes through the center of local space (no x/z offset)
/// Targets pivots are at their center.
/// </summary>
public class BeatSlicer : MonoBehaviour
{    
    private new Rigidbody rigidbody;

    // Center of mass (mid blade if collider properly setup to match blade) is a reasonable approximation of the blades motion for impacts
    private Vector3 lastBladeCenterPositionWorld;
    private Vector3 currentBladeCenterPositionWorld;
    private Vector3 bladeMovementDirection = Vector3.zero;

    // For debugging
    //public Transform debugPointA;
    //public Transform debugPointB;
    //public Transform debugPointC;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();

        // Unity (2018.3.3)(bug?) won't calculate (or RecalculateCenterofMass) center of mass if isKinematic is true. Ensure isKinematic is set false so that CoM is recalculated then restore whatever the original value was.
        bool isKinematic = rigidbody.isKinematic;
        rigidbody.isKinematic = false;
        rigidbody.isKinematic = isKinematic;

        currentBladeCenterPositionWorld = transform.position + transform.TransformDirection(rigidbody.centerOfMass);
        lastBladeCenterPositionWorld = currentBladeCenterPositionWorld;
    }

    private void LateUpdate()
    {
        // Update blade motion and position vectors
        currentBladeCenterPositionWorld = transform.position + transform.TransformDirection(rigidbody.centerOfMass);
        bladeMovementDirection = currentBladeCenterPositionWorld - lastBladeCenterPositionWorld;
        bladeMovementDirection.Normalize();
        lastBladeCenterPositionWorld = currentBladeCenterPositionWorld;
    }

    private void OnTriggerEnter(Collider other)
    {
        BeatTarget target = other.GetComponentInParent<BeatTarget>();
        if (target != null)
        {            
            if (other is MeshCollider)
            {
                Assert.IsTrue(((MeshCollider)other).convex, "Target collider is not convex. Please use only convex colliders on targets");
            }

            Vector3 thirdSlicingPlanePoint = transform.position + bladeMovementDirection;            
            if (bladeMovementDirection == Vector3.zero)
            {
                // If the blade isn't moving then the third point would be on the line of the other two points and a slicing plane can not be created.
                // this is very rough, it produces a slice but quite possibly not oriented very well
                thirdSlicingPlanePoint = other.ClosestPoint(currentBladeCenterPositionWorld);
            }
            // Unhandled case: If the movement is perpendicular to the impacted face the slice will fail. (target motion isn't accounted for)

            SliceTarget(target, transform.position, transform.position + transform.up, thirdSlicingPlanePoint);
        }
    }

    /// <summary>
    /// Perform the slice using the plane defined by three points (A,B,C) on that plane
    /// </summary>
    /// <param name="target">To slice</param>
    /// <param name="planePointA"></param>
    /// <param name="planePointB"></param>
    /// <param name="planePointC"></param>
    private void SliceTarget(BeatTarget target, Vector3 planePointA, Vector3 planePointB, Vector3 planePointC)
    {
        //debugPointA.position = planePointA;
        //debugPointB.position = planePointB;
        //debugPointC.position = planePointC;

        TurboSlicer turboSlicer = TurboSlicerSingleton.Instance;
        bool destroyOriginal = true;
        TurboSlicerSingleton.Instance.SliceByTriangle(target.gameObject, new Vector3[] { planePointA, planePointB, planePointC }, destroyOriginal);

        // Can also slice by specifying the plane normal and a point on the plane. 
        // TurboSlicerSingleton.Instance.Slice(GameObject subject, Vector4 planeInLocalSpace, bool destroyOriginal)
        // Note that the plane needs to be in the local space of the target and in Point-Normal form. This Point-Normal form plane can be created 
        // using MathHelpers.PointNormalPlane(Vector3 point, Vector3 normal). Remember, make sure your normal and point vectors are in the targets
        // local space before creating the PointNormalPlane.
    }

}
