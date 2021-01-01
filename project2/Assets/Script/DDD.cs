using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
public class PlacementIndicator : MonoBehaviour
{
    public GameObject heliContainer;
    public Camera arCam;
    public Animator fade;
    private ARRaycastManager rayManager;
    private GameObject visual;
    private GameObject trivia;
    private GameObject heliObject;
    private Quaternion savedRotation;
    private Vector3 savedScale;
     void Start()
    {
        // get the component
        rayManager = FindObjectOfType<ARRaycastManager>();
        visual = transform.GetChild(0).gameObject;
        // hide the placement visual
        //heliContainer.SetActive(false);
        visual.SetActive(false);

    }

     void Update()
    {
        Vector2 screenCenter=arCam.ViewportToScreenPoint(new Vector3(0.5f,0.5f));
        // shoot a raycast from the center of the screen
        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        rayManager.Raycast(screenCenter, hits, TrackableType.Planes);
        // if we hit AR Plane, Update the postion and rotation
        if(GameObject.FindWithTag("trivia")!=null){
            trivia.transform.SetPositionAndRotation(trivia.transform.position,hits[0].pose.rotation);
        }
        if (hits.Count > 0)
        {
            transform.SetPositionAndRotation(hits[0].pose.position,hits[0].pose.rotation);
            if(GameObject.FindWithTag("helicopter")==null){
                visual.SetActive(true);
               if(Input.touchCount>0&&Input.GetTouch(0).phase==TouchPhase.Began){
                    heliObject=Instantiate(heliContainer,transform.position,transform.rotation)as GameObject;
                    trivia=heliObject.transform.GetChild(1).gameObject;
                    trivia.SetActive(false);
                    heliObject=heliObject.transform.GetChild(0).gameObject;
                    savedRotation=heliObject.transform.rotation;
                    savedScale=heliObject.transform.localScale;
                    visual.SetActive(false);
                }
            }
            else{
                if(Input.touchCount==2){
                    Touch firstFinger=Input.GetTouch(0);
                    Touch secondFinger=Input.GetTouch(1);
                    Vector2 firstFingerPrevPos=firstFinger.position-firstFinger.deltaPosition;
                    Vector2 secondFingerPrevPos=secondFinger.position-secondFinger.deltaPosition;
                    float prevMagnitude=(firstFingerPrevPos-secondFingerPrevPos).magnitude;
                    float currentMagnitude=(firstFinger.position-secondFinger.position).magnitude;
                    float difference=currentMagnitude-prevMagnitude;
                    zoom(difference*0.0001f);
                }
                else if(Input.touchCount==1){
                    Touch touch=Input.GetTouch(0);
                    if(touch.phase==TouchPhase.Moved){
                        Quaternion rot=Quaternion.Euler(0f,-touch.deltaPosition.x*0.1f,0f);
                        heliObject.transform.rotation=rot*heliObject.transform.rotation;
                    }
                }
            }
        }
        else{
            visual.SetActive(false);
        }
    }
    void zoom(float scaleFactor){
        float clamp=Mathf.Clamp(heliObject.transform.localScale.x+scaleFactor,0.03f,0.07f);
        heliObject.transform.localScale=new Vector3(clamp,clamp,clamp);
    }
    public void reset(){
        if(GameObject.FindWithTag("helicopter")!=null){
            heliObject.transform.rotation=savedRotation;
            heliObject.transform.localScale=savedScale;
        }
        else{
            return;
        }
    }
    public void Home(){
        fade.SetTrigger("FadeOut");
    }
    public void playAnim(){
        heliObject.GetComponent<Animator>().Play("Fly Out");
    }
    public void setTrivia(){
        if(GameObject.FindWithTag("trivia")!=null){
            trivia.SetActive(false);
        }
        else{
            trivia.SetActive(true);
        }
    }
}
