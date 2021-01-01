using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class test : MonoBehaviour
{
    public GameObject a;
    private float b=1;
    private int c=0;
    public Text data;
    void Start(){
        
    }
    void Update(){
        data.text=b.ToString();
        Debug.Log("dds");
        if (Input.GetKeyDown(KeyCode.Space)){
            if(b==0f){
                Debug.Log("ooo");
                c=1;
            }
            else if(b==1f){
                Debug.Log("ihihih");
                c=-1;
            }
        }
        if(c==1){
            b+=0.05f;
            a.GetComponent<Renderer>().material.SetFloat("_Level",b);
            if(b>0.95f){
                b=1;
                c=0;
            }
        }
        else if(c==-1){
            b-=0.05f;
            a.GetComponent<Renderer>().material.SetFloat("_Level",b);
            if(b<0.05f){
                b=0;
                c=0;
            }
        }
        
    }
    // Start is called before the first frame update
}
