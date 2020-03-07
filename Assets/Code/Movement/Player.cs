using Assets.Objects.ObjectPooler;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Transform m_weaponAttachment = null;
    public Transform m_head = null;
    private void Awake()
    {
        m_head = this.transform.Find("Main Camera");
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {

            PooledObject b = Game.POOL.SpawnFromPool("bullet", Game.CURRENTWEAPON.m_muzzleTransform.position, Game.CURRENTWEAPON.m_muzzleTransform.rotation);
            if(b != null)
            {
                b.GetComponent<Bullet>().FireBullet();
                GetComponent<AudioSource>().Play();
            }
        }
    }
}
