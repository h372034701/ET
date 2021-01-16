using System;
using ET.Ability;

namespace ET
{
	[ActorMessageHandler]
	public class G2M_CreateBattleFieldHandler : AMActorRpcHandler<Scene, G2M_CreateBattleField, M2G_CreateBattleField>
	{
		protected override async ETTask Run(Scene scene, G2M_CreateBattleField request, M2G_CreateBattleField response, Action reply)
		{
			var battleFieldsComponent = scene.GetComponent<BattleFieldsComponent>();
			BattleField battleField = EntityFactory.CreateWithId<BattleField>(scene, IdGenerater.GenerateId());
			battleField.Parent = battleFieldsComponent;
			battleField.AddComponent<MailBoxComponent>();
			await battleField.AddLocation();
			var unitComponent = battleField.AddComponent<UnitComponent>();
			battleField.AddComponent<UnitGateComponent, long>(request.GateSessionId);
			
			Unit unit = EntityFactory.CreateWithId<Unit>(scene, IdGenerater.GenerateId());
			var numeric = unit.AddComponent<NumericComponent>();
			numeric.Set(NumericType.MaxHp, 100);
			numeric.Set(NumericType.Hp, 100);
			numeric.Set(NumericType.Speed, 1);
			Log.Error($"{numeric[NumericType.MaxHp]}");
			unitComponent.Add(unit);
			var abilityTable = unit.AddComponent<UnitAbilityTableComponent>();
			abilityTable.AddAbiblity(0);
			
			// response.UnitId = battleField.Id;
			
			// 广播创建的unit
			// M2C_CreateUnits createUnits = new M2C_CreateUnits();
			// Unit[] units = scene.GetComponent<UnitComponent>().GetAll();
			// foreach (Unit u in units)
			// {
			// 	UnitInfo unitInfo = new UnitInfo();
			// 	unitInfo.X = u.Position.x;
			// 	unitInfo.Y = u.Position.y;
			// 	unitInfo.Z = u.Position.z;
			// 	unitInfo.UnitId = u.Id;
			// 	createUnits.Units.Add(unitInfo);
			// }
			// MessageHelper.Broadcast(battleField, createUnits);
			
			reply();
		}
	}
}