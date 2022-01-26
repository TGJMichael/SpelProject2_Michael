using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaminaSystem : MonoBehaviour
{
    public float maxStamina = 30;
    public float currentStamina;
    public float staminaRegSpeed = 3;
    public float staminaRegCooldownTime = 1.5f;

    public int StamReg = 10;
    public GameObject staminaPrefab;
    public GameObject stamina;
    public StaminaBar staminaBar;

    [SerializeField]
    public bool staminaRegOn;
    public Coroutine staminaCooldown;

    public void Start()
    {
        currentStamina = maxStamina;
        staminaBar.SetMaxStamina(maxStamina);

        staminaCooldown = StartCoroutine(staminaRegCooldown());
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            GetComponent<StaminaSystem>();
        {
            Destroy(stamina);
            currentStamina += StamReg;
            staminaBar.SetStamina(currentStamina);
        }
    }




    public void Update()
    {
        if (staminaRegOn && currentStamina < maxStamina)
        {
            currentStamina += staminaRegSpeed * Time.deltaTime;
            staminaBar.SetStamina(currentStamina);

        }
    }
    

    public void SpendStamina (int spent)
    {
        currentStamina -= spent;

        staminaBar.SetStamina(currentStamina);        

        StopCoroutine(staminaCooldown);
        staminaCooldown = StartCoroutine(staminaRegCooldown());
                
    }

    private IEnumerator staminaRegCooldown()
    {
        staminaRegOn = false;

        yield return new WaitForSeconds(staminaRegCooldownTime);

        staminaRegOn = true;
    }
}
