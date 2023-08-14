using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Add Card Start Action", menuName = "Add Card Start Action")]
public class AddCardStartActionInfo : StartActionInfo
{
    public override StartActionType actionType => StartActionType.AddCard;
    public CardInfo cardInfo;
    public int numCards;
}
