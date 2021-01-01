using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class heliAnim : MonoBehaviour
{
    public Animator heliAnimator;
    // Start is called before the first frame update
    void Start()
    {
        heliAnimator.Play("Fly In");
    }
}
