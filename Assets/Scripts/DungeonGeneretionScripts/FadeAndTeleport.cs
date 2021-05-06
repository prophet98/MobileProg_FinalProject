using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeAndTeleport : MonoBehaviour
{
    private GameObject player;
    private WaitForSeconds wait;

    private const string imageName = "FadeImage";
    private Image fadeImage;
    [SerializeField]
    private Color fadeColor;

    void Start()
    {
        fadeImage = GameObject.Find(imageName).GetComponent<Image>();
        fadeImage.color = fadeColor; 
        fadeImage.CrossFadeAlpha(0f, 1.5f, false);
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
        fadeImage.CrossFadeAlpha(1f, 0f, true);
        yield return wait;
        fadeImage.CrossFadeAlpha(0f, 1.5f, true);
        yield break;
    }
}
