using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Abilities
{
    public enum SizeMode { Big,Medium,Small }
    public enum MoveMode { Air,Ground }
    public enum AttackMode { Far,Close }

    public SizeMode sizeMode;
    public MoveMode moveMode;
    public AttackMode attackMode;
}
