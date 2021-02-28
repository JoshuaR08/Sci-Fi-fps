using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSettings : MonoBehaviour
{
    public AudioSource _audioSource1;
    public AudioSource _audioSource2;
    public GameObject MuzzleFlash;
    public AudioClip headShotHit;
    public AudioClip bodyShotHit;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void PlayEffects()
    {
        _audioSource1.Play();
        MuzzleFlash.SetActive(true);
        StartCoroutine(muzzleFlash());
    }

    IEnumerator muzzleFlash()
    {
        yield return new WaitForSeconds(0.1f);
        MuzzleFlash.SetActive(false);
    }

    public void PlayHitEffects(enemyCollider.ColliderType colliderType)
    {
        if(colliderType == enemyCollider.ColliderType.Head)
        {
            _audioSource2.PlayOneShot(headShotHit);
        }
        else if(colliderType == enemyCollider.ColliderType.Body)
        {
            _audioSource2.PlayOneShot(bodyShotHit);
        }
    }

    

}
