using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGameplayManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private AudioSource asource;
    void Start()
    {
        Health health = GetComponent<Health>();
        health.SetMaxHealth(GameDataManager.instance.GetPlayerHealth());
        health.takeDamageAction += PlayHitSound;
    }

    public  void PlayHitSound()
    {
        asource.Play();
    }

}
