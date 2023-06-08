using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    // Start is called before the first frame update
    public bool ball;
    public GameObject ballObj;

    public float shot1,shot2;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(ball)
        {
            if(Input.GetMouseButtonDown(0)){
                ballObj.GetComponent<Rigidbody>().velocity = transform.TransformDirection (new Vector3 (0,0,6));
            }
            if(Input.GetMouseButtonDown(1)){
                ballObj.GetComponent<Rigidbody>().velocity = transform.TransformDirection (new Vector3 (0,0,Random.Range(shot1,shot2)));
            }           
        }

    }
}
