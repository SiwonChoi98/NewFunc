using UnityEngine;

public static class Utill
{
    public static int GetTargetLayerTypeToLayerNumber(TargetLayerType targetLayerType)
    {
        int targetLayerNumber = -1;
        switch (targetLayerType)
        {
            case TargetLayerType.Enemy:
                targetLayerNumber = LayerMask.NameToLayer("Enemy");
                break;
            case TargetLayerType.Hero:
                targetLayerNumber = LayerMask.NameToLayer("Hero");
                break;
        }

        return targetLayerNumber;
    }
}
