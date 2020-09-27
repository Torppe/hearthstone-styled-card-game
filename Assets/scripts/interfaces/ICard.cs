using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICard {
    void Activate(GameObject placeholder);
    CardStats GetCardStats();
    void SetCardStats(CardStats cardStats);
    Player GetOwner();
    void SetOwner(Player owner);
}
