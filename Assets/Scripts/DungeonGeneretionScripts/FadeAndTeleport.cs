using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeAndTeleport : MonoBehaviour
{
    private GameObject _player;
    private WaitForSeconds _wait;

    private const string ImageName = "FadeImage";
    private Image _fadeImage;
    [SerializeField] private Color fadeColor;

    private void Start()
    {
        _fadeImage = GameObject.Find(ImageName).GetComponent<Image>();
        _fadeImage.color = fadeColor;
        _fadeImage.CrossFadeAlpha(0f, 1.5f, false);
        _wait = new WaitForSeconds(0.3f);

        _player = GameObject.FindGameObjectWithTag("Player");
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
        _player.transform.position = targetGo.position;
        StartCoroutine(FadeCoroutine());
    }

    private IEnumerator FadeCoroutine()
    {
        _fadeImage.CrossFadeAlpha(1f, 0f, true);
        yield return _wait;
        _fadeImage.CrossFadeAlpha(0f, 1.5f, true);
        yield break;
    }
}