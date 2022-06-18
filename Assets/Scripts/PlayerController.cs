using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed, gravity;

    [SerializeField] Transform carryPoint;
    CharacterController characterController;
    Collider portableObject;

    bool carrying;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        carrying = false;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) 
            * speed * Time.deltaTime;

        characterController.Move(movement);
        characterController.Move(Vector3.down * gravity);

        if(movement.normalized.magnitude > 0.3f)
            transform.rotation = Quaternion.LookRotation(movement);

        if (Input.GetButtonDown("Interact"))
        {
            if (!carrying && portableObject != null)
                carrying = true;
            else if(carrying)
            {
                carrying = false;
                portableObject.transform.position = GetComponentInChildren<BoxCollider>().transform.position;
            }
        }

        if (carrying)
            portableObject.transform.position = carryPoint.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!carrying && other.tag == "PortableObject")
            portableObject = other;
    }

    private void OnTriggerExit(Collider other)
    {
        if (!carrying && portableObject == other)
            portableObject = null;
    }
}
