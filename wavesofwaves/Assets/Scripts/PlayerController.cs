﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 6f;            // The speed that the player will move at.

    Vector3 movement;                   // The vector to store the direction of the player's movement.
    Animator anim;                      // Reference to the animator component.
    Rigidbody playerRigidbody;          // Reference to the player's rigidbody.
    int floorMask;                      // A layer mask so that a ray can be cast just at gameobjects on the floor layer.
    float camRayLength = 100f;          // The length of the ray from the camera into the scene.
    public GameObject Airblast;
    public GameObject Waterwave;
    private float airTimer;
    [HideInInspector]
    public float waterTimer;

    void Awake()
    {
        // Create a layer mask for the floor layer.
        floorMask = LayerMask.GetMask("Floor");

        // Set up references.
        playerRigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        airTimer = 0;
        waterTimer = 0;
        Airblast = Resources.Load<GameObject>("Prefabs/AirBlast");
        Waterwave = Resources.Load<GameObject>("Prefabs/WaterWave");
    }


    void FixedUpdate()
    {
        // Store the input axes.
        float h = Input.GetAxisRaw("xMoveKey");
        float v = Input.GetAxisRaw("yMoveKey");

        // Move the player around the scene.
        Move(h, v);

        // Turn the player to face the mouse cursor.
        if (waterTimer <= 0)
        {
            Turning();
        }
    }

    private void Update()
    {
        if (airTimer <= 0 && Input.GetButtonDown("Fire1"))
        {
            AirBlast();
            airTimer = 1f;
        }

        if (waterTimer <= 0 && Input.GetButtonDown("Fire2"))
        {
            WaterWave();
            waterTimer = 1f;
        }
        airTimer -= Time.deltaTime;
        waterTimer -= Time.deltaTime;
    }

    void Move(float h, float v)
    {
        // Set the movement vector based on the axis input.
        movement.Set(h, 0f, v);

        // Normalise the movement vector and make it proportional to the speed per second.
        movement = movement.normalized * speed * Time.deltaTime;

        // Move the player to it's current position plus the movement.
        playerRigidbody.MovePosition(transform.position + movement);
    }

    void Turning()
    {
        // Create a ray from the mouse cursor on screen in the direction of the camera.
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        // Create a RaycastHit variable to store information about what was hit by the ray.
        RaycastHit floorHit;

        // Perform the raycast and if it hits something on the floor layer...
        if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask))
        {
            // Create a vector from the player to the point on the floor the raycast from the mouse hit.
            Vector3 playerToMouse = floorHit.point - transform.position;

            // Ensure the vector is entirely along the floor plane.
            playerToMouse.y = 0f;

            // Create a quaternion (rotation) based on looking down the vector from the player to the mouse.
            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);

            // Set the player's rotation to this new rotation.
            playerRigidbody.MoveRotation(newRotation);
        }
    }

    void AirBlast()
    {
        GameObject.Instantiate(Airblast, transform.position, transform.rotation);
    }

    void WaterWave()
    {
        GameObject.Instantiate(Waterwave);
    }
}
