using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandWrap : MonoBehaviour
{
  public Material enabledMaterial;
  public Material disabledMaterial;
  public SkinnedMeshRenderer meshRenderer;
  public bool log = false;
  public bool isEnabled { get; private set; }
  void Start()
  {
    GetRenderer().material = disabledMaterial;
  }
  public void SetEnabled(bool enabled)
  {
    isEnabled = enabled;
    // GetRenderer().material = enabled ? enabledMaterial : disabledMaterial;
    GetRenderer().material.color = new Color(0.11f, 0.46f, 1f, enabled ? 1f : 0.2f);
    GetSkelton().isUpdating = enabled;
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
