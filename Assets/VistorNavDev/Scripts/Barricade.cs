using UnityEngine;

public class Barricade : MonoBehaviour, IBonkable
{
    [SerializeField] int _hp;

    void OnCollisionEnter(Collision other)
    {
        IBonker bonker;
        if(other.gameObject.TryGetComponent<IBonker>(out bonker))
            OnBonk(bonker);
    }
    public void OnBonk(IBonker bonker)
    {
        _hp -= bonker.GetBonkValue();
        if (_hp <= 0)
        {
            Destroy(gameObject);
        }
    }
}