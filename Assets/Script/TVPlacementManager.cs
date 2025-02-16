using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System.Collections.Generic;

public class TVPlacementManager : MonoBehaviour
{
    public GameObject tvPrefab; // Assign the TV prefab in the inspector
    private ARRaycastManager raycastManager;
    private List<ARRaycastHit> hits = new List<ARRaycastHit>();
    private List<GameObject> placedTVs = new List<GameObject>();

    void Start()
    {
        raycastManager = FindObjectOfType<ARRaycastManager>();
    }

    public void TryPlaceTV()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            if (raycastManager.Raycast(Input.GetTouch(0).position, hits, TrackableType.PlaneWithinPolygon))
            {
                Pose hitPose = hits[0].pose;
                GameObject newTV = Instantiate(tvPrefab, hitPose.position, hitPose.rotation);
                placedTVs.Add(newTV);
            }
        }
    }

    public List<GameObject> GetAllPlacedTVs()
    {
        return placedTVs;
    }
}