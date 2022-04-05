using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemCard : MonoBehaviour
{
    [SerializeField] private GameObject _displayItem;
    [SerializeField] private GameObject _objectToBuy;
    [SerializeField] int _price = 1;
    [SerializeField] Transform _spawnPoint;
    [SerializeField] TMP_Text _costText;
    private GameObject _previewObject;

    void Start() {
        _previewObject = GameObject.Find("PreviewObject");
        _costText.text = "$" + _price;
    }

    public void PurchaseItem()
    {
        if (GameManager.Instance.TaxDollars < _price) { return;}
        GameManager.Instance.TaxDollars = GameManager.Instance.TaxDollars - _price;
        Instantiate(_objectToBuy, _spawnPoint.position, _spawnPoint.rotation);
    }

    public void ChangeDisplay() {
        foreach(Transform child in _previewObject.transform) {
            child.gameObject.SetActive(false);
        }

        _displayItem.SetActive(true);
    }
}
