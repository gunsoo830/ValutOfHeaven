using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleEffectPool : ObjectPool
{
    protected override GameObject _createObject(int targetIndex)
    {
        var retVal = base._createObject(targetIndex);
        retVal.AddComponent<BattleParticleController>();
        return retVal;
    }
}
