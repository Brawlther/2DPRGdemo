using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    private Damage damageModule;
    private BoxCollider2D swordCollider;
    private HashSet<GameObject> hitEnemies;

    void Awake()
    {
        damageModule = GetComponent<Damage>();
        swordCollider = GetComponent<BoxCollider2D>();
        swordCollider.enabled = false;
        damageModule.SetAttackPoint(25);
        hitEnemies = new HashSet<GameObject>();
    }

    public void EnableSwordCollider()
    {
        swordCollider.enabled = true;
        hitEnemies.Clear();
    }

    public void DisableSwordCollider()
    {
        swordCollider.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Monster"))
        {
            if (hitEnemies.Contains(other.gameObject))
            {
                // Already hit this monster during this attack, ignore
                return;
            }

            // First time hitting this monster in this swing
            hitEnemies.Add(other.gameObject);

            DummyController dummy = other.GetComponent<DummyController>();
            if (dummy != null)
            {
                other.GetComponent<Dummy>().UpdateHp(damageModule.GetAttackPoint() * (-1));
            }
        }
    }
}
