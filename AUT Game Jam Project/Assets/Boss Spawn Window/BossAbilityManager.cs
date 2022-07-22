using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;

public class BossAbilityManager : MonoBehaviour
{
    public HackerBoss hackerBossPrefab;

    public HackerBoss hackerBossPreview;

    public Transform bossSpawnPoint;

    public Button spawnButton;

    public BossAbility[] allAbilities;

    public float timeToDesign = 10;

    public TMPro.TextMeshProUGUI timeText;
    public string pishvand = "Remaining: ";

    float time = 0;

    Transform parent;

    Abilities abilities = new Abilities();

    public static bool DesigningBoss { get; private set; } = false;

    private void Awake()
    {
        parent = transform.GetChild(0);
        CloseMenu();
    }

    public void SetMode(Abilities.SizeMode sizeMode)
    {
        abilities.sizeMode = sizeMode;
    }
    public void SetMode(Abilities.MoveMode moveMode)
    {
        abilities.moveMode = moveMode;
    }
    public void SetMode(Abilities.AttackMode attackMode)
    {
        abilities.attackMode = attackMode;
    }
    public void SetMode(Abilities.SpeedMode speedMode)
    {
        abilities.speedMode = speedMode;
    }

    private void Update()
    {
        if (time <= 0)
            return;

        time -= Time.deltaTime;

        timeText.SetText(pishvand + time.ToString("0"));

        if (time <= 0)
            CloseMenu();

        foreach (var ab in allAbilities)
        {
            ab.GiveTypeToManager();
        }

        hackerBossPreview.SetAbilities(abilities);

        Abilities.SizeMode size = abilities.sizeMode;
        Abilities.AttackMode attack = abilities.attackMode;
        Abilities.MoveMode move = abilities.moveMode;
        Abilities.SpeedMode speed = abilities.speedMode;

        /*foreach (var match in wrongMatches)
        {
            if (match.IsMatch(size, attack, move, speed))
            {
                spawnButton.interactable = false;
                //print(size.ToString() + " " + attack.ToString() + " " + move.ToString());
                return;
            }
        }*/
        #region Wrong Matchs
        if (speed == Abilities.SpeedMode.Fast || speed == Abilities.SpeedMode.Medium)
        {
            if(size == Abilities.SizeMode.Big)
            {
                spawnButton.interactable = false;
                return;
            }
        }
        if(move == Abilities.MoveMode.Air)
        {
            if (size == Abilities.SizeMode.Big)
            {
                spawnButton.interactable = false;
                return;
            }
        }
        if(size == Abilities.SizeMode.Small)
        {
            if(attack == Abilities.AttackMode.Far)
            {
                spawnButton.interactable = false;
                return;
            }
        }
        #endregion
        spawnButton.interactable = true;
    }
    [Button]
    public void StartDesigning()
    {
        time = timeToDesign;
        OpenMenu();
    }
    
    void OpenMenu()
    {
        parent.gameObject.SetActive(true);
        DesigningBoss = true;
    }
    [Button]
    void CloseMenu()
    {
        parent.gameObject.SetActive(false);
        DesigningBoss = false;
    }

    public void SpawnBoss()
    {
        foreach (var ab in allAbilities)
        {
            ab.GiveTypeToManager();
        }

        HackerBoss hackerBoss =
            Instantiate(hackerBossPrefab, bossSpawnPoint.position, bossSpawnPoint.rotation);

        hackerBoss.SetAbilities(abilities);

        time = 0;
        CloseMenu();
    }
}
/*
[System.Serializable]
public class ErrorMatch
{
    public bool bypassSize = false;
    public bool bypassAttack = false;
    public bool bypassMove = false;
    public bool bypassSpeed = false;
    [Space]
    public Abilities.SizeMode sizeMode;
    public Abilities.AttackMode attackMode;
    public Abilities.MoveMode moveMode;
    public Abilities.SpeedMode speedMode;

    public bool IsMatch(Abilities.SizeMode size, Abilities.AttackMode attack, Abilities.MoveMode move, Abilities.SpeedMode speed)
    {
        if (bypassAttack == false && bypassMove == false && bypassSize == false && bypassSpeed == false)
        {
            if (size == sizeMode && attack == attackMode && move == moveMode && speed == speedMode)
                return true;

        }
        else if (bypassAttack == false && bypassMove == false && bypassSize == false)
        {
            if (size == sizeMode && attack == attackMode && move == moveMode)
                return true;

        }
        else if(bypassAttack == false && bypassMove == false && bypassSpeed == false)
        {
            if (attack == attackMode && move == moveMode && speed == speedMode)
                return true;
        }
        else if (bypassAttack == false && bypassSize == false && bypassSpeed == false)
        {
            if (attack == attackMode && size == sizeMode && speed == speedMode)
                return true;
        }
        else if (bypassSize == false && bypassMove == false && bypassSpeed == false)
        {
            if (size == sizeMode && move == moveMode && speed == speedMode)
                return true;
        }
        else if(bypassAttack == false && bypassMove == false)
        {
            if (attack == attackMode && move == moveMode)
                return true;
        }
        else if(bypassAttack == false & bypassSize == true)
        {
            if (attack == attackMode && size == sizeMode)
                return true;
        }
        else if(bypassAttack == false & bypassSpeed == true)
        {
            if (attack == attackMode && speed == speedMode)
                return true;
        }
        else if (bypassMove == false && bypassSize == false)
        {
            if (move == moveMode && size == sizeMode)
                return true;
        }
        else if (bypassMove == false && bypassSpeed == false)
        {
            if (move == moveMode && speed == speedMode)
                return true;
        }
        else if (bypassSize == false && bypassSpeed == false)
        {
            if (size == sizeMode && speed == speedMode)
                return true;
        }

        return false;
    }
}
*/