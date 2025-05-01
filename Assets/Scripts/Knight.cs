using UnityEngine;

public class Knight : EntityCharacter
{
    /* TO BE REMOVED */
    [SerializeField] private GameObject attriNode;
    private AttriController ac;
    /* TO BE REMOVED */

    private int _staminaConsumptionDash;
    private int _mpConsumptionFireball;

    public int GetStaminaConsumptionDash(){
        return _staminaConsumptionDash;
    }

    public int GetMpConsumptionFireball(){
        return _mpConsumptionFireball;
    }

    public override void UpdateHp(int change){
        base.UpdateHp(change);
        ac.UpdateMpBarValueUI((float)Hp / MaxHp);
    }

    public override void UpdateMp(int change){
        base.UpdateMp(change);
        ac.UpdateMpBarValueUI((float)Mp / MaxMp);
    }


    public override void UpdateStamina(int change){
        base.UpdateStamina(change);
        ac.UpdateStaminaBarValueUI((float)Stamina / MaxStamina);
    }

    protected void Awake()
    {
        ac = attriNode.GetComponent<AttriController>();

        UpdateMaxHp(100);
        UpdateMaxMp(100);
        UpdateMaxStamina(30);
        UpdateSpeed(3);
        UpdateDashSpeed(9);
        UpdateJumpForce(10);
        UpdateHp(MaxHp);
        UpdateMp(MaxMp);
        UpdateStamina(MaxStamina);

        CapsuleCollider2D collider = GetComponent<CapsuleCollider2D>();
        OriginalColliderSize = collider.size;
        OriginalColliderOffset = collider.offset;

        //to be removed
        _staminaConsumptionDash = 10;
        _mpConsumptionFireball = 50;
    }
}
