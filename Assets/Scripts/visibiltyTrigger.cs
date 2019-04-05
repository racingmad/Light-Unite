using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class visibiltyTrigger : MonoBehaviour
{
    private Button toggleButton;

    public bool isVisible;

    private void Start()
    {
        toggleButton = GetComponent<Button>();
        toggleButton.onClick.AddListener(LevelKeyOnClick);
    }

    public void LevelKeyOnClick()
    {
        isVisible = !isVisible;
    }
}
