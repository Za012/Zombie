using Assets.Objects.ObjectPooler;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Transform m_weaponAttachment = null;
    public Transform m_head = null;
    public int healthPoints;
    public GameObjectAudio painAudio = null;
    public GameObjectAudio shootingAudio = null;
    private void Awake()
    {
        m_head = this.transform.Find("Main Camera");
        healthPoints = 100;
    }

    private void Start()
    {
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            PooledObject b = Game.POOL.SpawnFromPool("bullet", Game.CURRENTWEAPON.m_muzzleTransform.position, Game.CURRENTWEAPON.m_muzzleTransform.rotation);
            if (b != null)
            {
                b.GetComponent<Bullet>().FireBullet();
                shootingAudio.TriggerRandomAudio();
            }
        }
    }

    public void Damage(int hp)
    {
        healthPoints -= hp;
        if (healthPoints <= 0)
        {
            Game.UI.ChangeHpText("ZOMBIE");
        }
        else
        {
            Game.UI.ChangeHpText(healthPoints.ToString());
        }
        painAudio.TriggerRandomAudio();
    }
}
