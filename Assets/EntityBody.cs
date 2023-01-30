using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityBody : MonoBehaviour
{
    //
    //VARIABLE
    //
    [SerializeField] private Material bigDaddyTexture;
    float indiceOfVisibility = 1;
    private SkinnedMeshRenderer[] meshRenderers = new SkinnedMeshRenderer[10];
    private List<GameObject> lightThatAffectTheBody = new();

    //
    //MONOBEHAVIOUR
    //
    private void Awake()
    {
        meshRenderers = gameObject.GetComponentsInChildren<SkinnedMeshRenderer>();
    }
    //
    //FONCTION
    //
    public void IsTheBodyAffectedByALight(GameObject parent)
    {
        lightThatAffectTheBody.Add(parent);
        indiceOfVisibility += 0.5f;
        BecomingInvisible(indiceOfVisibility);
    }
    public void ThisLightDoesntAffectedTheBody(GameObject parent)
    {
        if(lightThatAffectTheBody.Contains(parent))
        {
            indiceOfVisibility -= 0.5f;
            BecomingInvisible(indiceOfVisibility);
        }
    }
    public void BecomingInvisible(float opacity)
    {
        if (opacity <= 0) opacity = 0;
        if (opacity >= 1) opacity = 1;
        foreach (SkinnedMeshRenderer renderer in meshRenderers)
        {
            renderer.material.SetColor("_BaseColor", new Vector4(255, 255, 255, opacity));
            renderer.material.SetFloat("_Smoothness", 1 - opacity);
        }
    }
}
