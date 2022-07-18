using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Abilities
{
    public enum SizeMode { Big,Medium,Small }
    public enum MoveMode { Air,Ground }
    public enum AttackMode { Far,Close }
    public enum SpeedMode { Fast, Medium, Slow }

    public SizeMode sizeMode;
    public MoveMode moveMode;
    public AttackMode attackMode;
    public SpeedMode speedMode;
}
