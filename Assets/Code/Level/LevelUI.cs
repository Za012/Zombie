using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LevelUI : LevelBase
{
    public Text m_hp;
    public Text m_announcement = null;
    public Text m_ammo = null;

    public void Awake()
    {
        m_announcement.enabled = false;
    }
    public void ChangeHpText(string text)
    {
        m_hp.text = text;
    }

    public void TriggerAnnouncement(string text)
    {
        StartCoroutine(Announcement(text));
    }
    private IEnumerator Announcement(string text)
    {
        m_announcement.text = text;
        m_announcement.enabled = true;
        yield return new WaitForSeconds(4f);
        m_announcement.enabled = false;
    }
}
