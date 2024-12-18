using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatsManager : CharacterStatsManager
{
    PlayerManager player;
    protected override void Awake()
    {
        base.Awake();

        player = GetComponent<PlayerManager>();
    }

    public override void Start()
    {
        base.Start();

        CalculateTotalHealthBasedOnLevel(player.vitality.Value);
        CalculateTotalStaminaBasedOnLevel(player.endurance.Value);
    }
}
