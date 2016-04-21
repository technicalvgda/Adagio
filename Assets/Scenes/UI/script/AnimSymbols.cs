using UnityEngine;
using System.Collections;

public class AnimSymbols : MonoBehaviour
{
    public Animation[] symbols;

    public void LightUp(int symbol)
    {
        symbol = Mathf.Clamp(symbol, 0, symbols.Length);
        symbols[symbol].Play("animSymbolLightUp", PlayMode.StopAll);
    }

    public void TurnToDot(int symbol)
    {
        symbol = Mathf.Clamp(symbol, 0, symbols.Length);
        symbols[symbol].Play("animSymbolToDot", PlayMode.StopAll);
    }

    //in your dreams
    //public Animation[] anims;
    //public string[] clips;
    //public void Play(int animIndex, int clipIndex) {}
}
