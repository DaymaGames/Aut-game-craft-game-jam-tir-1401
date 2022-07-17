using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAbilityManager : MonoBehaviour
{
    public HackerBoss hackerBossPrefab;
    public Transform bossSpawnPoint;

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
        BossAbility[] abs = FindObjectsOfType<BossAbility>();
        foreach (var ab in abs)
        {
            ab.GiveTypeToManager();
        }

        HackerBoss hackerBoss =
            Instantiate(hackerBossPrefab, bossSpawnPoint.position, bossSpawnPoint.rotation);

        hackerBoss.SetAbilities(abilities);
    }
}