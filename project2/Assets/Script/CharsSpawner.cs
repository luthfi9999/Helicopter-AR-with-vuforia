using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class CharsSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject button;
    [SerializeField]
    private GameObject targetButton;
    [SerializeField]
    [Tooltip("Insert textmeshpro parent Here")]
    private GameObject parent;
    [SerializeField]
    [Tooltip("Insert TextMeshPro with your Format Here")]
    private GameObject charGenerator;
    [SerializeField]
    [Tooltip("Insert Word")]
    private string words;
    [SerializeField]
    [Tooltip("Please Enter The Number That Matches The Length Of The Words Variable")]
    private List<Transform> wordsTarget;
    private GameObject test;
    private int counter=0;
    // Start is called before the first frame update
    void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(counter<words.Length&&!LeanTween.isTweening()){
            test=Instantiate(charGenerator,this.transform.position,this.transform.rotation,parent.transform) ;
            test.GetComponent<TextMeshProUGUI>().text=words[counter].ToString();
            LeanTween.move(test,wordsTarget[counter].transform.position,0.4f).setEase(LeanTweenType.animationCurve);
            counter++;
        }
        else if(counter==words.Length&&!LeanTween.isTweening()){
            //Debug.Log("sdad");
            LeanTween.move(button,targetButton.transform.position,2).setEase(LeanTweenType.animationCurve);
            //LeanTween.rotateZ(button,359,2f).setEase(LeanTweenType.animationCurve);
            LeanTween.rotateAroundLocal(button,Vector3.forward,360f,2f);
            counter++;
        }
    }
}
