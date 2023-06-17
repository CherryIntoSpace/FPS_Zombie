using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextStage : MonoBehaviour
{
    public bool IsLast;
    public GameObject Inform;
    public GameObject goal;


    // Start is called before the first frame update
    void Start()
    {
        IsLast = false;
    }

    // Update is called once per frame
    void Update()
    {   
        if(IsLast == true)
        {
            Inform.SetActive(true);
            goal.SetActive(true);
        }
    }
}
