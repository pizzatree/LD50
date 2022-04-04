using System;
using System.Collections.Generic;
using Common.Player.Inputs;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private UnityEvent _ohNo;

    [SerializeField] TMP_Text _taxDollarText;
    [SerializeField] GameObject _settingsMenu;

    public HashSet<Bonkable> Bonkables;

    PlayerInputs _inputs;
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
            Bonkables             =  new HashSet<Bonkable>();
            Bonkable.OnSpawnEvent += HandleBonkableSpawnEvent;
            _inputs = new PlayerInputs();
            UnpauseGame();
            PlayerController.OnPause += PauseGame;
            Visitor.OnPooStepping    += HandleGameOver;
        }
        else {Destroy(gameObject);}
    }

    private void HandleGameOver(Visitor obj)
    {
        _ohNo?.Invoke();
    }

    public void HandleBonkableSpawnEvent(Bonkable bonkable, bool active)
    {
        if(active)
        {
            Bonkables.Add(bonkable);
            return;
        }

        Bonkables.Remove(bonkable);
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
    
    public void PauseGame()
    {
        _inputs.UI.Enable();
        _settingsMenu.SetActive(true);
        Time.timeScale = 0;
    }

    public void UnpauseGame()
    {
        _inputs.Gameplay.Enable();
        _settingsMenu?.SetActive(false);
        Time.timeScale = 1;
    }

    void UpdateText()
    {
        _taxDollarText.text = $"${_taxDollars}";
    }
}
