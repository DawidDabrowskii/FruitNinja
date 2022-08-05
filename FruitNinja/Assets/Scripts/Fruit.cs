using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    public GameObject whole;
    public GameObject sliced;

    private Rigidbody fruitRigidbody;
    private Collider fruitCollider;
    private ParticleSystem juiceParticleEffect;

    private void Awake()
    {
        fruitRigidbody = GetComponent<Rigidbody>();
        fruitCollider = GetComponent<Collider>();
        juiceParticleEffect = GetComponentInChildren<ParticleSystem>();
    }

    private void Slice(Vector3 direction, Vector3 position, float force)
    {
        FindObjectOfType<GameManager>().IncreaseScore(); // getting access to GameManager from other script/it is public
        whole.SetActive(false);
        sliced.SetActive(true);

        fruitCollider.enabled = false;
        juiceParticleEffect.Play(); // play particle effects on Slice

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; // angle which we can rotate fruit
        sliced.transform.rotation = Quaternion.Euler(0f, 0f, angle); // rotation only in Z axes

        Rigidbody[] slices = sliced.GetComponentsInChildren<Rigidbody>();

        foreach (Rigidbody slice in slices)
        {
            slice.velocity = fruitRigidbody.velocity;
            slice.AddForceAtPosition(direction * force, position, ForceMode.Impulse); // transfer velocity from blade with direction, pos and impulse
        }
    }    

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Blade blade = other.GetComponent<Blade>();
            Slice(blade.direction, blade.transform.position, blade.sliceForce);
        }
    }
}



