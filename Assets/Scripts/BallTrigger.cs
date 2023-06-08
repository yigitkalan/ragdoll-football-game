using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallTrigger : MonoBehaviour
{
    public float shot1,shot2;
    void OnTriggerStay(Collider ball)
    {
        if(ball.gameObject.tag == "Ball")
        {
            if(Input.GetMouseButtonDown(0)){
                ball.GetComponent<Rigidbody>().velocity = transform.TransformDirection (new Vector3 (0,0,-12));
            }
            if(Input.GetMouseButtonDown(1)){
                ball.GetComponent<Rigidbody>().velocity = transform.TransformDirection (new Vector3 (0,0,Random.Range(-shot1,-shot2)));
            }           
        }
    }
}
