using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using EnumTypes;

public class GameManager : SingletonDontDestroy<GameManager>
{
    public Class CurPlayerClass { get; set; }
    [SerializeField]
    private bool isPlaying = false;
    private bool isPause;
    [SerializeField]
    private bool isGameOver;
    //TODO ���� ����
    private bool isGameClear;
    public bool IsPlaying
    {
        get => isPlaying;
    }
    public bool IsPause
    {
        get => isPause;
    }
    public bool IsGameOver
    {
        get => isGameOver;
        set => isGameOver = value;
    }

    private void Update()
    {
        if (isPlaying)
        {  //Scene1���� �˻��� ����

            if (isGameOver)
            {
                Debug.Log("��������");
                Invoke("GameOver", 3f);
                isPlaying = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
           UIManager.Instance.OnWindowWithPause(Window.OPTION);
        }
    }
    public void Pause()
    {
        if (!isPause)
        {
            isPause = true;
            Time.timeScale = 0f;
        }
    }
    public void Resume()
    {
        if (isPause)
        {
            Time.timeScale = 1f;
            isPause = false;
        }
    }
    public void GameOver()
    {
        UIManager.Instance.OnWindowWithPause(Window.GAMEOVER);
    }
    public void LoadMenuScene()
    {
        CardManager.Instance.ClearPlayerCardList();
        SceneManager.LoadScene(0);
        isGameOver = false;
    }
    public void LoadPlayScene() // ���� �޴� -> ���� ��ư
    {
        SceneManager.LoadScene(1);
        isPlaying = true;
        Resume();
        UIManager.Instance.OnWindow(Window.PLAYERSTATE);
    }
}
