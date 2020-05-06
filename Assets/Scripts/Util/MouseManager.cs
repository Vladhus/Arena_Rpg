using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using SingleMagicMoba;

public class MouseManager : MonoBehaviour
{
    private bool useDefaultCursor = false;

    public LayerMask clickableLayer;

    public Texture2D pointer;
    public Texture2D target;
     public Texture2D doorWay;

    public Events.OnClickEnvironmentVector3 onClickEnvironmentVector;

    private void Start()
    {
        HandleGameStateChanged(GameManager.GameState.PREGAME, GameManager.GameState.PREGAME);
        GameManager.Instance.OnGameStateChanged.AddListener(HandleGameStateChanged);
    }

    private void HandleGameStateChanged(GameManager.GameState currentGameState, GameManager.GameState previousGameState)
    {
        useDefaultCursor = currentGameState == GameManager.GameState.PAUSED || GameManager.Instance.CurrentGameState == GameManager.GameState.PREGAME;
    }

    private void Update()
    {

        if (useDefaultCursor )
        {
            Cursor.SetCursor(pointer, new Vector2(16, 16), CursorMode.Auto);
            return;
        }
        else
        {
            Cursor.visible = false;
        }
        //RaycastHit hit;
        //if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 50, clickableLayer.value)) 
        //{
        //    //bool door = false;

        //    //if (hit.collider.gameObject.tag == "DoorWay")
        //    //{
        //    //    Cursor.SetCursor(doorWay, new Vector2(16, 16), CursorMode.Auto);
        //    //    door = true;
        //    //}
        //    //else
        //    //{
        //    //    Cursor.SetCursor(target, new Vector2(16, 16), CursorMode.Auto);
        //    //}
        //}
    }
}
