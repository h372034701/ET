namespace ET.Ability
{
    public delegate void OnChannelFinish();
    public delegate void OnChannelInterrupted();
    public delegate void OnChannelSucceeded();
    public delegate void OnOwnerDied();
    public delegate void OnOwnerSpawned();
    public delegate void OnProjectileFinish();
    public delegate void OnProjectileHitUnit();
    public delegate void OnSpellStart();
    public delegate void OnToggleOff();
    public delegate void OnToggleOn();
    public delegate void OnUpgrade();
    
    /// <summary>
    /// 技能instance的事件组件
    /// </summary>
    public sealed class AbilityEventComponent: Entity
    {
        
    }
}