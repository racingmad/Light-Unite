using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class changeScene : MonoBehaviour
{
    private Button startButton;

    public string selectScene;

    private void Start()
    {
        startButton = GetComponent<Button>();
        startButton.onClick.AddListener(ChangeSceneOnClick);
    }

    public void ChangeSceneOnClick()
    {
        SceneManager.LoadScene(selectScene);
    }
}
