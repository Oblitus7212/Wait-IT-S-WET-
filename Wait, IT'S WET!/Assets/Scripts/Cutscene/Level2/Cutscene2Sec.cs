using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cutscene2Sec : MonoBehaviour
{
    public GameObject Player;
    public GameObject Cutscene;
    public Transform teleportLocation;
    public GameObject Butterfly;
    private PlayerController _playerController;

    private void Start()
    {
        Player.TryGetComponent<PlayerController>(out _playerController);
    }

    void OnTriggerEnter(Collider other)
    {
        this.gameObject.GetComponent<BoxCollider>().enabled = false;
        Player.SetActive(false);
        Cutscene.SetActive(true);
        StartCoroutine(Finish());
    }

    public virtual IEnumerator Finish()
    {
        yield return new WaitForSeconds(3.5f);
        yield return new WaitForSeconds(0.1f);
        Player.transform.position = teleportLocation.position;
        yield return new WaitForSeconds(0.1f);
        _playerController.disabled = false;
        Butterfly.SetActive(true);
        Cutscene.SetActive(false);
        Player.SetActive(true);
    }
}
