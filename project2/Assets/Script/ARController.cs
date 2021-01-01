using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
public class ARController : MonoBehaviour
{
    public GameObject poseIndicator;
    public GameObject helicopter;
    public ARRaycastManager raycastManager;
    public Camera arCam;
    private Pose pose;
    private bool isPosePlacementValid=false;
    // Start is called before the first frame update
    // Update is called once per frame
    void Update()
    {
        UpdatePosePlacement();
        UpdatePoseIndicator();
        if(isPosePlacementValid&&Input.touchCount>0&&Input.GetTouch(0).phase==TouchPhase.Began){
            PlaceObject();
        }
    }
    void PlaceObject(){
        Instantiate(helicopter,pose.position,pose.rotation);
    }
    void UpdatePoseIndicator(){
        if(isPosePlacementValid){
           poseIndicator.SetActive(true);
           poseIndicator.transform.SetPositionAndRotation(pose.position,pose.rotation);
        }
        else{
            poseIndicator.SetActive(false);
        }
    }
    void UpdatePosePlacement(){
        Vector2 screenCenter=arCam.ViewportToScreenPoint(new Vector3(0.5f,0.5f));
        List<ARRaycastHit> raycastHit= new List<ARRaycastHit>();
        raycastManager.Raycast(screenCenter,raycastHit,TrackableType.Planes);
        isPosePlacementValid=raycastHit.Count>0;
        if(isPosePlacementValid){
            pose=raycastHit[0].pose;
            Vector3 cameraForward=Camera.current.transform.forward;
            Vector3 cameraBearing=new Vector3(cameraForward.x,0,cameraForward.z).normalized;
            pose.rotation=Quaternion.LookRotation(cameraBearing);
        }
    }
}
