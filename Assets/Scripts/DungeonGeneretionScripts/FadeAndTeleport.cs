using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeAndTeleport : MonoBehaviour
{
    private GameObject player;
    private WaitForSeconds wait;

    private const string imageName = "BlackImage";
    private Image blackImage;

    void Start()
    {
        blackImage = GameObject.Find(imageName).GetComponent<Image>();
        blackImage.color = Color.blue; //sparafleshato
        blackImage.CrossFadeAlpha(0f, 1.5f, false);
        wait = new WaitForSeconds(0.3f);

        player = GameObject.FindGameObjectWithTag("Player");

    }

    private void OnEnable()
    {
        Door.OnTeleport += TeleportPlayer;
    }

    private void OnDisable()
    {
        Door.OnTeleport -= TeleportPlayer;
    }

    private void TeleportPlayer(Transform targetGo)
    {
        player.transform.position = targetGo.position;
        StartCoroutine(FadeCoroutine());
    }

    private IEnumerator FadeCoroutine()
    {
        blackImage.CrossFadeAlpha(0.9f, 0f, true);
        yield return wait;
        blackImage.CrossFadeAlpha(0f, 1.5f, true);
        yield break;
    }
}
