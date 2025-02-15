using UnityEngine;
using TMPro;

public class TaskOnKill : MonoBehaviour
{
    public static TaskOnKill Instance;

    [SerializeField]
    private TMP_Text textScoreForLift;

    [SerializeField]
    private int scoreForLift;

    private int currentScore = 0;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("More than one TaskOnKill");
            return;
        }

        Instance = this;

        textScoreForLift.text = $"{currentScore + "/" + scoreForLift}";
    }

    public void Increase()
    {
        currentScore++;

        textScoreForLift.text = $"{currentScore + "/" + scoreForLift}";
        
        if (currentScore >= scoreForLift)
            GameManager.Instance.StartLift();
    }
}