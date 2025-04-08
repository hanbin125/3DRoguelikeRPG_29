using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CurrencyType
{
    Gold

}

public class CurrencyManager : MonoBehaviour
{
    public Dictionary<CurrencyType, int> currencies;
    public event Action<int> OnGoldChange;

    private void Start()
    {
        init();
    }

    public void init()
    {
        currencies = new Dictionary<CurrencyType, int>
        {
            { CurrencyType.Gold, 0 }
        };
    }
    public bool AddCurrency(CurrencyType currencyType, int amount)
    {
        if (currencies.TryGetValue(currencyType, out int currency))
        {
            int AddCurrency = currency + amount;
            if (AddCurrency < 0)
            {
                Debug.Log($"{currencyType}의 값이 0보다 작습니다");
                return false;
            }
            else
            {
                currencies[currencyType] = AddCurrency;
                if (currencyType == CurrencyType.Gold)
                {
                    OnGoldChange?.Invoke(currencies[currencyType]);
                }
                return true;
            }

        }
        else
        {
            Debug.Log($"{currencyType}이 잘못됬습니다. or {currencyType}값이 없습니다");
            return false;
        }
    }

}
