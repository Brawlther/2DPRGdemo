using UnityEngine;

public class Fireball : MonoBehaviour
{
    private float _speed;
    public float Speed {
        get => _speed;
        set => _speed = value;
    }
    private Vector2 _direction;
    private float knockbackForceHorizontal;
    private float knockbackForceVertical;
    private Vector3 spawnOffset;
    private Damage damageModule;
    private CapsuleCollider2D cc;
    [SerializeField]private GameObject hitEffectPrefab;

    void Awake()
    {
        cc = GetComponent<CapsuleCollider2D>();
        damageModule = GetComponent<Damage>();
        damageModule.SetAttackPoint(50);
        knockbackForceHorizontal = 3f;
        knockbackForceVertical = 7f;
        spawnOffset = new Vector3(0.5f, 0.5f, 0f);
    }

    public void SetDirection(Vector2 dir)
    {
        _direction = dir.normalized;
    }

    void Update()
    {
        transform.Translate(_direction * _speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ground"))
        {
            Instantiate(PrefabsManager.Instance.GetPrefabByName("explosioneffect"), transform.position + spawnOffset, Quaternion.identity);
            Destroy(gameObject);
        }
        else if (other.CompareTag("Monster"))
        {
            cc.enabled = false;
            Instantiate(PrefabsManager.Instance.GetPrefabByName("explosioneffect"), transform.position + spawnOffset, Quaternion.identity);
            DummyController dummy = other.GetComponent<DummyController>();
            if (dummy != null)
            {
                other.GetComponent<Dummy>().UpdateHp(damageModule.GetAttackPoint() * (-1));
            }
            Rigidbody2D enemyRB = other.GetComponent<Rigidbody2D>();
            if (enemyRB != null)
            {
                _direction.Normalize();
                Vector2 horizontalPush = new(_direction.x * knockbackForceHorizontal, 0f);
                Vector2 verticalPush = new(0f, knockbackForceVertical);
                Vector2 knockbackVelocity = horizontalPush + verticalPush;
                enemyRB.linearVelocity = knockbackVelocity;
            }
            Destroy(gameObject);
        }
    }
}
