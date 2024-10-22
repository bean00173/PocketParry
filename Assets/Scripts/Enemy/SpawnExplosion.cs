using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class SpawmExplosion : MonoBehaviour
{

    public float explosionForce;
    public float radius;
    public float upwardsForce;
    public bool explode;
    public bool solo;

    public Transform explodePos;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ActivateRb());
    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator ActivateRb()
    {
        //yield return new WaitForSeconds(5f);

        if (!solo)
        {
            //Collider mainCol = this.GetComponent<Collider>();
            //mainCol.enabled = false;

            //this.GetComponent<Rigidbody>().useGravity = false;

            Collider[] colliders = GetComponentsInChildren<Collider>(true);

            //Debug.Log($"{name} {GetInstanceID()} has {colliders.Length} children colliders.");

            foreach (Collider hit in colliders)
            {
                //if (hit != mainCol)
                //{
                //    hit.enabled = true;

                //    Rigidbody rb = hit.GetComponent<Rigidbody>();

                //    rb.isKinematic = false;
                //    rb.useGravity = true;

                //    if (explode) rb.AddExplosionForce(explosionForce, explodePos.position, radius, upwardsForce);

                //    //Debug.Log($"Added Explosion Force to {rb.name}.");
                //}

                hit.enabled = true;

                Rigidbody rb = hit.GetComponent<Rigidbody>();

                rb.isKinematic = false;
                rb.useGravity = true;

                if (explode) rb.AddExplosionForce(explosionForce, explodePos.position, radius, upwardsForce);

                //Debug.Log($"Added Explosion Force to {rb.name}.");
            }

            yield return null;
        }
    }
}