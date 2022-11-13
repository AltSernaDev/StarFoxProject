using UnityEngine;

public abstract class BossBaseState
{
    public abstract void EnterState(BossManager b);
    public abstract void UpdateState(BossManager b);
}
