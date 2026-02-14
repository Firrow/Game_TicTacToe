using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // On gère le son localement afin d'éviter d'utiliser de la bande passante pour rien
    [SerializeField] private Transform placeSFXPrefab;
    [SerializeField] private Transform winSFXPrefab;
    [SerializeField] private Transform loseSFXPrefab;



    private void Start()
    {
        GameManager.Instance.OnPlacedObject += GameManager_OnPlacedObject;
        GameManager.Instance.OnGameWin += GameManager_OnGameWin;
    }

    private void GameManager_OnPlacedObject(object sender, System.EventArgs e)
    {
        Transform SFXTransform = Instantiate(placeSFXPrefab);
        Destroy(SFXTransform.gameObject, 5.0f); //Destroy sound after 5 secondes
    }

    private void GameManager_OnGameWin(object sender, GameManager.OnGameWinEventArgs e)
    {
        if (GameManager.Instance.GetLocalPlayerType() == e.winPlayerType)
        {
            Transform SFXTransform = Instantiate(winSFXPrefab);
            Destroy(winSFXPrefab.gameObject, 5.0f); //Destroy sound after 5 secondes
        }
        else
        {
            Transform SFXTransform = Instantiate(loseSFXPrefab);
            Destroy(loseSFXPrefab.gameObject, 5.0f); //Destroy sound after 5 secondes
        }
    }
}
