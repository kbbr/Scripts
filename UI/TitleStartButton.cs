using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleStartButton : MonoBehaviour {
    public void TitleStartButtonOn() {
        SceneManager.LoadScene("MainScene");
    }
}
