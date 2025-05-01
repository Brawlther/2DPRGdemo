public abstract class StaminaAbility : Ability
{
    public override bool CanCast(EntityCharacter caster){
        return caster.Stamina >= Consumption;
    }
    protected override void ConsumeResource(EntityCharacter caster) {
        caster.UpdateStamina(-Consumption);
    }
}