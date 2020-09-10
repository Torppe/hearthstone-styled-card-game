using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICard {
    void Activate();
    void SetCardStats(CardStats cardStats);
    CardStats GetCardStats();
}
