using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using SingleMagicMoba;

public class GameManager : myManager<GameManager>
{
    public enum GameState
    {
        PREGAME,
        RUNNING,
        PAUSED
    }

    public GameObject[] systemPrefabs;
    public Events.EventGameState OnGameStateChanged;

    private List<GameObject> currentInstantiatedSystemPrefabs; 
    private List<AsyncOperation> loadOperations;
    private GameState _currentGameState = GameState.PREGAME;


    private string currentLevelName = string.Empty;

    public override void Awake()
    {
        base.Awake();
    }

    public GameState CurrentGameState
    {
        get { return _currentGameState; }
        set { _currentGameState = value; }
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);

        currentInstantiatedSystemPrefabs = new List<GameObject>();
        loadOperations = new List<AsyncOperation>();

        InstantiateSystemPrefabs();

        UIManager.Instance.OnMainMenuFadeComplete.AddListener(HandleMainMenuFadeComplete);

    }
    private void Update()
    {
        if (_currentGameState == GameState.PREGAME)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause_Game();
        }

    }

    private void  HandleMainMenuFadeComplete(bool fadeOut)
    {
        if (!fadeOut)
        {
            UnloadLevel(currentLevelName);
        }
        
    }


private void UpdateState(GameState state)
    {
        GameState previousGameState = _currentGameState;

        _currentGameState = state;

        switch (_currentGameState)
        {
            case GameState.PREGAME:
                Time.timeScale = 1.0f;
                break;
            case GameState.RUNNING:
                Time.timeScale = 1.0f;
                break;
            case GameState.PAUSED:
                Time.timeScale = 0.0f;
                break;
            default:
                break;


        }

        OnGameStateChanged.Invoke(_currentGameState, previousGameState);
    }

    private void InstantiateSystemPrefabs()
    {
        GameObject prefabInstance;
        for (int i = 0; i < systemPrefabs.Length; ++i)
        {
            prefabInstance = Instantiate(systemPrefabs[i]);
            currentInstantiatedSystemPrefabs.Add(prefabInstance);
        }
    }

    private void LoadLevel(string levelName)
    {
        AsyncOperation ao = SceneManager.LoadSceneAsync(levelName,LoadSceneMode.Additive);
        if (ao == null)
        {
            Debug.LogWarning("[GAMAMANAGER] Unlable to LOAD level " + levelName);
            return;
        }

        ao.completed += OnLoadLevelComplete;
        loadOperations.Add(ao);
        currentLevelName = levelName;
    }

    private void OnLoadLevelComplete(AsyncOperation AO)
    {
        if (loadOperations.Contains(AO))
        {
            loadOperations.Remove(AO);

            if (loadOperations.Count == 0)
            {
                UpdateState(GameState.RUNNING);
            }

            
        }

        Debug.Log("Load Complete");
    }

    private void UnloadLevel(string levelName)
    {
        AsyncOperation ao = SceneManager.UnloadSceneAsync(levelName);
        if (ao == null)
        {
            Debug.LogWarning("[GAMAMANAGER] Unlable to UNLoadlevel " + levelName);
            return;
        }

        ao.completed += OnUnLoadOperationComplete;
    }

    private void OnUnLoadOperationComplete(AsyncOperation AO)
    {
        Debug.Log("UnLoad Complete");
    }

    protected void OnDestroy()
    {
        if (currentInstantiatedSystemPrefabs == null)
        {
            return;
        }

        for (int i = 0; i < currentInstantiatedSystemPrefabs.Count; ++i)
        {
            Destroy(currentInstantiatedSystemPrefabs[i]);
        }
        currentInstantiatedSystemPrefabs.Clear();
    }

    public void Start_Game()
    {
        LoadLevel("Main");
    }

    public void Pause_Game()
    {
        UpdateState(_currentGameState == GameState.RUNNING ? GameState.PAUSED : GameState.RUNNING);

    }

    public void Restart_Game()
    {
        UpdateState(GameState.PREGAME);
    }

    public void Quit_Game()
    {
        Application.Quit();
    }
}
