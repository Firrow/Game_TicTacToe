using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // On gère le son localement afin d'éviter d'utiliser de la bande passante pour rien
    [SerializeField] private AudioClip placeSFXPrefab;
    [SerializeField] private AudioClip winSFXPrefab;
    [SerializeField] private AudioClip loseSFXPrefab;



    private void Start()
    {
        GameManager.Instance.OnPlacedObject += GameManager_OnPlacedObject;
        GameManager.Instance.OnGameWin += GameManager_OnGameWin;
    }

    private void GameManager_OnPlacedObject(object sender, System.EventArgs e)
    {
        AudioSource.PlayClipAtPoint(placeSFXPrefab, new Vector3(0, 0, 0));
    }

    private void GameManager_OnGameWin(object sender, GameManager.OnGameWinEventArgs e)
    {
        if (GameManager.Instance.GetLocalPlayerType() == e.winPlayerType)
        {
            AudioSource.PlayClipAtPoint(winSFXPrefab, new Vector3(0, 0, 0));
        }
        else
        {
            AudioSource.PlayClipAtPoint(loseSFXPrefab, new Vector3(0, 0, 0));
        }
    }
}
