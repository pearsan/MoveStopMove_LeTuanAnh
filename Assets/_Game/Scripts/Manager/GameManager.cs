using System.Collections;
using System.Collections.Generic;
using _Game.Scripts.Gameplay.Character;
using UnityEngine;

public enum GameState { MainMenu, GamePlay, Finish, Revive, Setting }

public class GameManager : Singleton<GameManager>
{
    // [SerializeField] CameraFollower cameraFollower;

    private static GameState gameState;

    public Character Winner { get; set; }

    public static void ChangeState(GameState state)
    {
        gameState = state;
    }

    public static bool IsState(GameState state) => gameState == state;

    private void Awake()
    {
        Input.multiTouchEnabled = false;
        Application.targetFrameRate = 60;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        int maxScreenHeight = 1920;
        float ratio = (float)Screen.currentResolution.width / (float)Screen.currentResolution.height;
        if (Screen.currentResolution.height > maxScreenHeight)
        {
            Screen.SetResolution(Mathf.RoundToInt(ratio * (float)maxScreenHeight), maxScreenHeight, true);
        }
    }

    void Start()
    {
        // UIManager.Instance.OpenUI<CanvasMainMenu>();
        // cameraFollower.OnInit();
        OnInit();
    }

    public void OnInit()
    {
        ChangeState(GameState.GamePlay);
        // Winner = null;
        // cameraFollower.OnInit();
    }
}

