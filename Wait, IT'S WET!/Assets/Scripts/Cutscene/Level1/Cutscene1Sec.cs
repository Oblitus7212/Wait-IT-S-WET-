using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutscene1Sec : MonoBehaviour
{
    [SerializeField] protected GameObject Player;
    [SerializeField] protected GameObject Cutscene;
    [SerializeField] protected GameObject AntBoss;
    public Transform teleportLocation;

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
        yield return new WaitForSeconds(6);
        Cutscene.SetActive(false);
        Player.SetActive(true);
        _playerController.disabled = true;
        yield return new WaitForSeconds(0.1f);
        Player.transform.position = teleportLocation.position;
        yield return new WaitForSeconds(0.1f);
        _playerController.disabled = false;
        AntBoss.SetActive(true);
    }
}
