using System.Collections.Generic;
using UnityEngine;

public class HitchhikeController : MonoBehaviour
{
  public GameObject cube;
  public Transform head;
  public List<HandWrap> handWraps;
  List<OVREyeGaze> eyeGazes;
  int maxRaycastDistance = 100;
  void Start()
  {
    eyeGazes = new List<OVREyeGaze>(GetComponents<OVREyeGaze>());
    ActivateHandWrap(handWraps[0]);
  }

  void Update()
  {
    if (eyeGazes == null) return;
    if (!eyeGazes[0].EyeTrackingEnabled)
    {
      Debug.Log("Eye tracking not working");
      return;
    }

    Ray gazeRay = GetGazeRay();
    int layerMask = 1 << LayerMask.NameToLayer("Hitchhike");

    RaycastHit closestHit = new RaycastHit();
    float closestDistance = float.PositiveInfinity;
    foreach (var hit in Physics.RaycastAll(gazeRay, maxRaycastDistance, layerMask))
    {
      // finding a nearest hit
      var colliderDistance = Vector3.Distance(hit.collider.gameObject.transform.position, head.transform.position);
      if (colliderDistance < closestDistance)
      {
        closestHit = hit;
        closestDistance = colliderDistance;
      }
    }

    HandWrap currentGazeWrap = null;
    if (closestDistance < float.PositiveInfinity)
    {
      currentGazeWrap = GetHandWrapFromHit(closestHit);
      ActivateHandWrap(currentGazeWrap);
    }
  }

  private void ActivateHandWrap(HandWrap afterWrap)
  {
    handWraps.ForEach((e) => { e.SetEnabled(e == afterWrap); });
  }
  private HandWrap GetActiveHandWrap()
  {
    HandWrap wrap = null;
    handWraps.ForEach((e) => { if (e.isEnabled) wrap = e; });
    return wrap;
  }
  private HandWrap GetHandWrapFromHit(RaycastHit hit)
  {
    var target = hit.collider.gameObject;
    return target.GetComponentInParent<HandWrap>();
  }
  private int GetHandWrapIndex(HandWrap wrap)
  {
    var i = handWraps.FindIndex(e => e == wrap);
    return i;
  }


  Vector3? filteredDirection = null;
  Vector3? filteredPosition = null;
  float ratio = 0.3f;
  private Ray GetGazeRay()
  {
    Vector3 direction = Vector3.zero;
    eyeGazes.ForEach((e) => { direction += e.transform.forward; });
    direction /= eyeGazes.Count;

    if (!filteredDirection.HasValue)
    {
      filteredDirection = direction;
      filteredPosition = head.transform.position;
    }
    else
    {
      filteredDirection = filteredDirection.Value * (1 - ratio) + direction * ratio;
      filteredPosition = filteredPosition.Value * (1 - ratio) + head.transform.position * ratio;
    }
    return new Ray(filteredPosition.Value, filteredDirection.Value);
  }


}