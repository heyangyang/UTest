using UnityEngine;
using System.IO;
using System.Collections;
using System.Collections.Generic;

public partial class T_MapPve : ByteBase {

	/// <summary>
	/// 关卡ID
	///</summary>
	public int Id;
	/// <summary>
	/// 详细获取
	///</summary>
	public string DropDesc;
	/// <summary>
	/// 关卡名称
	///</summary>
	public string[] Name;
	/// <summary>
	/// 关卡按钮
	///</summary>
	public string Button;
	/// <summary>
	/// 副本类型
	///</summary>
	public int CarbonType;
	/// <summary>
	/// 前置关卡
	///</summary>
	public int[] FrontPVE;
	/// <summary>
	/// 解锁关卡
	///</summary>
	public int[] UnlockPVE;
	/// <summary>
	/// 等级限制
	///</summary>
	public int UnlockLv;
	/// <summary>
	/// 限制时间
	///</summary>
	public int Time;
	/// <summary>
	/// 推荐战力
	///</summary>
	public int Combat;
	/// <summary>
	/// 每天可打次数
	///</summary>
	public int Limit;
	/// <summary>
	/// 每天可扫荡次数
	///</summary>
	public int SweepLimit;
	/// <summary>
	/// 消耗体力
	///</summary>
	public int Energy;
	/// <summary>
	/// 关卡描述
	///</summary>
	public string Dsc;
	/// <summary>
	/// 关卡背景图
	///</summary>
	public string PveBack;
	/// <summary>
	/// 固定阵型
	///</summary>
	public int Formation;
	/// <summary>
	/// 固定英雄
	///</summary>
	public int[] Hero;
	/// <summary>
	/// 固定英雄所在位置
	///</summary>
	public int[] HeroPosition;
	/// <summary>
	/// 固定队长
	///</summary>
	public int Captain;
	/// <summary>
	/// 不可上阵英雄
	///</summary>
	public int[] AntiHero;
	/// <summary>
	/// 可上阵英雄数量（不包括固定）
	///</summary>
	public int HeroQuantity;
	/// <summary>
	/// 不可上阵英雄所在位置
	///</summary>
	public int[] HeroPosition1;
	/// <summary>
	/// 临时物品
	///</summary>
	public int[] TemporaryItem;
	/// <summary>
	/// 怪物组
	///</summary>
	public int[] MonsterGroup;
	/// <summary>
	/// 胜利条件1
	///</summary>
	public int[] Victory1;
	/// <summary>
	/// 胜利条件2
	///</summary>
	public int[] Victory2;
	/// <summary>
	/// 失败条件1
	///</summary>
	public int[] Fail1;
	/// <summary>
	/// 失败条件2
	///</summary>
	public int[] Fail2;
	/// <summary>
	/// 挑战目标条件1
	///</summary>
	public int[] Star1;
	/// <summary>
	/// 文字描述1
	///</summary>
	public string Description1;
	/// <summary>
	/// 挑战目标条件2
	///</summary>
	public int[] Star2;
	/// <summary>
	/// 文字描述2
	///</summary>
	public string Description2;
	/// <summary>
	/// 挑战目标条件3
	///</summary>
	public int[] Star3;
	/// <summary>
	/// 文字描述3
	///</summary>
	public string Description3;
	/// <summary>
	/// 奖励金币
	///</summary>
	public int GetGold;
	/// <summary>
	/// 奖励战队经验
	///</summary>
	public int GetTeamExp;
	/// <summary>
	/// 奖励角色经验
	///</summary>
	public int GetExp;
	/// <summary>
	/// 一般通关奖励
	///</summary>
	public int[] NormalGetItem;
	/// <summary>
	/// 首次通关奖励
	///</summary>
	public int[] FristGetItem;
	/// <summary>
	/// 首次三星通关奖励
	///</summary>
	public int[] ThreeStarGetItem;
	/// <summary>
	/// 时间校验
	///</summary>
	public int TestTime;
	/// <summary>
	/// 战斗力校验
	///</summary>
	public int TestBattle;
	/// <summary>
	/// 战斗气泡
	///</summary>
	public int BattleBubble;

