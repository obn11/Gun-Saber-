using UnityEngine;
using UnityEngine.Assertions;

[RequireComponent(typeof(Sliceable))]
public class BeatTarget : MonoBehaviour
{
    public Sliceable Sliceable { get; private set; }

    void Awake()
    {
        Sliceable = GetComponent<Sliceable>();
        Assert.IsNotNull(Sliceable, "Sliceable component required");
    }
}
