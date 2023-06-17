using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigAnimation : MonoBehaviour
{
    public Animator playerAnimator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerAnimator.GetBool("isShooting"))
        {
            playerAnimator.SetBool("isShooting", false);
        }
    }
}
