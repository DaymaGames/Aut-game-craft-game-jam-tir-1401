using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossAbilityManager : MonoBehaviour
{
    public HackerBoss hackerBossPrefab;
    public Transform bossSpawnPoint;

    public List<ErrorMatch> wrongMatches = new List<ErrorMatch>();

    public Button spawnButton;

    public BossAbility[] allAbilities;

    [Space]

    public ErrorMatch matchToAdd;

    Transform parent;

    Abilities abilities = new Abilities();

    private void Awake()
    {
        parent = transform.GetChild(0);
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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (parent.gameObject.activeSelf)
            {
                CloseMenu();
            }
            else
            {
                OpenMenu();
            }
        }

        foreach (var ab in allAbilities)
        {
            ab.GiveTypeToManager();
        }

        Abilities.SizeMode size = abilities.sizeMode;
        Abilities.AttackMode attack = abilities.attackMode;
        Abilities.MoveMode move = abilities.moveMode;

        foreach (var match in wrongMatches)
        {
            if (match.IsMatch(size, attack, move))
            {
                spawnButton.interactable = false;
                //print(size.ToString() + " " + attack.ToString() + " " + move.ToString());
                return;
            }
        }
        spawnButton.interactable = true;
    }

    void OpenMenu()
    {
        parent.gameObject.SetActive(true);
    }

    void CloseMenu()
    {
        parent.gameObject.SetActive(false);
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
    }
}
[System.Serializable]
public class ErrorMatch
{
    public bool bypassSize = false;
    public bool bypassAttack = false;
    public bool bypassMove = false;
    [Space]
    public Abilities.SizeMode sizeMode;
    public Abilities.AttackMode attackMode;
    public Abilities.MoveMode moveMode;

    public bool IsMatch(Abilities.SizeMode size, Abilities.AttackMode attack, Abilities.MoveMode move)
    {
        if(bypassAttack == false && bypassMove == false && bypassSize == false)
        {
            if (size == sizeMode && attack == attackMode && move == moveMode)
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
        else if (bypassMove == false && bypassSize == false)
        {
            if (move == moveMode && size == sizeMode)
                return true;
        }

        return false;
    }
}