	public override void Read(){
		Id=ReadInt();
		DropDesc=ReadString();
		Name=ReadArrayStr();
		Button=ReadString();
		CarbonType=ReadInt();
		FrontPVE=ReadArrayInt();
		UnlockPVE=ReadArrayInt();
		UnlockLv=ReadInt();
		Time=ReadInt();
		Combat=ReadInt();
		Limit=ReadInt();
		SweepLimit=ReadInt();
		Energy=ReadInt();
		Dsc=ReadString();
		PveBack=ReadString();
		Formation=ReadInt();
		Hero=ReadArrayInt();
		HeroPosition=ReadArrayInt();
		Captain=ReadInt();
		AntiHero=ReadArrayInt();
		HeroQuantity=ReadInt();
		HeroPosition1=ReadArrayInt();
		TemporaryItem=ReadArrayInt();
		MonsterGroup=ReadArrayInt();
		Victory1=ReadArrayInt();
		Victory2=ReadArrayInt();
		Fail1=ReadArrayInt();
		Fail2=ReadArrayInt();
		Star1=ReadArrayInt();
		Description1=ReadString();
		Star2=ReadArrayInt();
		Description2=ReadString();
		Star3=ReadArrayInt();
		Description3=ReadString();
		GetGold=ReadInt();
		GetTeamExp=ReadInt();
		GetExp=ReadInt();
		NormalGetItem=ReadArrayInt();
		FristGetItem=ReadArrayInt();
		ThreeStarGetItem=ReadArrayInt();
		TestTime=ReadInt();
		TestBattle=ReadInt();
		BattleBubble=ReadInt();

	}
	
	public override void Write (){
		WriteInt(Id);
		WriteString(DropDesc);
		WriteArrayStr(Name);
		WriteString(Button);
		WriteInt(CarbonType);
		WriteArrayInt(FrontPVE);
		WriteArrayInt(UnlockPVE);
		WriteInt(UnlockLv);
		WriteInt(Time);
		WriteInt(Combat);
		WriteInt(Limit);
		WriteInt(SweepLimit);
		WriteInt(Energy);
		WriteString(Dsc);
		WriteString(PveBack);
		WriteInt(Formation);
		WriteArrayInt(Hero);
		WriteArrayInt(HeroPosition);
		WriteInt(Captain);
		WriteArrayInt(AntiHero);
		WriteInt(HeroQuantity);
		WriteArrayInt(HeroPosition1);
		WriteArrayInt(TemporaryItem);
		WriteArrayInt(MonsterGroup);
		WriteArrayInt(Victory1);
		WriteArrayInt(Victory2);
		WriteArrayInt(Fail1);
		WriteArrayInt(Fail2);
		WriteArrayInt(Star1);
		WriteString(Description1);
		WriteArrayInt(Star2);
		WriteString(Description2);
		WriteArrayInt(Star3);
		WriteString(Description3);
		WriteInt(GetGold);
		WriteInt(GetTeamExp);
		WriteInt(GetExp);
		WriteArrayInt(NormalGetItem);
		WriteArrayInt(FristGetItem);
		WriteArrayInt(ThreeStarGetItem);
		WriteInt(TestTime);
		WriteInt(TestBattle);
		WriteInt(BattleBubble);

	}

	public System.Object Clone()  
	{
		return this.MemberwiseClone();
	}
}

public partial class T_MapPveMgr{
	public Dictionary<int,T_MapPve> itemData = new Dictionary<int, T_MapPve>();
	//public Dictionary<int,List<T_MapPve>> groupData = new Dictionary<int,List<T_MapPve>>();
	public int size;

	public T_MapPveMgr(){
		//ReadConfig ();
	}

	public IEnumerator ReadConfig(){
		string path = PathUtil.StreamingPath("Config/T_MapPve.bytes");
		WWW data = new WWW (path);
		yield return data;
		MemoryStream stream = new MemoryStream (data.bytes);
		stream.Position = 0;
		
		T_MapPve byteObj = new T_MapPve ();
		byteObj.SetStream (stream);
		size = byteObj.ReadInt ();
		
		for (int i=0; i<size; i++) {
			T_MapPve item = new T_MapPve();
			item.Deserialization(stream);
			
	//		if(!groupData.ContainsKey()){
	//			groupData[] = new List<T_MapPve>();
	//		}
	//		groupData[].Add(item);

	      if(itemData.ContainsKey(item.Id))Loger.Error("T_MapPve is Repeat KEY = "+item.Id);
	      itemData[item.Id]=item;
		}

		Loger.Info ("T_MapPve Config load Complete, size:"+size);
	}

	public T_MapPve GetData(int Id){
		int id=Id;
		if (itemData.ContainsKey (id)) {
			return itemData[id];
		}
  		//WorldMgr.ShowMessageLog("T_MapPve not find >> id:"+id);
		Loger.Error ("T_MapPve not find >> id:"+id);
		return null;
	}
	
	//public List<T_MapPve> GetGroup(){
	//	
	//	if (groupData.ContainsKey (id)) {
	//		return groupData[id];
	//	}
  	//	//WorldMgr.ShowMessageLog("T_MapPve not find >> id:"+id);
	//	Loger.Error ("T_MapPve not find Group >> id:"+id);
	//	return null;
	//}

	public bool HasData(int Id){
		int id=Id;
		return itemData.ContainsKey (id);
	}
	
	//public bool HasList(){
	//	
	//	return groupData.ContainsKey (id);
	//}
}
