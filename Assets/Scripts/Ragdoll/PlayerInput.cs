using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [HideInInspector]
    public float _horizontal;
    [HideInInspector]
    public float _vertical;

    [HideInInspector]
    public float _mouseX;
    [HideInInspector]
    public float _mouseY;

    public event Action onJump = delegate {  };

    public bool isRunning;
    public bool isJumping;

    private KeyCode runKey;
    private KeyCode jumpKey;

    PhotonView PV;

    private void Awake()
    {
        PV = GetComponent<PhotonView>();
    }
    // Start is called before the first frame update
    void Start()
    {
        runKey = KeyCode.LeftShift;
        jumpKey = KeyCode.Space;
    }

    // Update is called once per frame
    void Update()
    {
        if (!PV.IsMine)
        {
            return;
        }
        GetMoveInput(); 
        CheckRunning();
        GetMouseInput();
        CheckJumping();

    }
    void GetMoveInput(){
        _horizontal = Input.GetAxisRaw("Horizontal");
        _vertical= Input.GetAxisRaw("Vertical");
    }
    void CheckRunning(){
        if(Input.GetKeyDown(runKey)){
            isRunning = true;
        }
        if(Input.GetKeyUp(runKey)){
            isRunning  = false;
        }
    }

    void GetMouseInput(){
        _mouseX = Input.GetAxis("Mouse X");
        _mouseY = Input.GetAxis("Mouse Y");
    }

    void CheckJumping()
    {
        if (Input.GetKeyDown(jumpKey))
        {
            onJump();
        }
    }
}
