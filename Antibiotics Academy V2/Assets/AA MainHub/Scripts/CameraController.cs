using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        GetComponent<UnityEngine.Camera>().orthographicSize = 6;
    }

}
