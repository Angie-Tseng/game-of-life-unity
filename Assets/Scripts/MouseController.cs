using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MouseController : MonoBehaviour, ISelectHandler{
    private GameObject cell;

	// Use this for initialization
	void Start () {

        Button a = this.GetComponent<Button>();
        a.onClick.AddListener(ActiveTask);
        cell = this.transform.GetChild(0).gameObject;
    }

    void Update()
    {
        if (EventSystem.current.currentSelectedGameObject != null)
        {
            EventSystem.current.SetSelectedGameObject(null);
        }
    }

    public void ActiveTask()
    {
        cell.SetActive(!cell.activeSelf);
    }

    public void OnSelect(BaseEventData eventData)
    {
    }
}
