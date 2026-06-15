using AudioScripts;
using UnityEngine;

public class JingleManager : MonoBehaviour
{
    [Header("Victory Jingle")] [SerializeField]
    private string winJingleEventId = "Win_Jingle";

    [Header("Defeat jingle")] [SerializeField]
    private string loseJingleEventId = "Lose_Jingle";

    public void PlayWinJingle() => AudioSystem.Play(winJingleEventId);
    public void PlayLoseJingle() => AudioSystem.Play(loseJingleEventId);
}
