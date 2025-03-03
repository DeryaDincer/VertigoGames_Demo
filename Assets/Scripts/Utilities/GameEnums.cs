using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEnums 
{
  
}
public enum RewardType
{
    ArmorPoint = 0,
    BaseballCap= 1,
    BayonetEaster= 2,
    BayonetSummer= 3,
    Bomb= 4,
    Cash= 5,
    ChestBig= 6,
    ChestBronze= 7,
    ChestSilver= 8,
    ChestGold= 9,
    ChestStandart= 10,
    ChestSuper= 11,
    Molotov= 12,
    Glasses= 13,
    Gold= 14,
    GrenadeM26= 15,
    GrenadeM67= 16,
    HealthshotNeurostim= 17,
    HealthshotRegenerator= 18,
    Pumpkin= 19,
    KnifePoints= 20,
    PistolPoints= 21,
    RiflePoints= 22,
    Shotgun= 23,
    SmgPoints= 24,
    Sniper= 25,
    SniperPoints= 26,
    SubmachinePoints= 27,
    ChestSmall= 28,
    Tier2Mle= 29,
    Tier2Rifle= 30,
    Tier3Shotgun= 31,
    Tier3Smg= 32,
    Tier3Snipe= 33,
    VectPoints= 34,
}

public enum RewardWeight
{
    VeryLow = 0,    
    Low = 3,       
    Medium = 5,     
    High = 7,      
    VeryHigh = 9    
}

public enum ZoneType
{
    Normal,
    Safe,
    Super
}

public enum WindowType
{
    None,
    RewardWindow
}

public enum TaskType
{
    None,
    ShowRewardWindow = 1,
    ZoneBar = 3,
    RewardArea = 2,
}