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
    private bool _isPause = false;
    private bool _isInventoryOpen = false;

    private Coroutine _soundCoroutine;
    private AudioSource _audioSource;
    public AudioClip InventorySoundClip;
    public float InventorySoundPitch = 1.2f;

    public float OpenSoundStartTime = 0f;
    public float OpenSoundEndTime = 0.9f;
    public float CloseSoundStartTime = 1.4f;
    public float CloseSoundEndTime = 2.2f;

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

        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_isInventoryOpen) ToggleInventory();

            else
            {
                if (_isPause) CloseMenu();

                else OpenMenu();
            }
        }

        if (!_isPause && (Input.GetKeyDown(KeyCode.Tab) || Input.GetKeyDown(KeyCode.I))) 
        {
            ToggleInventory();
        }
    }

    public void OpenMenu()
    {
        _menuUI.SetActive(true);
        Time.timeScale = 0f;        // 정지
        _isPause = true;
        Cursor.lockState = CursorLockMode.None;     // 정지해도 마우스 커서 고정 해제
        Cursor.visible = true;                      // 조작은 해야 하니 마우스 커서는 보이게
    }

    public void CloseMenu()
    {
        _menuUI.SetActive(false);
        Time.timeScale = 1f;        // 정지 해제
        _isPause = false;
        Cursor.lockState = CursorLockMode.Locked;   // 게임 시작 시, 마우스 커서 숨기기
        Cursor.visible= false;
    }

    public void ToggleInventory()
    {
        if (_soundCoroutine != null) StopCoroutine(_soundCoroutine);
        _audioSource.Stop();

        _isInventoryOpen = !_isInventoryOpen;

        _inventoryUI.SetActive(_isInventoryOpen);
        IsPlayerStop = _isInventoryOpen;
        _audioSource.pitch = InventorySoundPitch;

        if (_isInventoryOpen)
        {
            _soundCoroutine = StartCoroutine(PlaySoundSegment(InventorySoundClip, OpenSoundStartTime, OpenSoundEndTime));
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        else
        {
            _soundCoroutine = StartCoroutine(PlaySoundSegment(InventorySoundClip, CloseSoundStartTime, CloseSoundEndTime));

            if (_toolTipManager != null) _toolTipManager.HideToolTip();

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
    
    private IEnumerator PlaySoundSegment(AudioClip clip, float startTime, float endTime)
    {
        _audioSource.clip = clip;
        _audioSource.time = startTime;
        _audioSource.Play();

        yield return new WaitForSeconds(endTime - startTime);

        _audioSource.Stop();
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
