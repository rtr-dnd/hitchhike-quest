using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandWrap : MonoBehaviour
{
    public Material enabledMaterial;
    public Material disabledMaterial;
    public bool isEnabled { get; private set; }
    public void SetEnabled (bool enabled)
    {
        GetRenderer().material = enabled ? enabledMaterial : disabledMaterial;
        GetSkelton().isUpdating = enabled;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private SkinnedMeshRenderer GetRenderer()
    {
        return gameObject.GetComponentInChildren<SkinnedMeshRenderer>();
    }
    private OVRSkeleton GetSkelton()
    {
        return gameObject.GetComponentInChildren<OVRSkeleton>();
    }
}
