using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class fadeControl : MonoBehaviour
{
    // Start is called before the first frame update
    public void ChangeScene(){
        if(SceneManager.GetActiveScene().buildIndex==0){
            SceneManager.LoadScene(1);
        }
        else{
            SceneManager.LoadScene(0);
        }
    }
}
