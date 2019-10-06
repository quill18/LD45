using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    //public static SoundManager Instance { get { return _instance; } }
    static SoundManager _instance;

    public GameObject AudioClipPlayerPrefab;

    void Awake()
    {
        _instance = this;
    }

    public static void PlayClip(AudioClip clip)
    {
        if (clip == null)
            return;


        _instance._PlayClip(clip);
    }

    public static void PlayClip(AudioClip[] clips)
    {
        if (clips == null || clips.Length <= 0)
            return;

        _instance._PlayClip(clips[Random.Range(0, clips.Length)]);
    }

    void _PlayClip(AudioClip clip)
    {
        Debug.Log("_PlayClip");
        AudioSource src = NewSource();
        src.clip = clip;
        src.Play();
    }

    AudioSource NewSource()
    {
        GameObject go = Instantiate(AudioClipPlayerPrefab, Vector3.zero, Quaternion.identity);
        go.transform.parent = this.transform;
        AudioSource src = go.GetComponent<AudioSource>();
        return src;
    }

}
