using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaminaItem : MonoBehaviour

{

    public GameObject Player;
    public float maxStamina = 30;
    public float currentStamina;
    public int StamReg = 10;
    public GameObject staminaPrefab;
    public GameObject stamina;
    public StaminaBar staminaBar;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))

        {
            if (staminaPrefab && currentStamina + StamReg < maxStamina)
            {
                Destroy(stamina);
                currentStamina += StamReg;
                staminaBar.SetStamina(currentStamina);

            }

        }
        if (staminaPrefab && currentStamina + StamReg > maxStamina)
        {
            Destroy(stamina);
            currentStamina = maxStamina;
            staminaBar.SetStamina(currentStamina);

        }

    }
}
