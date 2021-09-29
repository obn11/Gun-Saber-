using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KatanaController : MonoBehaviour
{
    public GameObject gun;
    public Camera camera;
    public float moveSpeed = 0.5f;
    public float zDistance;
    public float sliceSpeed = 10f;

    public static bool slicing;
    
    Vector3 defultPos;
    Quaternion defultRot;
    Vector3 startPosition;
    Vector3 mousePosition;

    private void Start()
    {
        defultPos = gameObject.transform.localPosition;
        defultPos = gameObject.transform.position;
        defultRot = gameObject.transform.localRotation;
    }

    // Checks quickswaping and slashing NOT WORKING
    void Update()
    {
        //Swap to gun
        if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.E) || Input.GetAxis("Mouse ScrollWheel") != 0f)
        {
            QuickSwap();
        }

        //Slash
        if (Input.GetMouseButtonDown(0))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            slicing = true;
            startPosition = Input.mousePosition;

        }

        if (slicing)
        {
            var mousePos = Input.mousePosition;
            this.transform.position = camera.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y-100, zDistance));

            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x,
                                                      transform.localEulerAngles.y,
                                                      transform.localEulerAngles.z);
        }

        //Stop Slash
        if (Input.GetMouseButtonUp(0))
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            slicing = false;
            Slash();
            gameObject.transform.localPosition = defultPos;
            transform.localRotation = defultRot;
            
        }

        
    }
    
    //Controls the slashing angle (Would have been an animation)
    void Slash()
    {
        Vector3 mouseDelta = Input.mousePosition - startPosition;
        float angle = Mathf.Atan2(mouseDelta.x, mouseDelta.y) * Mathf.Rad2Deg;
        if (angle < 0) angle += 360;
        
        //slide slice
        if (/*angle > 45 && angle <= 135*/ true)
        {
            transform.Rotate(0, 0, -90);
        }
    }

    //Swaps back to gun
    void QuickSwap()
    {
        gun.SetActive(true);
        gameObject.transform.parent.gameObject.SetActive(false);
    }

    
}
