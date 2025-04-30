using Unity.VisualScripting;
using UnityEngine;

public class Knight : MonoSingleton<Knight>
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _dashSpeed;
    [SerializeField] private GameObject attriNode;
    private AttriController ac;
    private int _maxHp;
    private int _hp;
    private int _maxMp;
    private int _mp;
    private int _maxStamina;
    private int _stamina;
    private int _staminaConsumptionDash;
    private int _mpConsumptionFireball;

    public int GetStaminaConsumptionDash(){
        return _staminaConsumptionDash;
    }

    public int GetMpConsumptionFireball(){
        return _mpConsumptionFireball;
    }

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
    }

    public int GetMp(){
        return _mp;
    }

    public void UpdateMp(int change){
        _mp += change;
        ac.UpdateMpBarValueUI((float)_mp / _maxMp);
    }

    public int GetStamina(){
        return _stamina;
    }

    public void UpdateStamina(int change){
        _stamina += change;
        ac.UpdateStaminaBarValueUI((float)_stamina / _maxStamina);
    }

    protected override void Awake()
    {
        base.Awake();
        ac = attriNode.GetComponent<AttriController>();
        _maxHp = 100;
        _maxMp = 100;
        _maxStamina = 30;
        _speed = 3f;
        _dashSpeed = 9f;
        _jumpForce = 10f;
        _staminaConsumptionDash = 10;
        _mpConsumptionFireball = 50;
        UpdateHp(_maxHp);
        UpdateMp(_maxMp);
        UpdateStamina(_maxStamina);
    }
}
