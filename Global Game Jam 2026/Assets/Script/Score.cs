using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.VFX;

public class Score : MonoBehaviour
{
    private TextMeshProUGUI text;
    private int score = 0;
    [SerializeField] private int scoreReward = 1;
    [SerializeField] private Transform claps;
    [SerializeField] private Transform comboVFX;

    public UnityEvent onCombo = new UnityEvent();

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    public void OnSuccess()
    {
        score += scoreReward;
        EvaluateScore();
    }

    public void OnFailure()
    {
        score = 0;
        EvaluateScore();
    }

    private void EvaluateScore()
    {
        if (score == 0)
        {
            foreach (Transform child in claps)
            {
                child.gameObject.SetActive(false);
            }
        }
        else if (score < claps.childCount)
        {
            claps.GetChild(score - 1).gameObject.SetActive(true);
        }

        switch (score)
        {
            case 10:
                PlayCombo(0);
                break;
            case 20:
                PlayCombo(1);
                break;
            case 30:
                PlayCombo(2);
                break;
            case 40:
                PlayCombo(3);
                break;
            case 50:
                PlayCombo(4);
                break;
        }

        text.text = score.ToString();
    }

    private void PlayCombo(int i)
    {
        comboVFX.transform.GetChild(i).GetComponent<VisualEffect>().Play();
        onCombo.Invoke();
    }
}
