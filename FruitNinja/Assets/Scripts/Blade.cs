using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blade : MonoBehaviour
{
    private Camera mainCamera;
    private bool slicing;
    private Collider bladeCollider;

    [SerializeField] private float minSliceVelocity = 0.01f;

    private TrailRenderer bladeTrail;
    public Vector3 direction { get; private set; } // other classes can read it but only here value can be changed
    public float sliceForce = 5f;
    private void Awake()
    {
        mainCamera = Camera.main;
        bladeCollider = GetComponent<Collider>();
        bladeTrail = GetComponentInChildren<TrailRenderer>();
    }

    private void OnEnable() // makse sure slicing is off also OnEnable
    {
        StopSlicing();
    }
    private void OnDisable() // stop slicing when blade becomes disabled
    {
        StopSlicing();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))  // when left mouse button (0) is pressed down slice,right stop, otherwise continue
        {
            StartSlicing();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            StopSlicing();
        } 
        else if (slicing)
        {
            ContinueSlicing();
        }
    }
    
    private void StartSlicing()
    {
        Vector3 newPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        newPosition.z = 0f;

        transform.position = newPosition; // as soon as clicking mouse button blade will move to this position and it will enable slicing

        slicing = true;
        bladeCollider.enabled = true;

        bladeTrail.enabled = true;
        bladeTrail.Clear(); // it will clear all the points from renderer
    }

    private void StopSlicing()
    {
        slicing = false;
        bladeCollider.enabled = false;
        bladeTrail.enabled= false;
    }    

    private void ContinueSlicing()
    {
        Vector3 newPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition); // conversion from screenspace to worldspace, mouse pos is 2d and we set it to 3d
        newPosition.z = 0f;

        direction = newPosition - transform.position;

        float velocity = direction.magnitude / Time.deltaTime; // how much vector has moved over past frame
        bladeCollider.enabled = velocity > minSliceVelocity; // if our velocity is greater than 0.01 collider is enabled

        transform.position = newPosition;
    }
}
