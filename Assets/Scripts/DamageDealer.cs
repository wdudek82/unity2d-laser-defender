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

    private void OnTriggerEnter2D(Collider2D other)
    {
        Hit();
    }

    private void Hit()
    {
        Destroy(gameObject);
    }
}