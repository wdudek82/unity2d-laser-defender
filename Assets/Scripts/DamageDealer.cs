using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] private int damage = 100;
    [SerializeField] int enemyMissle;
    [SerializeField] int enemyBomb;

    public int Damage
    {
        get => damage;
    }
    
    public void Hit()
    {
        Destroy(gameObject);
    }
}