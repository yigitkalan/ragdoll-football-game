using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawnpoint : MonoBehaviour
{
    [SerializeField] GameObject go;

    private void Awake()
    {
        go.SetActive(false);
    }
}
