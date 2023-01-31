using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionSensor : MonoBehaviour
{
    public Camera playerCamera;

    public bool useSensor = true;
    public float maxDistance = 300f;

    public RaycastHit hit;

    public void Detect()
    {
        Vector3 cameraPosition = playerCamera.transform.position;
        Vector3 cameraForward = playerCamera.transform.forward;

        if (Physics.Raycast(cameraPosition, cameraForward, out hit, maxDistance))
        {
            if(hit.collider.gameObject.tag == "Generator" && !hit.collider.GetComponent<Generator>().isOn)
            {
                hit.collider.GetComponent<Generator>().On();
                hit.collider.GetComponent<Generator>().door.generatorManager.ValidateStep();
                hit.collider.GetComponent<Generator>().door.generatorManager.SpawnCurrentStep();
                hit.collider.GetComponent<Generator>().door.VerifyAllGenerators();
            }

            if(hit.collider.gameObject.tag == "Door")
            {
                hit.collider.GetComponent<DoorCollider>().door.Exit();
            }
            Debug.DrawRay(cameraPosition, cameraForward * maxDistance, Color.green);
        }
        else
        {
            Debug.DrawRay(cameraPosition, cameraForward * maxDistance, Color.red);
        }
    }
    public void Update()
    {
        if (useSensor && Input.GetButtonDown("Fire1"))
        {
            Detect();
        }
    }
}
