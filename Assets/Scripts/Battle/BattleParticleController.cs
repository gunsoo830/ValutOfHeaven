using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleParticleController : ParticleController
{
    private BattleEffectPool _effectPool;
    // Start is called before the first frame update
    protected void Start()
    {
        base.Start();
        this._effectPool = GameObject.FindAnyObjectByType<BattleEffectPool>();
    }

    protected override void _onParticleFinish()
    {
        base._onParticleFinish();
        this._effectPool.returnPoolObject(0, this.gameObject);
    }
}
