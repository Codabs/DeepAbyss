using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableLight : MonoBehaviour
{
    //
    //VARIABLE
    //
    public Light lightComponent;
    [SerializeField] private Vector2 speedOfTheLightBreaking = new Vector2(0.2f, 0.3f);
    [SerializeField] private Material blackLightMaterial;
    [SerializeField] private Material whiteLightMaterial;
    private MeshRenderer meshRenderer;
    private Color lightColor;
    [SerializeField] private int viewMask = 8;

    //
    //MONOBEHAVIOUR
    //

    private void Awake()
    {
        meshRenderer = transform.parent.GetComponent<MeshRenderer>();
        lightColor = lightComponent.color;
    }
    private void Reset()
    {
        try
        {
            lightComponent = gameObject.GetComponent<Light>();
            SphereCollider collider = gameObject.AddComponent<SphereCollider>();
            collider.radius = lightComponent.range;
            collider.isTrigger = true;
        }
        catch { }
    }
    private void OnTriggerEnter(Collider other)
    {
        print("e");
        if(other.gameObject.TryGetComponent<EntityBrain>(out EntityBrain brain))
        {
            print("e");
            if (!Physics.Linecast(transform.position, other.transform.position, viewMask))
            {
                brain.entityBody.IsTheBodyAffectedByALight(transform.parent.gameObject);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent<EntityBrain>(out EntityBrain brain))
        {
            brain.entityBody.ThisLightDoesntAffectedTheBody(transform.parent.gameObject);
        }
    }
    //
    //FONCTION
    //
    public void button()
    {
        StartCoroutine(ShutDownTheLight());
    }
    public IEnumerator ShutDownTheLight()
    {
        bool flipflop = false;
        for (int i = 0; i <= 5; i++)
        {
            yield return new WaitForSeconds(UnityEngine.Random.Range(speedOfTheLightBreaking.x, speedOfTheLightBreaking.y));
            if (flipflop)
            {
                DeactiveLight();
                flipflop = false;
            }
            else
            {
                ActivateLight();
                flipflop = true;
            }
        }
        DestroyAll();
    }
    private void ActivateLight()
    {
        lightComponent.color = lightColor;
        meshRenderer.material = whiteLightMaterial;
    }

    private void DeactiveLight()
    {
        lightComponent.color = Color.black;
        meshRenderer.material = blackLightMaterial;
    }
    private void DestroyAll()
    {
        DeactiveLight();
        Destroy(gameObject);
    }
    //
    //GIZMOS
    //
    private void OnDrawGizmos()
    {
        try
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, lightComponent.range);
        }
        catch
        {

        }
    }
}
