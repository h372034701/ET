using System.Collections.Generic;
using ET;
        
namespace ET.Ability
{
    /// <summary>
    /// unit 拥有的技能列表  unit的技能系统的根
    /// 1. 添加/删除技能
    /// 2. Activate/Deactiveate ability
    /// 3. 修改技能参数  等级、添加技能修改器(例如 斗战神的精修, dota2的天赋树会修改技能)
    /// 4. 释放技能
    /// </summary>
    public sealed class UnitAbilityTableComponent: Entity
    {
        /// <summary>
        /// 添加一个ability
        /// param 技能的等级 修改器等参数
        /// </summary>
        public void AddAbiblity(int id)
        {
            // TODO: 数据库中Load技能参数
            
            TTAbility ability = EntityFactory.CreateWithParent<TTAbility>(this);
            NumericComponent numericComponent = ability.AddComponent<NumericComponent>();
            AbilityModifierComponent modifyComponent = ability.AddComponent<AbilityModifierComponent>();
            ability.Cast();
            // TODO: init 设置技能等级  修改器参数

            //TODO: 调用添加技能回调

        }
        /// <summary>
        /// 移除技能
        /// </summary>
        /// <param name="id"></param>
        public void RemoveAbility(int id)
        {
            foreach (KeyValuePair<long, Entity> kv in this.Children)
            {
                Ability ability = kv.Value as Ability;
                // if ability.id
                // TODO: 判断id 进行移除
                // 调用remove回调
            }
        }
        /// <summary>
        /// 释放技能
        /// todo: 需要整理技能释放流程
        /// </summary>
        /// <param name="id"></param>
        public void CastAbility(int id)
        {
            
        }
        
        /// <summary>
        /// 添加技能修改器
        /// </summary>
        /// <param name="id"></param>
        /// <param name="modifyId"></param>
        public void AddAbilityAttributeModifier(int id, int modifyId)
        {
            
        }
        
        /// <summary>
        /// 移除技能修改器
        /// </summary>
        /// <param name="id"></param>
        /// <param name="modifyId"></param>
        public void RemoveAbilityAttributeModifier(int id, int modifyId)
        {
            
        }
    }
    
}