using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Audio", menuName = "Audio/MusicBlueSettings", order = 2)]
public class MusicBlue : ScriptableObject
{
    [Header("Música de cuarto azul")]
    public AudioClip blueMusic;
}
