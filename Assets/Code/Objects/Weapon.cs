using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float m_shootPower = 100f;
    public Transform m_muzzleTransform = null;
    public Effects m_muzzleEffect = null;
    private void Awake()
    {
        m_muzzleTransform = this.transform.Find("MuzzleTransform"); 
    }
    // Update is called once per frame
    void Update()
    {
        if(Game.PLAYER != null)
        {
            this.transform.position = Game.PLAYER.m_weaponAttachment.transform.position;
            this.transform.rotation = Game.PLAYER.m_head.transform.rotation;
        }
    }
}
