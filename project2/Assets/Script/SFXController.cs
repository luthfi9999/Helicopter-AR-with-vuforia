using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        playAudio("welcome",1.5f);
        playAudio("instruction",3.5f);
    }

    // Update is called once per frame
    public void playAudio(string audio,float delay=0){
        AudioController.Play(audio,100f,delay);
    }
}
