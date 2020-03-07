using Assets.Objects.ObjectPooler;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : PooledObject
{
    public Rigidbody m_rb = null;
    private bool fired = false;

    GameObjectAudio wallHitAudio = null;
    GameObjectAudio zombieHitAudio = null;
    public void Awake()
    {
        m_rb = this.GetComponent<Rigidbody>();
        GameObjectAudio[] audios = GetComponents<GameObjectAudio>();
        zombieHitAudio = audios[1];
        wallHitAudio = audios[0];
    }


    internal void FireBullet()
    {
        m_rb.velocity = new Vector3(0, 0, 0);
        m_rb.AddForce(Game.CURRENTWEAPON.transform.forward * Game.CURRENTWEAPON.m_shootPower, ForceMode.Impulse);
        Game.CURRENTWEAPON.m_muzzleEffect.PlayEffect();
        fired = true;
    }

    public override void OnObjectSpawn()
    {
        waitingForDespawn = false;
        StartCoroutine(BulletTimeOut());
    }
    private IEnumerator BulletTimeOut()
    {
        yield return new WaitForSeconds(2);
        Game.POOL.DespawnObject("bullet", this);
    }
    public override void OnObjectDespawn()
    {
    }
    private bool waitingForDespawn = false;
    public void OnCollisionEnter(Collision collision)
    {
        if (fired && !waitingForDespawn)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Zombie"))
            {
                zombieHitAudio.TriggerRandomAudio();
            }
            else
            {
                wallHitAudio.TriggerRandomAudio();
            }
            waitingForDespawn = true;
            Game.POOL.DespawnObject("bullet", this, wallHitAudio.currentAudioClipLength);
        }
    }
}
