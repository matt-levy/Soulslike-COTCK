using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.Mathematics;
using System;

public class UI_Character_HP_Bar : UI_StatBar
{
    private CharacterManager character;

    [SerializeField] bool displayCharacterNameOnDamage = false;
    [SerializeField] float defaultTimeBeforeBarHides = 6;
    [SerializeField] float hideTimer = 0;
    [SerializeField] int currentDamageTaken = 0;
    //[SerializeField] TextMeshProUGUI characterName;
    //[SerializeField] TextMeshProUGUI characterDamage;

    protected override void Awake()
    {
        base.Awake();

        character = GetComponentInParent<CharacterManager>();
    }

    protected override void Start()
    {
        base.Start();

        gameObject.SetActive(false);
    }

    public override void SetStat(int newValue)
    {
        // Optional for now, ai doesnt have name
        // if (displayCharacterNameOnDamage)
        // {
            
        // }

        // Call this incase max health changes from an effect or something
        slider.maxValue = character.maxHealth;

        // TODO: run secondary bar logic (gray-red appears when damaged);

        // Total the damage taken whilst the bar is active
        int oldDamage = currentDamageTaken;
        currentDamageTaken = currentDamageTaken + oldDamage - newValue;

        if (currentDamageTaken < 0)
        {
            currentDamageTaken = Math.Abs(currentDamageTaken);
            //characterDamage.text = "+ " + currentDamageTaken.ToString();
        }
        // else
        // {
        //     characterDamage.text = "- " + currentDamageTaken.ToString();
        // }

        slider.value = newValue;

        if (character.currentHealth.Value != character.maxHealth)
        {
            hideTimer = defaultTimeBeforeBarHides;
            gameObject.SetActive(true);
        }
    }


    private void Update()
    {
        transform.LookAt(transform.position + Camera.main.transform.forward);

        if (hideTimer > 0)
        {
            hideTimer -= Time.deltaTime;
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    private void OnDisable()
    {
        currentDamageTaken = 0;
    }
}
