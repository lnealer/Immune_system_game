using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameValues : MonoBehaviour
{
    public static float health { get; set; } = 40;
    public static float levelNVaccine { get; set; } = 0;
    public static int losses { get; set; } = 0;
    public static float healthGainPerLevel {get; set;} = 20;
}
