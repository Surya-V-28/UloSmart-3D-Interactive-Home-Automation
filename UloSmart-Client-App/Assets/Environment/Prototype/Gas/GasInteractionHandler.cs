using UnityEngine;
using GasSystem;

[RequireComponent(typeof(Gas))]
public class GasInteractionHandler : MonoBehaviour
{
    private void OnEnable()
    {
        gas = GetComponent<Gas>();

        gas.AddOnToggleEmitListener(AddOnToggleEmitListener);
    }

    private void AddOnToggleEmitListener(Gas gas, bool isEmitting)
    {
        if (isEmitting)
        {
            // Warning System turn on
        }
        else
        {
            // Warning System turn off
        }
    }

    private void OnDestroy()
    {
        
    }

    private void OnDisable()
    {
        gas.RemoveOnToggleEmitListener(AddOnToggleEmitListener);
    }



    private Gas gas = null;
}
