namespace RPG_Project
{
    public delegate void OnButtonPress();

    public delegate void OnCharacterChanged(BattleChar character);

    public delegate void OnHealthChanged(int amount);
    public delegate void OnStaminaChanged(int amount);
    public delegate void OnPoiseChanged(int amount);
}