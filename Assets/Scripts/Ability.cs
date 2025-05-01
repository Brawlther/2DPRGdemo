public abstract class Ability
{
    protected string abilityName;
    public string AbilityName {
        get => abilityName;
        set => abilityName = value;
    }
    protected int consumption;
    public int Consumption {
        get => consumption;
        set => consumption = value;
    }

    public void Launch(EntityCharacter caster){
        if (CanCast(caster)) {
            ConsumeResource(caster);
            Activate(caster);
        } else {
            ShowInsufficientResourdceAlert();
        }
    }
    //WIP
    protected void ShowInsufficientResourdceAlert() { }
    public abstract bool CanCast(EntityCharacter caster);
    protected abstract void ConsumeResource(EntityCharacter caster);
    protected abstract void Activate(EntityCharacter caster);
}
