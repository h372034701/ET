/*
 * https://developer.valvesoftware.com/wiki/Dota_2_Workshop_Tools:zh-cn/Scripting:zh-cn/Abilities_Data_Driven:zh-cn#.E6.8A.80.E8.83.BD.E8.8C.83.E4.BE.8B
 */

 
using System;

namespace ET.Ability
{
    /// <summary>
    /// 
    /// 作为UnitAbilityTableComponent的child
    /// </summary>
    public class Ability: Entity
    {
    }

    public sealed class AbilityAttributeModifier: Entity
    {
        public NumericType numericType;
        public int value;
    }

    public sealed class TTAbility: Ability
    {
        public Unit target;
        public long startTime;

        #region 技能事件
        public Action onSpellStart;
        public Action onOwnerDied;
        public Action onOwnerSpawned;
        
        public Action onChannelFinish;
        public Action onChannelInterrupted;
        public Action onChannelSucceeded;

        

        #endregion

        #region 技能操作
        /// <summary>
        /// Target, abilityName
        /// </summary>
        public void AddAbility()
        {
            
        }
        /// <summary>
        /// Target, Action 动作名
        /// </summary>
        public void ActOnTargets()
        {
            
        }
        /// <summary>
        /// Target ModifierName
        /// </summary>
        public void ApplyModifier()
        {
            
        }
        /// <summary>
        /// EffectName, EffectAttachType, Target, ControlPoints, EffectColorA, EffectColorB, EffectAlphaScale
        /// </summary>
        public void AttachEffect()
        {
            
        }
        /// <summary>
        /// Target
        /// </summary>
        public void Blink()
        {
            
        }
        /// <summary>
        /// 分裂攻击
        /// </summary>
        public void CleaveAttack()
        {
            
        }
        /// <summary>
        /// Target, ModifierName
        /// </summary>
        public void CreateThinker()
        {
            
        }
        /// <summary>
        /// Target, Type, MinDamage/MaxDamage, Damage, CurrentHealthPercentBasedDamage, MaxHealthPercentBasedDamage
        /// </summary>
        public void Damage()
        {
            
        }
        /// <summary>
        /// 延迟操作 Delay, Action
        /// </summary>
        public void DelayedAction()
        {
            
        }
        /// <summary>
        /// EffectName, Target
        /// </summary>
        public void FireSound()
        {
            
        }

        public void Heal()
        {
            
        }
        /// <summary>
        /// 击退
        /// Target, Center, Duration, Distance, Height, IsFixedDistance, ShouldStun
        /// 目标，中心，持续时间，距离，高度，固定距离，是否眩晕
        /// </summary>
        public void Knockback()
        {
            
        }
        /// <summary>
        /// 吸血
        /// Target, LifestealPercent
        /// </summary>
        public void Lifesteal()
        {
            
        }

        public void LinearProjecttile()
        {
            
        }
        /// <summary>
        /// Chance, PseudoRandom, OnSuccess, OnFailure
        /// </summary>
        public void Random()
        {
            
        }
        /// <summary>
        /// Target, AbilityName
        /// </summary>
        public void RemoveAbility()
        {
            
        }
        /// <summary>
        /// Target, ModifierName
        /// </summary>
        public void RemoveModifier()
        {
            
        }
        /// <summary>
        /// 	Target, ScriptFile, Function
        /// </summary>
        public void RunScript()
        {
            
        }

        /// <summary>
        /// UnitName, UnitCount, UnitLimit, SpawnRadius, Duration, Target, GrantsGold, GrantsXP
        /// </summary>
        public void SpawnUnit()
        {
            
        }
        /// <summary>
        /// Target, Duration
        /// </summary>
        public void Stun()
        {
            
        }
        /// <summary>
        /// Target, EffectName, Dodgeable, ProvidesVision, VisionRadius, MoveSpeed, SourceAttachment
        /// 	目标，特效名称，是否可闪避，提供视野，视野范围，移动速度，起源附着点
        /// </summary>
        public void TrackingProjectile()
        {
            
        }
        
        #endregion

        public async ETVoid Cast()
        {
            TimerComponent timerComponent = Game.Scene.GetComponent<TimerComponent>();
            var token = new ETCancellationToken();
            this.startTime = TimeHelper.Now();
            Log.Error($"Start {startTime}");
            await this.StartCast(token);
        }

        protected async ETTask StartCast(ETCancellationToken cancellationToken)
        {
            TimerComponent timerComponent = Game.Scene.GetComponent<TimerComponent>();
            // 协程如果取消，将算出玩家的真实位置，赋值给玩家
            bool canceled = false; 
            cancellationToken.Register(() =>
            {
                canceled = true;
                long timeNow = TimeHelper.Now();
                Log.Error($"cancel lation token{timeNow - this.startTime}");
            });

            await timerComponent.WaitAsync(1000, cancellationToken);
            if (canceled)
            {
                long timeNow = TimeHelper.Now();
                Log.Error($"time canceled{timeNow - this.startTime}");
                return;
            }
            Log.Error($"WaitAsync 1000 {TimeHelper.Now() - this.startTime}");
        }

        public void ApplyModifier(Unit unit, string modifierName)
        {
            
        }
    }
}