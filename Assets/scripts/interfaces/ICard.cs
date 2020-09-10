using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICard {
    void Activate();
    CardStats GetCardStats();
    void SetCardStats(CardStats cardStats);
    Player GetOwner();
    void SetOwner(Player owner);
}
