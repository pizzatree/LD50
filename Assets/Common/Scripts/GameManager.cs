using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] TMP_Text _taxDollarText;

    int _taxDollars = 0;

    void Awake()
    {
        if (!Instance) { Instance = this; }
        else {Destroy(gameObject);}
    }

    public void RegisterVisitor()
    {
        _taxDollars++;
        UpdateText();
    }
    public void UnRegisterVisitor() {  }

    void UpdateText()
    {
        _taxDollarText.text = $"${_taxDollars}";
    }
}
