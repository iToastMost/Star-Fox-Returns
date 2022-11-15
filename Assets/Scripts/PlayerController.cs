using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("General Movement Settings")]
    [SerializeField] InputAction movement;
    [Tooltip("How fast the ship moves up and down")] [SerializeField] float moveSpeed = 0f;
    [Tooltip("How far the ship can move horizontally")] [SerializeField] float xRange = 5f;
    [Tooltip("How far the ship can move vertically")] [SerializeField] float yRange = 5f;

    [Header("Screen position tuning")]
    [SerializeField] float positionPitchFactor = -2f;
    [SerializeField] float positionYawFactor = 2f;

    [Header("Player input tuning")]
    [SerializeField] float controlPitchFactor = -10f;
    [SerializeField] float controlRollFactor = -25f;

    [Header("Laser gun array")]
    [SerializeField] GameObject[] lasers;

    float xThrow;
    float yThrow;

    // Start is called before the first frame update
    void Start()
    {
        
    }



    // Update is called once per frame
    void Update()
    {
        ProcessRotation();
        ProcessPosition();
        ProcessFiring();
    }

    private void ProcessPosition()
    {
        //xThrow = movement.ReadValue<Vector2>().x;
        //yThrow = movement.ReadValue<Vector2>().y;

        xThrow = Input.GetAxis("Horizontal");
        yThrow = Input.GetAxis("Vertical");

        float xOffset = xThrow * Time.deltaTime * moveSpeed;
        float rawXPos = transform.localPosition.x + xOffset;
        float clampedXPos = Mathf.Clamp(rawXPos, -xRange, xRange);

        float yOffset = yThrow * Time.deltaTime * moveSpeed;
        float rawYPos = transform.localPosition.y + yOffset;
        float clampedYPos = Mathf.Clamp(rawYPos, -yRange, yRange);

        transform.localPosition = new Vector3(clampedXPos, clampedYPos, 0);
    }

    void ProcessRotation()
    {
        float pitchDueToPostion = transform.localPosition.y * positionPitchFactor;
        float pitchDueToControlThrow = yThrow * controlPitchFactor;

        float yawDueToPostion = transform.localPosition.x * positionYawFactor;
        float yawDueToControlThrow = xThrow * controlPitchFactor;

        

        float pitch = (pitchDueToPostion + pitchDueToControlThrow);
        float yaw = (yawDueToPostion);
        float roll = (xThrow * controlRollFactor);
        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    void ProcessFiring()
    {
        if ((Input.GetButton("Fire1")) || (Input.GetKey(KeyCode.Space)))
        {
            SetLasersActive(true);
        }
        else
        {
            SetLasersActive(false);
        }
    }
    void SetLasersActive(bool isActive)
    {

        foreach(GameObject laser in lasers)
        {
            var emissionModule = laser.GetComponent<ParticleSystem>().emission;
            emissionModule.enabled = isActive;
        }
    }
}
