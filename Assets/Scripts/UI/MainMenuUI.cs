using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuUI : MonoBehaviour
{
    // public void SwitchTo(GameObject menu) {
    //     Transform body = transform.Find("Body");
    //     for (int i = 0; i < body.childCount; i++) {
    //         body.GetChild(i).gameObject.SetActive(false);
    //     }

    //     if (menu != null) {
    //         menu.SetActive(true);
    //     }
    // }

    public void SelectButton(GameObject menu) {
        Transform body = transform.Find("Body");
        for (int i = 0; i < body.childCount; i++) {
            GameObject bodyObject = body.GetChild(i).gameObject;
            bodyObject.SetActive(bodyObject.name == menu.name);
        }
    }
}
