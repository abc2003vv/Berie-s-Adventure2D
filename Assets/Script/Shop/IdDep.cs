using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable, CreateAssetMenu(fileName = "New IdDep", menuName = "Create new IdDep")]
public class IdDep : ScriptableObject
{
    public enum DepIds { Deplao1, Deplao2, Deplao3, Deplao4, Deplao5, Deplao6 }
    public DepIds depIds;

    public Sprite idSprite;
    public int idDepPrice;
}
