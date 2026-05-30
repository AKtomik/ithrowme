using UnityEngine;
using System;

public enum  SoundType
{
    ITEM,
    PLAYER,
    UI
}

[RequireComponent(typeof(AudioSource)), ExecuteInEditMode]
public class SoundManager : MonoBehaviour
{
    [SerializeField] private SoundList[] soundList;
    private static SoundManager instance;
    private AudioSource audioSource;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public static void PlaySound(SoundType sound, int index, float volume = 1)
    {
        AudioClip[] clips = instance.soundList[(int)sound].Sounds;


        instance.audioSource.PlayOneShot(clips[index], volume);
    }

#if UNITY_EDITOR
    private void OnEnable()
    {
        string[] names = Enum.GetNames(typeof(SoundType));
        Array.Resize(ref soundList, names.Length);

        for (int i = 0; i < soundList.Length; i++)
        {
            soundList[i].name = names[i];
        }
    }
#endif
}

[Serializable]
    public struct SoundList
{
    public AudioClip[] Sounds { get => Sounds; }
    [HideInInspector] public string name;
    [SerializeField] private AudioClip[,] soundList;
}