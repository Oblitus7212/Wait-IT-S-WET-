using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    public Transform MainCamera;
    private void Update()
    {
        transform.LookAt(MainCamera);
    }
}
