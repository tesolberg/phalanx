using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="New formation settings", menuName="Settings/Formation")]
public class FormationSettings : ScriptableObject
{
    public int maxUnitsSelected = 50;
    public int frontlineMaxWidth = 100;
    public float phalanxLinkRadius;
    public float maxPhalanxLinkDistance;
    public float minPhalanxLinkDistance;
    public float standardPhalanxLinkDistance;    

    public float[] circleFormationDistances;
    public int[] circleFormationRingCounts;
    
}
