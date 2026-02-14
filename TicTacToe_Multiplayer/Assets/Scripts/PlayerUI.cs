using TMPro;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private GameObject crossArrowGameObject;
    [SerializeField] private GameObject circleArrowGameObject;
    [SerializeField] private GameObject crossTextGameObject;
    [SerializeField] private GameObject circleTextGameObject;
    [SerializeField] private TextMeshProUGUI playerCircleScoreTextMesh;
    [SerializeField] private TextMeshProUGUI playerCrossScoreTextMesh;



    private void Awake()
    {
        crossArrowGameObject.SetActive(false);
        circleArrowGameObject.SetActive(false);
        crossTextGameObject.SetActive(false);
        circleTextGameObject.SetActive(false);

        playerCircleScoreTextMesh.text = "";
        playerCrossScoreTextMesh.text = "";
    }

    private void Start()
    {
        GameManager.Instance.OnGameStarted += GameManager_OnGameStarted;
        GameManager.Instance.OnCurrentPlayablePlayerTypeChanged += GameManager_OnCurrentPlayablePlayerTypeChanged;
        GameManager.Instance.OnScoreChanged += GameManager_OnScoreChanged;
    }

    private void GameManager_OnGameStarted(object sender, System.EventArgs e)
    {
        if (GameManager.Instance.GetLocalPlayerType() == GameManager.PlayerType.Circle)
        {
            circleTextGameObject.SetActive(true);
        }
        else
        {
            crossTextGameObject.SetActive(true);
        }

        playerCircleScoreTextMesh.text = "0";
        playerCrossScoreTextMesh.text = "0";

        UpdateCurrentArrow();
    }

    private void GameManager_OnCurrentPlayablePlayerTypeChanged(object sender, System.EventArgs e)
    {
        UpdateCurrentArrow();
    }

    private void GameManager_OnScoreChanged(object sender, System.EventArgs e)
    {
        GameManager.Instance.GetScores(out int playerCircleScore, out int playerCrossScore);

        playerCircleScoreTextMesh.text = playerCircleScore.ToString();
        playerCrossScoreTextMesh.text = playerCrossScore.ToString();
    }

    private void UpdateCurrentArrow()
    {
        if (GameManager.Instance.GetCurrentPlayablePlayerType() == GameManager.PlayerType.Circle)
        {
            crossArrowGameObject.SetActive(false);
            circleArrowGameObject.SetActive(true);
        }
        else
        {
            crossArrowGameObject.SetActive(true);
            circleArrowGameObject.SetActive(false);
        }
    }
}
