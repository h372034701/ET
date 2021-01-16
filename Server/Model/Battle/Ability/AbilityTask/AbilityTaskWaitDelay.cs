using System;

namespace ET.Ability
{
    public class AbilityTaskWaitDelay: Entity
    {
        public long timeStarted;
        public long time;
        public Action onTimeFinish;
        public Action onExternalCancel;
        
        public bool IsCompleted(float t)
        {
            return t > this.timeStarted + this.time;
        }

        public async ETTask Activate(long time, ETCancellationToken cancellationToken)
        {
            this.time = time;
            TimerComponent timerComponent = Game.Scene.GetComponent<TimerComponent>();
            // 协程如果取消，将算出玩家的真实位置，赋值给玩家
            cancellationToken.Register(() =>
            {
                
            });
            bool isOutTime = await timerComponent.WaitAsync(this.time, cancellationToken);
            if (!isOutTime)
            {
                this.onTimeFinish?.Invoke();
            }
            else
            {
                onExternalCancel?.Invoke();
            }
        }
        
    }

    public static class AbilityTaskWaitDelaySystem
    {
        
    }
}