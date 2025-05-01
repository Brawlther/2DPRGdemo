public abstract class Ability
{
    private string _abilityName;
    public string AbilityName {
        get => _abilityName;
        set => _abilityName = value;
    }
    private int _consumption;
    public int Consumption {
        get => _consumption;
        set => _consumption = value;
    }

    public void Launch(EntityCharacter caster){
        if (HasSufficientResource(caster)) {
            ConsumeResource(caster);
            Activate(caster);
        } else {
            ShowInsufficientResourdceAlert();
        }
    }
    //WIP
    protected void ShowInsufficientResourdceAlert() { }
    protected abstract bool HasSufficientResource(EntityCharacter caster);
    protected abstract void ConsumeResource(EntityCharacter caster);
    protected abstract void Activate(EntityCharacter caster);
}
