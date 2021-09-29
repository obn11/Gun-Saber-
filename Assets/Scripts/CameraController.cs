using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	Vector2 rotation = Vector2.zero;
	public float speed = 3;

    private void Start()
    {
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}

	//Camera follows mouse
    void Update()
	{
		if (!PauseMenu.paused && !KatanaController.slicing)
		{
			rotation.y += Input.GetAxis("Mouse X");
			rotation.x += -Input.GetAxis("Mouse Y");
			transform.eulerAngles = (Vector2)rotation * speed;
		}
	}
}
