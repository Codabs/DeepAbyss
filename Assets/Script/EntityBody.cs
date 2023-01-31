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
    [SerializeField] private Animator entityAnimator;
    [SerializeField] private EntityBrain brain;
    private SkinnedMeshRenderer[] meshRenderers = new SkinnedMeshRenderer[10];
    private List<GameObject> lightThatAffectTheBody = new();

    Vector3 previousPosition = Vector3.zero;
    float velocityBooster = 1;

    //
    //MONOBEHAVIOUR
    //
    private void Awake()
    {
        meshRenderers = gameObject.GetComponentsInChildren<SkinnedMeshRenderer>();
    }
    private void Update()
    {
        AnimationManager();
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
    public void AnimationManager()
    {
        Vector3 velocity = transform.position - previousPosition;
        previousPosition = brain.transform.position;
        float value = Mathf.Abs(velocity.x) + Mathf.Abs(velocity.z);
        entityAnimator.SetFloat("Velocity", value * velocityBooster);
    }
}
