using System;
using TMPro;
using UnityEngine;
using UnityEngine.VFX;

public class Score : MonoBehaviour
{
    [SerializeField] Shooting shooting;
    private TextMeshProUGUI text;
    private int score = 0;
    [SerializeField] private int scoreReward = 1;
    [SerializeField] private Transform claps;
    [SerializeField] private Transform comboVFX;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
        shooting.onFailure.AddListener(OnFailure);
        shooting.onSuccess.AddListener(OnSuccess);
    }

    private void OnSuccess()
    {
        score += scoreReward;
        EvaluateScore();
    }

    private void OnFailure()
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

        switch(score)
        {
            case 10:
                comboVFX.transform.GetChild(0).GetComponent<VisualEffect>().Play();
                break;
            case 20:
                comboVFX.transform.GetChild(1).GetComponent<VisualEffect>().Play();
                break;
            case 30:
                comboVFX.transform.GetChild(2).GetComponent<VisualEffect>().Play();
                break;
            case 40:
                comboVFX.transform.GetChild(3).GetComponent<VisualEffect>().Play();
                break;
            case 50:
                comboVFX.transform.GetChild(4).GetComponent<VisualEffect>().Play();
                break;
        }

        text.text = score.ToString();
    }
}
