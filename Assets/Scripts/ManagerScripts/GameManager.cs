using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private GameObject _menuUI;
    [SerializeField] private GameObject _inventoryUI;
    [SerializeField] private ToolTipManager _toolTipManager;

    private bool isPause = false;

    public static bool IsPlayerStop = false;                // 플레이어 행동 제어

    // Start is called before the first frame update
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_inventoryUI.activeSelf) ToggleInventory();

            else
            {
                if (isPause) CloseMenu();

                else OpenMenu();
            }
        }

        if (!isPause && (Input.GetKeyDown(KeyCode.Tab) || Input.GetKeyDown(KeyCode.I))) 
        {
            ToggleInventory();
        }
    }

    public void OpenMenu()
    {
        _menuUI.SetActive(true);
        Time.timeScale = 0f;        // 정지
        isPause = true;
        Cursor.lockState = CursorLockMode.None;     // 정지해도 마우스 커서 고정 해제
        Cursor.visible = true;                      // 조작은 해야 하니 마우스 커서는 보이게
    }

    public void CloseMenu()
    {
        _menuUI.SetActive(false);
        Time.timeScale = 1f;        // 정지 해제
        isPause = false;
        Cursor.lockState = CursorLockMode.Locked;   // 게임 시작 시, 마우스 커서 숨기기
        Cursor.visible= false;
    }

    public void ToggleInventory()
    {
        bool isActive = !_inventoryUI.activeSelf;
        _inventoryUI.SetActive(isActive);
        IsPlayerStop = isActive;

        if (isActive)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        else
        {
            if (_toolTipManager != null) _toolTipManager.HideToolTip();

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
