using System;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    [SerializeField] Shooting shooting;
    private TextMeshProUGUI text;
    private int score = 0;
    [SerializeField] private int scoreReward = 1;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
        shooting.onFailure.AddListener(OnFailure);
        shooting.onSuccess.AddListener(OnSuccess);
    }

    private void OnSuccess()
    {
        score += scoreReward;
        text.text = score.ToString();
    }

    private void OnFailure()
    {
        score = 0;
        text.text = score.ToString();
    }
}
