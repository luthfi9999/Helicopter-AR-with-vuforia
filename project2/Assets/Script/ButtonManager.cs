using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ButtonManager : MonoBehaviour
{
    private Button button;
    // Start is called before the first frame update
    void Start()
    {
        button=GetComponent<Button>();
        button.onClick.AddListener(SelectObj);
    }
    void SelectObj(){

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
