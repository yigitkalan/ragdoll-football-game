using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyAnimation : MonoBehaviour
{
    [SerializeField]
    private Transform targetLimb;

    [SerializeField ]
    private bool mirror;

    private ConfigurableJoint cj;
    private RagdollMovement rm;
    // Start is called before the first frame update
    void Start()
    {
        rm = FindObjectOfType<RagdollMovement>();
        cj = GetComponent<ConfigurableJoint>();
    }

    // Update is called once per frame
    void Update()
    {
        if(rm.canMove){
            MirrorAnimation();
        }
    }
    void MirrorAnimation(){
        if(!mirror){
            cj.targetRotation = targetLimb.rotation;
        }
        else{
            cj.targetRotation = Quaternion.Inverse(targetLimb.rotation);
        }
    }
}
