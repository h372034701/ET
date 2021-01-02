# ET Client 启动流程

###1. open unity scene

###2. Init.Cs

1. SynchronizationContext.SetSynchronizationContext(OneThreadSynchronizationContext.Instance);
2. 遍历Assembly 注册到Game.EventSystem
3. EventSystem.Publish Event.AppStart
4. 

###3. AppStart_Init.Cs

1. Game.Scene.AddComponent<TimerComponent>();  ResourcesComponent ConfigComponent 
2. MessageDispatcherComponent  UIEventComponent
3. 创建zoneScene NetOuterComponent UnitComponent
4. OpcodeTypeComponent> 注册 protobuf opcode 
5. Game.EventSystem.Publish(new EventType.AppStartInitFinish())

###4. UILoginComponentAwakeSystem.Cs
1. 创建LoginUI
2. login button callback LoginHelper.Login

###5. 重点  LoginHelper.Cs
1. R2C_Login 请求Gate相关的数据
2. 创建gateSession
    ```c#
    Session gateSession = zoneScene.GetComponent<NetOuterComponent>().Create(r2CLogin.Address);
    zoneScene.AddComponent<SessionComponent>().Session = gateSession;
    ```
3. G2C_LoginGate 登录gate   gate会创建player并返回playerId  Server C2G_LoginGate.Cs

###6. LoginFinish_CreateLobbyUI.Cs  创建大厅
1. 创建大厅
2. UILobbyComponentSystem.EnterMap C2G_EnterMap 发送进入map消息  并返回和自己的unitId unit list
