using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatorEffect : MonoBehaviour
{
    public EffectManager effectManager;

    private void OnTriggerEnter(Collider other)
    {
        if (effectManager.IsAnyEffectActive())
        {
            effectManager.HideEffect();

            //Проиграть звук
        }
        else
        {
            //Проиграть звук
        }       

    }

}
