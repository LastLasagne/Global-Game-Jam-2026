using UnityEngine;
using UnityEngine.UI;

public class MenuScoreDisplay : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI scoreText;
    [SerializeField] private Image scorePanel;

    int maxScore = 50;

    private void OnEnable()
    {
        int score = MenuScore.Instance.score;
        if (score == 0)
            return;
        else
        {
            scorePanel.enabled = true;
            scoreText.text =  score.ToString() + "/50";
        }
    }
}
