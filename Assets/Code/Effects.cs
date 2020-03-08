using System.Collections;
using UnityEngine;

public class Effects : MonoBehaviour
{
    public ParticleSystem m_ps = null;
    public Light m_light = null;
    public float duration = 0.2f;

    public void PlayEffect()
    {
        m_light.enabled = true;
        m_ps.Play();
        duration = m_ps.main.startLifetime.constant;
        StartCoroutine(StopEffect());
    }

    private IEnumerator StopEffect()
    {
        yield return new WaitForSeconds(duration);
        m_light.enabled = false;

    }
}
