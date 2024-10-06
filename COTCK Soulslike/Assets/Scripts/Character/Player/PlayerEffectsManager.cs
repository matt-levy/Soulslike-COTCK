using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class PlayerEffectsManager : CharacterEffectsManager
{
    [Header("Debug, delete later")]
    [SerializeField] InstantCharacterEffect effectTest;
    [SerializeField] bool processEffect = false;

    private void Update()
    {
        if (processEffect)
        {
            processEffect = false;
            // Why are we instantiating a copy of this???
            // Better to edit a the stats of a copy rather than the original
            InstantCharacterEffect effect = Instantiate(effectTest);
            ProcessInstantEffect(effect);
        }
    }
}   
