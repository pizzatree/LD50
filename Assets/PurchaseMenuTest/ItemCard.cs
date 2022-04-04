using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCard : MonoBehaviour
{
    [SerializeField] private GameObject _displayItem;
    private GameObject _previewObject;

    void Start() {
        _previewObject = GameObject.Find("PreviewObject");
    }

    public void ChangeDisplay() {
        foreach(Transform child in _previewObject.transform) {
            child.gameObject.SetActive(false);
        }

        _displayItem.SetActive(true);
    }
}
