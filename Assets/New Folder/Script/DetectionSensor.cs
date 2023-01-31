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
            if(hit.collider.gameObject.tag == "Generator")
            {
                Generator _generator = hit.collider.GetComponent<Generator>();
                if (_generator.isOn) return ;
                _generator.On();
                _generator.door.generatorManager.ValidateStep();
                _generator.door.generatorManager.SpawnCurrentStep();
                _generator.door.VerifyAllGenerators();
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
