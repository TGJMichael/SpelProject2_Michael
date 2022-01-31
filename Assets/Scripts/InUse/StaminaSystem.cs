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

    public StaminaBar staminaBar;

    [SerializeField]
    private bool staminaRegOn;
    private Coroutine staminaCooldown;

    // Item drop  // not needed.
    //public int StamReg = 10;
    //public GameObject staminaPrefab;
    //public GameObject stamina;

    void Start()
    {
        currentStamina = maxStamina;
        staminaBar.SetMaxStamina(maxStamina);

        staminaCooldown = StartCoroutine(staminaRegCooldown());
    }


    private void Update()
    {
        if (staminaRegOn && currentStamina < maxStamina)
        {
            currentStamina += staminaRegSpeed * Time.deltaTime;
            staminaBar.SetStamina(currentStamina);

        }
    }
    // Item drop  // not needed
    //public void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Player"))
    //        GetComponent<StaminaSystem>();
    //    {
    //        Destroy(stamina);
    //        currentStamina += StamReg;
    //        staminaBar.SetStamina(currentStamina);
    //    }
    //}

    // new Item drop
    public void RegainStamina(int StamReg)
    {
        currentStamina += StamReg;

        if (currentStamina >= maxStamina)
        {
            currentStamina = maxStamina;
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
