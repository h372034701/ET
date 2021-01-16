## Server结构

#### Server.Program.cs

1. 启动时会根据传进的参数决定加载哪些Component，这就决定了这个server的功能， 比如，
   ```c# 
   // 旧版启动代码
    case AppType.Gate:
        Game.Scene.AddComponent<PlayerComponent>();
        Game.Scene.AddComponent<ActorMessageDispatherComponent>();
        Game.Scene.AddComponent<NetInnerComponent, string>(innerConfig.Address);
        Game.Scene.AddComponent<NetOuterComponent, string>(outerConfig.Address);
        Game.Scene.AddComponent<LocationProxyComponent>();
        Game.Scene.AddComponent<ActorMessageSenderComponent>();
        Game.Scene.AddComponent<ActorLocationSenderComponent>();
        Game.Scene.AddComponent<GateSessionKeyComponent>();
    case AppType.Location:
        Game.Scene.AddComponent<NetInnerComponent, string>(innerConfig.Address);
        Game.Scene.AddComponent<LocationComponent>();
        break;
    case AppType.Map:
        Game.Scene.AddComponent<NetInnerComponent, string>(innerConfig.Address);
        Game.Scene.AddComponent<UnitComponent>();
        Game.Scene.AddComponent<LocationProxyComponent>();
        Game.Scene.AddComponent<ActorMessageSenderComponent>();
        Game.Scene.AddComponent<ActorLocationSenderComponent>();
        Game.Scene.AddComponent<ActorMessageDispatherComponent>();
        Game.Scene.AddComponent<PathfindingComponent>();
        break;
    ```

#### MapServer的主要功能

即场景服务器，下面摘自https://www.gameres.com/799041.html
> 场景服务器：它负责完成主要的游戏逻辑，这些逻辑包括：
> 角色在游戏场景中的进入与退出、角色的行走与跑动、角色战斗（包括打怪）、任务的认领等。场景服务
> 器设计的好坏是整个游戏世界服务器性能差异的主要体现，它的设计难度不仅仅
> 在于通信模型方面，更主要的是整个服务器的体系架构和同步机制的设计。

#### GateServer的主要功能
如上
> 网关服务器: 在类型一种的架构中，玩家在多个地图跳转或者场景切换的时候
> 采用跳转的模式，以此进行跳转不同的服务器。还有一种方式是把这些服务器
> 的节点都通过网关服务器管理，玩家和网关服务器交互，每个场景或者服务器
> 切换的时候，也有网关服务器统一来交换数据，如此玩家操作会比较流畅。

#### ET的Player登录流程
1. client 向gate发送C2R_Login消息请求gateId key address
   ```c#
      // Server LoginHandler.cs
      // 随机分配一个Gate
      StartSceneConfig config = RealmGateAddressHelper.GetGate(session.DomainZone());
      //Log.Debug($"gate address: {MongoHelper.ToJson(config)}");
      
      // 向gate请求一个key,客户端可以拿着这个key连接gate
      G2R_GetLoginKey g2RGetLoginKey = (G2R_GetLoginKey) await ActorMessageSenderComponent.Instance.Call(
          config.SceneId, new R2G_GetLoginKey() {Account = request.Account});
   
      response.Address = config.OuterAddress;
      response.Key = g2RGetLoginKey.Key;
      response.GateId = g2RGetLoginKey.GateId;
   ```
   
2. client通过上面的参数创建geteSession,然后请求登录gate
   ```c#
       // Unity LoginHelper.cs
       // 创建一个gate Session,并且保存到SessionComponent中
       Session gateSession = zoneScene.GetComponent<NetOuterComponent>().Create(r2CLogin.Address);
       zoneScene.AddComponent<SessionComponent>().Session = gateSession;
       
       G2C_LoginGate g2CLoginGate = (G2C_LoginGate)await gateSession.Call(
           new C2G_LoginGate() { Key = r2CLogin.Key, GateId = r2CLogin.GateId});
   ```

3. server验证过key之后，创建player 
   ```c#
       //
       Player player = EntityFactory.Create<Player, string>(Game.Scene, account);
       scene.GetComponent<PlayerComponent>().Add(player);
       session.AddComponent<SessionPlayerComponent>().Player = player;
       session.AddComponent<MailBoxComponent, MailboxType>(MailboxType.GateSession);
   ```