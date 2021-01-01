using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class charspawner : MonoBehaviour
{
    [SerializeField]
    private TextMeshPro charGenerator;
    private TextMeshPro test;
    // Start is called before the first frame update
    void Awake()
    {
        test=Instantiate(charGenerator,this.transform.position,this.transform.rotation);
        test.text="a";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
