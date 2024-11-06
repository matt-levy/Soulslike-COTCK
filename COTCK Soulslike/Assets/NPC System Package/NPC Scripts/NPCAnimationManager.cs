using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCAnimationManager : CharacterAnimatorManager
{
    NPCManager npc;

    protected override void Awake()
    {
        base.Awake();

        npc = GetComponent<NPCManager>();
    }
}
