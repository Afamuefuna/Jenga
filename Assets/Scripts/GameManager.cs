using UnityEngine;
using System.Collections;
using System;

public delegate void GameStateChangedEventHandler(object sender, GameStateChangedEventArgs e);

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public event GameStateChangedEventHandler StateChanged;
    public GameState State
    {
        get { return _state; }
        set
        {
            if (_state == value)
                return;
            PreviousState = _state;
            _state = value;

            if (StateChanged != null)
                StateChanged(this, new GameStateChangedEventArgs(_state, PreviousState));
        }
    }
    public GameState PreviousState { get; protected set; }

    private GameState _state;

    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start() {
        StateChanged += OnStateChanged;

        State = GameState.MENU;
    }

    void OnStateChanged(object sender, GameStateChangedEventArgs e)
    {
        switch (State)
        {
            case GameState.PAUSEMENU:
                Time.timeScale = 0;
                break;
            case GameState.MENU:

                break;
            case GameState.PLAYING:

                break;
        }

        if (State == GameState.PLAYING || State == GameState.MENU)
            Time.timeScale = 1;
    }
}

public class GameStateChangedEventArgs
{
    public GameState CurrentState { get; private set; }
    public GameState PreviousState { get; private set; }

    public GameStateChangedEventArgs(GameState newState, GameState previousState)
    {
        CurrentState = newState;
        PreviousState = previousState;
    }
}

public enum GameState
{
    INTRO,
    MENU,
    EXIT,
    PLAYING,
    PAUSEMENU,
    OVER
}