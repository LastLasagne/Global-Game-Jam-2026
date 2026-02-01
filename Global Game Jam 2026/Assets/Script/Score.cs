using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.VFX;

public class Score : MonoBehaviour
{
    private TextMeshProUGUI text;
    private int combo = 0;
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
        combo += scoreReward;
        score += scoreReward;
        EvaluateScore();
    }

    public void OnFailure()
    {
        combo = 0;
        EvaluateScore();
    }

    private void EvaluateScore()
    {
        if (combo == 0)
        {
            foreach (Transform child in claps)
            {
                child.gameObject.SetActive(false);
            }
        }
        else if (combo < claps.childCount)
        {
            claps.GetChild(combo - 1).gameObject.SetActive(true);
        }

        switch (combo)
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

        text.text = combo.ToString();
    }

    private void PlayCombo(int i)
    {
        comboVFX.transform.GetChild(i).GetComponent<VisualEffect>().Play();
        onCombo.Invoke();
    }

    public void SendScore()
    {
        MenuScore.Instance.score = score;
        SceneManager.LoadScene("Menu");
    }
}
