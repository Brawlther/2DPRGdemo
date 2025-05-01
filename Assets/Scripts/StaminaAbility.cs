public abstract class StaminaAbility : Ability
{
    protected override bool HasSufficientResource(EntityCharacter caster){
        return caster.Stamina > Consumption;
    }
    protected override void ConsumeResource(EntityCharacter caster) {
        caster.UpdateStamina(Consumption);
    }
}