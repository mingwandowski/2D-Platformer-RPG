using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HeaderButtonUI : MonoBehaviour, ISelectHandler
{
    public void OnSelect(BaseEventData eventData)
    {
        GameObject button = eventData.selectedObject;
        UIManager.instance.mainMenu.SelectButton(button);
    }
}
