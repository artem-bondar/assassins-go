using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private Board board;
    private PlayerManager player;

    private bool hasLevelStarted = false;
    private bool hasLevelFinished = false;

    private bool isGamePlaying = false;
    private bool isGameOver = false;

    public bool HasLevelStarted
    {
        get => hasLevelStarted;
        set => hasLevelStarted = value;
    }

    public bool HasLevelFinished
    {
        get => hasLevelFinished;
        set => hasLevelFinished = value;
    }

    public bool IsGamePlaying
    {
        get => isGamePlaying;
        set => isGamePlaying = value;
    }

    public bool IsGameOver
    {
        get => isGameOver;
        set => isGameOver = value;
    }

    public float delay = 1f;

    private void Awake()
    {
        board = Object.FindObjectOfType<Board>().GetComponent<Board>();
        player = Object.FindObjectOfType<PlayerManager>().GetComponent<PlayerManager>();
    }

    private void Start()
    {
        if (player != null && board != null)
        {
            StartCoroutine("RunGameLoop");
        }
        else
        {
            Debug.LogWarning("Gamemanager Error: no player or board found!");
        }
    }

    private IEnumerator RunGameLoop()
    {
        yield return StartCoroutine("StartLevelRoutine");
        yield return StartCoroutine("PlayLevelRoutine");
        yield return StartCoroutine("EndLevelRoutine");
    }

    private IEnumerator StartLevelRoutine()
    {
        Debug.Log("Start level");

        player.playerInput.InputEnabled = false;

        while (!hasLevelStarted)
        {
            yield return null;
        }
    }

    private IEnumerator PlayLevelRoutine()
    {
        Debug.Log("Play level");

        isGamePlaying = true;

        yield return new WaitForSeconds(delay);

        player.playerInput.InputEnabled = true;

        while (!isGameOver)
        {
            yield return null;
        }
    }

    private IEnumerator EndLevelRoutine()
    {
        Debug.Log("End level");

        player.playerInput.InputEnabled = false;

        while (!hasLevelFinished)
        {
            yield return null;
        }

        RestartLevel();
    }

    private void RestartLevel()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    public void PlayLevel() => hasLevelStarted = true;
}
