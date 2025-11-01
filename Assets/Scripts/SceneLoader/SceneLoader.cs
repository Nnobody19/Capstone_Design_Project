using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    // PLAY 버튼 클릭 시 MainScene으로 이동
    public void LoadMainScene()
    {
        SceneManager.LoadScene("MainScene");
    }

    // (선택) 게임 종료용 함수 — Exit 버튼에도 쓸 수 있음
    public void QuitGame()
    {
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
