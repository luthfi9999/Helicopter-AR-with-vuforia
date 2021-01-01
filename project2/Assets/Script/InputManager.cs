using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class InputManager : MonoBehaviour
{
    public GameObject buttonContainer;
    public SFXController sFXController;
    public GameObject heliContainer;
    public Camera arCam;
    public Animator fade;
    private ARRaycastManager rayManager;
    private GameObject visual;
    private GameObject trivia;
    private GameObject heliObject;
    private Quaternion savedRotation;
    private Vector3 savedScale;
    private bool opening=false;
    private float diff=1,diffSwitch=0,dd=0;
     void Start()
    {
        // get the component
        rayManager = FindObjectOfType<ARRaycastManager>();
        visual = transform.GetChild(0).gameObject;
        // hide the placement visual
        //heliContainer.SetActive(false);
        buttonContainer.SetActive(false);
        visual.SetActive(false);
    }

     void Update()
    {
        if(diffSwitch!=0){
            triviaAnim();
        }
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
            if(!opening){
                sFXController.playAudio("alert1");
                sFXController.playAudio("instruction2",2.5f);
                opening=true;
            }
            transform.SetPositionAndRotation(hits[0].pose.position,hits[0].pose.rotation);
            if(GameObject.FindWithTag("helicopter")==null){
                visual.SetActive(true);
                buttonContainer.SetActive(true);
               if(Input.touchCount>0&&Input.GetTouch(0).phase==TouchPhase.Began){
                    sFXController.playAudio("alert2");
                    sFXController.playAudio("alert3",6f);
                    sFXController.playAudio("instruction3",9f);
                    heliObject=Instantiate(heliContainer,transform.position,transform.rotation)as GameObject;
                    trivia=heliObject.transform.GetChild(1).gameObject;
                    //trivia.SetActive(false);
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
            sFXController.playAudio("alert5");
            heliObject.transform.rotation=savedRotation;
            heliObject.transform.localScale=savedScale;
    }
    public void Home(){
       if(GameObject.FindWithTag("helicopter")!=null){
            sFXController.playAudio("exit");
            StartCoroutine(delay(2f));
            fade.SetTrigger("FadeOut");
       }
       
      // SceneManager.LoadScene(0);
    }
    public void playAnim(){
        if(GameObject.FindWithTag("helicopter")!=null){
            sFXController.playAudio("alert4");
            heliObject.GetComponent<Animator>().Play("Fly Out");
        }
    }
    public void setTrivia(){
        if(GameObject.FindWithTag("helicopter")!=null){
            if(diff==0){
                diffSwitch=1;
                sFXController.playAudio("trivia2");
            //trivia.SetActive(false);
            }
            else if(diff==1){
                diffSwitch=-1;
                sFXController.playAudio("trivia1");
            //trivia.SetActive(true);
             }
        }

        
    }
    IEnumerator delay(float sec){
        yield return new WaitForSeconds(sec);
    }
    private void triviaAnim(){
        //dd=trivia.GetComponent<Renderer>().material.GetFloat("_Level");
        if(diffSwitch==1){
            diff+=0.02f;
            trivia.GetComponent<Renderer>().material.SetFloat("_Level",diff);
            if(diff>0.95F){
                diff=1;
                diffSwitch=0;
            }
        }
        else if(diffSwitch==-1){
            diff-=0.02f;
            trivia.GetComponent<Renderer>().material.SetFloat("_Level",diff);
             if(diff<0.05F){
                diff=0;
                diffSwitch=0;
            }
        }
    }
}
