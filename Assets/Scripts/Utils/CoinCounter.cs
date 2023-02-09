using System;
using System.Collections.Generic;
using UnityEngine;

namespace PlatformerMVC
{
    public class CoinCounter
    {
        public Action<int> ChangeCoins { get; set; }

        int _coinAmount;

        public CoinCounter(int coinAmount)
        {
            _coinAmount = coinAmount;
        }

        public void AddCoins(QuestObjectView contactView)
        {
            _coinAmount += contactView.Points;
            ChangeCoins?.Invoke(_coinAmount);
        }
    }
}
