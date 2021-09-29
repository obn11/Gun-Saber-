using UnityEngine;

public class MathHelpers : MonoBehaviour
{
    /// <summary>
    /// Create a Point-Normal form plane from a position on the plane and normal of the plane.
    /// </summary>
    /// <param name="point">on the plane</param>
    /// <param name="normal">of the plane</param>
    /// <returns></returns>
    public static Vector4 PointNormalPlane(Vector3 point, Vector3 normal)
    {
        Vector4 result = (Vector4)normal.normalized;
        result.w = - (normal.x * point.x) - (normal.y * point.y) - (normal.z * point.z);

        return result;
    }
}
