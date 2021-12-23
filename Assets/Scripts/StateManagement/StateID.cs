namespace RPG_Project
{
    public enum StateID
    {
        Empty = 0,

        GameOverworld = 1,
        GameBattle = 2,
        GameMenu = 3,

        BattleStart = 21,
        BattleCombat = 22,
        BattleWin = 23,
        BattleLose = 24,
        BattleItem = 25,
        BattleSwitch = 26,

        PlayerOverworld = 51,
        PlayerBattle = 52,
        
        // Within PlayerBattleState
        PlayerCommand = 61,
        PlayerLook = 62,
        PlayerWait = 63,
    }
}