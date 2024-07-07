using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastBossPlatform : MonoBehaviour
{
    private float minDisableTime = 5f; // minimum time to disable object
    private float maxDisableTime = 15f; // maximum time to disable object
    private float enableTime = 2f; // time to wait before enabling object again

    private float nextDisableTime;
    private GameObject JumpyPlatform;

    void Start()
    {
        JumpyPlatform = transform.GetChild(0).gameObject;
        nextDisableTime = Time.time + Random.Range(minDisableTime, maxDisableTime);
    }

    void Update()
    {
        // check if it's time to disable object
        if (Time.time >= nextDisableTime)
        {
            JumpyPlatform.SetActive(false); // disable object
            nextDisableTime = Time.time + Random.Range(minDisableTime, maxDisableTime); // set next disable time
            StartCoroutine(EnableObject(enableTime)); // start coroutine on separate object
        }
    }

    IEnumerator EnableObject(float delay)
    {
        yield return new WaitForSeconds(delay); // wait for specified delay
        JumpyPlatform.SetActive(true); // enable object
    }
}
