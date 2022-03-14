using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    private const float defaultSensitivity = 10.0f;
    public float sensitivity = defaultSensitivity;

    public float drag = 1.0f;

    private Transform character;
    private Vector2 mouseDirection; //where mouse is
    private Vector2 smoothing; //smoothed cursors movements
    private Vector2 result; //final position

    private void Awake()
    {
        character = transform.parent;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (character != null)
        {
            mouseDirection = new Vector2(Input.GetAxisRaw("Mouse X") * sensitivity, Input.GetAxisRaw("Mouse Y") * sensitivity);
            smoothing = Vector2.Lerp(smoothing, mouseDirection, 1 / drag);
            result += smoothing;
            result.y = Mathf.Clamp(result.y, -90f, 90f);

            transform.localRotation = Quaternion.AngleAxis(-result.y, Vector3.right);
            character.rotation = Quaternion.AngleAxis(result.x, character.up);
        }

    }
}