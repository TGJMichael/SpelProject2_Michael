using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergySystem : MonoBehaviour
{
    // recharge and spit(ammo)   // moved to "EnergySystem"
    public float maxSpit = 5;       // each spit costs (1)
    public float currentSpit;
    public float spitRegSpeed = 3;
    public float spitRegCooldownTime = 1.5f;

    public SpitBar spitbar;

    [SerializeField]
    private bool spitRegOn;
    private Coroutine spitCooldown;

    private void Start()
    {
        spitbar = FindObjectOfType<SpitBar>();

        // recharge and "ammo"
        currentSpit = maxSpit;
        spitbar.SetMaxSpit(maxSpit);

        spitCooldown = StartCoroutine(spitRegCooldown());
    }
    private void Update()
    {

        // recharge and spit(energy)
        if (spitRegOn && currentSpit < maxSpit)
        {
            currentSpit += spitRegSpeed * Time.deltaTime;
            spitbar.SetSpit(currentSpit);
        }

    }

    // recharge and spit(energy)
    public void SpendSpit(int spent)
    {
        currentSpit -= spent;
        spitbar.SetSpit(currentSpit);

        StopCoroutine(spitCooldown);
        spitCooldown = StartCoroutine(spitRegCooldown());
    }

    private IEnumerator spitRegCooldown()
    {
        spitRegOn = false;

        yield return new WaitForSeconds(spitRegCooldownTime);

        spitRegOn = true;
    }

}
