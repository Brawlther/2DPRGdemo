using UnityEngine;

public class Dummy : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _dashSpeed;
    private int _maxHp;
    private int _hp;
    private int _maxMp;
    private int _mp;
    private int _maxStamina;
    private int _stamina;
    private DummyController controller;

    public float GetSpeed(){
        return _speed;
    }

    public float GetJumpForce(){
        return _jumpForce;
    }

    public float GetDashSpeed(){
        return _dashSpeed;
    }

    public int GetMaxHp(){
        return _maxHp;
    }

    public int GetMaxMp(){
        return _maxMp;
    }

    public int GetMaxStamina(){
        return _maxStamina;
    }

    public int GetHp(){
        return _hp;
    }

    public void UpdateHp(int change){
        _hp += change;
        if(_hp <= 0){
            controller.StartDie();
        }
    }

    public int GetMp(){
        return _mp;
    }

    public void UpdateMp(int change){
        _mp += change;
    }

    public int GetStamina(){
        return _stamina;
    }

    public void UpdateStamina(int change){
        _stamina += change;
    }

    void Awake()
    {
        _maxHp = 100;
        _maxMp = 100;
        _maxStamina = 30;
        _speed = 3f;
        _dashSpeed = 9f;
        _jumpForce = 10f;
        UpdateHp(_maxHp);
        UpdateMp(_maxMp);
        UpdateStamina(_maxStamina);
        controller = GetComponent<DummyController>();
    }
}
