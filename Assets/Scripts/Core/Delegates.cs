namespace RPG_Project
{
    // Look up C# actions to replace delegate voids
    public delegate void OnButtonPress();

    public delegate void OnCharacterChanged(Combatant combatant);
    public delegate void OnPartyChanged();

    public delegate void OnActivePartySelect(int index);
    public delegate void OnRegisteredItemSelect(int index);
    public delegate void OnShortcutSwitch(ShortcutMode mode);

    public delegate void OnHealthTick();
    public delegate void OnStaminaTick();
    public delegate void OnPoiseTick();

    public delegate void OnHealthChanged(int amount);
    public delegate void OnStaminaChanged(int amount);
    public delegate void OnPoiseChanged(int amount);

    public delegate void OnAmmoUse();
}