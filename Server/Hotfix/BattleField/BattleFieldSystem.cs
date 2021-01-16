using NLog;

namespace ET
{
    public class BattleFieldAwakeSystem: AwakeSystem<BattleField>
    {
        public override void Awake(BattleField self)
        {
            Log.Error("Battle Field Awake");
        }
    }
    
    
    public class BattleFieldSystem: UpdateSystem<BattleField>
    {
        public override void Update(BattleField self)
        {
            self.current_time += 1;
            if (self.current_time % 30 == 0)
            {
                // Log.Error($"hello battle field");
            }
        }
    }
}