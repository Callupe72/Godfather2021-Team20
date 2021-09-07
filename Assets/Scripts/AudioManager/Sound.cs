using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
    public float minDist = 0.1f;
    public float maxDist = 10f;
    [Range(0,1f)] public float volume = 1;
    public bool canLoop = false;
}
