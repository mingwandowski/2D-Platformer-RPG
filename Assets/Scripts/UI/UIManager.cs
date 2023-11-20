using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public MainMenuUI mainMenu;

    public bool menuActive = false;

    private void Awake() {
        if (instance != null) {
            Destroy(instance.gameObject);
        } else {
            instance = this;
        }
    }

    private void Start() {
        SetMenuActive(false);
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            SetMenuActive(!menuActive);
        }
    }

    private void SetMenuActive(bool active) {
        if (active) {
            Time.timeScale = 0f;
        } else {
            Time.timeScale = 1f;
        }
        menuActive = active;
        mainMenu.gameObject.SetActive(menuActive);
    }
}
