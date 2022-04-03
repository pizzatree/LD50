using System;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] TMP_Text _taxDollarText;

    int _visitorsSinceLastPath = 0;
    float _visitorsTillNextPath = 1;
    int _taxDollars = 0;
    int _visitorsLeft = 0;

    void Awake()
    {
        if (!Instance)
        {
            Instance = this;
            UpdateText();
        }
        else {Destroy(gameObject);}
    }

    public void RegisterVisitor()
    {
        _taxDollars++;
        UpdateText();
    }
    public void UnRegisterVisitor()
    {
        _visitorsLeft++;
        VistorPathManager.Instance.VisitorSpawnFrequency = 4 - Mathf.Log10(_visitorsLeft * 2);
        if (_visitorsLeft - _visitorsSinceLastPath > _visitorsTillNextPath)
        {
            Debug.Log("Increase difficulty");
            _visitorsTillNextPath *= 1.25f;
            VistorPathManager.Instance.AddPath();
            _visitorsSinceLastPath = _visitorsLeft;
        }
    }

    void UpdateText()
    {
        _taxDollarText.text = $"${_taxDollars}";
    }
}
