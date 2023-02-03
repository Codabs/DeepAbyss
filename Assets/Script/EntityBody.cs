using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityBody : MonoBehaviour
{
    //
    //VARIABLE
    //
    [SerializeField] private Material bigDaddyTexture;
    [SerializeField] private Animator entityAnimator;
    [SerializeField] private EntityBrain brain;
    private SkinnedMeshRenderer[] meshRenderers = new SkinnedMeshRenderer[10];

    Vector3 previousPosition = Vector3.zero;

    //
    //MONOBEHAVIOUR
    //
    private void Awake()
    {
        meshRenderers = gameObject.GetComponentsInChildren<SkinnedMeshRenderer>();
    }
    private void Update()
    {
        entityAnimator.speed = brain.entityPathfing.navMeshAgent.speed / 2;
    }
    //
    //FONCTION
    //

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
