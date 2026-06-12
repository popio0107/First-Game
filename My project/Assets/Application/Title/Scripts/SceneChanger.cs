using UnityEngine;
using UnityEngine.SceneManagement; // シーン切り替えに必要

public class SceneChanger : MonoBehaviour
{
    // ボタンが押されたときに呼び出すメソッド
    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}