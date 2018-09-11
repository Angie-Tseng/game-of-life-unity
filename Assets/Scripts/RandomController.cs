using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class RandomController :
    MonoBehaviour,
    IPointerExitHandler,
    IPointerEnterHandler
{

    public GameObject mapGameObject;
    public Text showNumber;
    private Slider slider;
    private int change;

    // Use this for initialization
    void Start()
    {
        slider = this.GetComponent<Slider>();
        change = Mathf.FloorToInt(slider.minValue);
        slider.interactable = false;
        slider.gameObject.SetActive(false);
        showNumber.gameObject.SetActive(false);
        showNumber.transform.localPosition = new Vector3(
                -25,
                -175 + 250 * change / 100,
                0
            );
    }

    public void Lock()
    {
        slider.interactable = false;
        slider.gameObject.SetActive(false);
    }

    public void Unlock()
    {
        slider.interactable = true;
        slider.gameObject.SetActive(true);
    }

    public void ChangeValue()
    {
        change = Mathf.FloorToInt(slider.value);
        mapGameObject.GetComponent<Map>().Init(change);
        showNumber.text = change.ToString() + "%";
        showNumber.transform.localPosition = new Vector3(
                -25,
                -175 + 250 * change / 100,
                0
            );
        showNumber.gameObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        showNumber.gameObject.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        showNumber.gameObject.SetActive(true);
    }
}
