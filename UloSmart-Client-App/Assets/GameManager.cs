using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using PlayerSystem;
using Cinemachine;

public class GameManager : MonoBehaviour
{
    private void Start()
    {
        player.DisableMovement();
        titleScreen.SetActive(true);
        titleScreenCamera.Priority = 1000;

        StartCoroutine(startSimulation());
    }

    IEnumerator startSimulation()
    {
        yield return new WaitForSeconds(0.5f);

        titleScreen.SetActive(false);
        player.EnableMovement();
        titleScreenCamera.Priority = -1;
    }




    [SerializeField]
    private GameObject titleScreen = null;

    [SerializeField]
    private CinemachineVirtualCamera titleScreenCamera = null;

    [SerializeField]
    private Player player = null;
}
