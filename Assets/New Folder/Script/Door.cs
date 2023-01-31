using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public GeneratorManager generatorManager;
    public Light lightRed;
    public Light lightGreen;
    public GameObject doorCollider;

    public void Exit()
    {
        if(VerifyAllGenerators())
        {
            Debug.Log("OKI");
            doorCollider.SetActive(false);
        }
        else
        {
            Debug.Log("NOK");
        }
    }

    public bool VerifyAllGenerators()
    {
        bool verification = true;

        foreach (Generator generator in generatorManager.generators)
        {
            if (!generator.isOn)
            {
                verification = false;
                break;
            }
        }

        if (verification)
        {
            lightRed.enabled = false;
            lightGreen.enabled = true;
        }
        return verification;
    }
}
