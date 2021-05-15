using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class VictoryScreen : MonoBehaviour
{
    [SerializeField]
    private CanvasGroup background;
    [SerializeField]
    private GameObject victoryText;
    [SerializeField]
    private GameObject trophy;

    [SerializeField]
    private ParticleSystem fireworkParticle;

    private readonly float backgroundOpeningTime = 0.4f;
    private readonly float backgroundClosingTime = 0.7f;
    private readonly float trophyTime = 0.5f;
    private readonly float victoryTextTime = 1f;
    private readonly float delay = 0.1f;

    private readonly float victoryScreenClosingTime = 3f;

    public static event Action VictoryEnd;

    private void OnEnable()
    {
        AudioManager.Instance.PlayVictorySound();

        background.alpha = 0;
        background.LeanAlpha(0.99f, backgroundOpeningTime);

        trophy.transform.localPosition = new Vector2(0, -Screen.height);
        trophy.LeanMoveLocalY(0, trophyTime).setEaseOutExpo().setOnComplete(OnOpeningComplete);

        victoryText.transform.localScale = Vector2.zero;
        victoryText.LeanScale(Vector2.one, victoryTextTime).setEaseOutBounce().delay = delay;

        Invoke(nameof(CloseScreen), victoryScreenClosingTime);
    }

    private void CloseScreen()
    {
        background.LeanAlpha(0, backgroundClosingTime).setOnComplete(OnClosingComplete);
        trophy.LeanMoveLocalY(-Screen.height, trophyTime).setEaseInExpo();
        victoryText.LeanScale(Vector2.zero, victoryTextTime).setEaseInBounce();
    }

    private void OnOpeningComplete()
    {
        fireworkParticle.Play();
    }

    private void OnClosingComplete()
    {
        gameObject.SetActive(false);
        VictoryEnd?.Invoke();
    }
}
