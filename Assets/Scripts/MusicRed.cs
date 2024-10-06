using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Audio", menuName = "Audio/MusicRedSettings", order = 1)]
public class MusicRed : ScriptableObject
{
    [Header("Música de cuarto rojo")]
    public AudioClip redMusic;  
}