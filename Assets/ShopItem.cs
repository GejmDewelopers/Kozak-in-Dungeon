using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopItem : MonoBehaviour
{
    public Transform player;

    int price = 10;

    [SerializeField] float damageIncrease = 0f;
    [SerializeField] float damageMultiplier = 1f;

    [SerializeField] float movementSpeedChange = 0f;
    [SerializeField] float movementSpeedMultiplier = 1f;

    [SerializeField] int healing = 0;

    [SerializeField] float dashChargeReduction = 0f;

    [SerializeField] float lightAttackMultiplierIncrease = 0f;
    [SerializeField] float lightAttackMultiplierMultiplier = 1f;

    [SerializeField] float mediumAttackMultiplierIncrease = 0f;
    [SerializeField] float mediumAttackMultiplierMultiplier = 1f;

    [SerializeField] float strongAttackMultiplierIncrease = 0f;
    [SerializeField] float strongAttackMultiplierMultiplier = 1f;

    PlayerShooting playerShooting;
    PlayerHealth playerHealth;
    PlayerMovement playerMovement;
    PlayerUtilities playerUtilities;

    CircleCollider2D itemCollider;

    TextMeshProUGUI priceDisplay;
    void Start()
    {
        //Finding player, that could be any unique script on player
        player = FindObjectOfType<PlayerHealth>().transform;

        //Load all scripts on player where stats will be modified
        playerShooting = player.GetComponent<PlayerShooting>();
        playerHealth = player.GetComponent<PlayerHealth>();
        playerMovement = player.GetComponent<PlayerMovement>();
        playerUtilities = player.GetComponent<PlayerUtilities>();

        //Create trigger collider to pick up
        itemCollider = gameObject.AddComponent<CircleCollider2D>();
        itemCollider.radius = 0.3f;
        itemCollider.isTrigger = true;

        //price above item
        priceDisplay = GetComponentInChildren<TextMeshProUGUI>();
        priceDisplay.text = price.ToString() + '$';
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player" && playerUtilities.GetMoneyAmount() >= price)
        {
            //modify stats on player
            playerUtilities.subtractMoney(price);

            playerShooting.FlatChangeDamage(damageIncrease);
            playerShooting.MultiplierChangeDamage(damageMultiplier);

            playerMovement.FlatChangeSpeed(movementSpeedChange);
            playerMovement.MultiplierChangeSpeed(movementSpeedMultiplier);

            playerHealth.ChangeHPAndDisplay(healing);

            playerMovement.FlatChangeTimeToChargeOneEvadeStack(dashChargeReduction);

            playerShooting.FlatChangeLightAttackMultiplier(lightAttackMultiplierIncrease);
            playerShooting.MultiplierChangeLightAttackMultiplier(lightAttackMultiplierMultiplier);

            playerShooting.FlatChangeMediumAttackMultiplier(mediumAttackMultiplierIncrease);
            playerShooting.MultiplierChangeMediumAttackMultiplier(mediumAttackMultiplierMultiplier);

            playerShooting.FlatChangeStrongAttackMultiplier(strongAttackMultiplierIncrease);
            playerShooting.MultiplierChangeStrongAttackMultiplier(strongAttackMultiplierMultiplier);


            Destroy(gameObject);
        }
    }



}
