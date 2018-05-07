using UnityEngine;
using System.IO;
using System.Collections;
using System.Collections.Generic;

public partial class T_Actor : ByteBase {

	/// <summary>
	/// 角色ID
	///</summary>
	public int Id;
	/// <summary>
	/// 角色名
	///</summary>
	public string Name;
	/// <summary>
	/// 属相参数
	///</summary>
	public int Constellation;
	/// <summary>
	/// 角色资源ID
	///</summary>
	public int ResourceId;
	/// <summary>
	/// 描述
	///</summary>
	public string Desc;
	/// <summary>
	/// 登场对白
	///</summary>
	public string ActorSentence;
	/// <summary>
	/// 等级
	///</summary>
	public int Lv;
	/// <summary>
	/// 星级
	///</summary>
	public int Star;
	/// <summary>
	/// 品质
	///</summary>
	public int Quality;
	/// <summary>
	/// 品阶
	///</summary>
	public int Class;
	/// <summary>
	/// 品级
	///</summary>
	public int ClassLv;
	/// <summary>
	/// 强化ID
	///</summary>
	public int Intensify;
	/// <summary>
	/// 职业
	///</summary>
	public int Profession;
	/// <summary>
	/// 角色抗性率ID
	///</summary>
	public int ActorHurtID;
	/// <summary>
	/// 角色类型
	///</summary>
	public int Type;
	/// <summary>
	/// 产生碎片
	///</summary>
	public int[] Fragment;
	/// <summary>
	/// 产生残页
	///</summary>
	public int ResidualPage;
	/// <summary>
	/// 碎片需求
	///</summary>
	public int[] Pieces;
	/// <summary>
	/// AI
	///</summary>
	public int Aiid;
	/// <summary>
	/// 队长技能
	///</summary>
	public int Leaderskill;
	/// <summary>
	/// 掉落ID
	///</summary>
	public int Dropid;
	/// <summary>
	/// 重量（kg）
	///</summary>
	public int Weight;
	/// <summary>
	/// 重心高度（cm）
	///</summary>
	public int Weighthight;
	/// <summary>
	/// 破天碰撞高度（cm）
	///</summary>
	public int[] Hight;
	/// <summary>
	/// 损耗天空或地面耐久值
	///</summary>
	public int[] LossDurable;
	/// <summary>
	/// 慢放参数
	///</summary>
	public int[] DieSlow;
	/// <summary>
	/// 生命值（资质）
	///</summary>
	public int[] Hpability;
	/// <summary>
	/// 物理攻击（资质）
	///</summary>
	public int[] Phyatkability;
	/// <summary>
	/// 护甲（资质）
	///</summary>
	public int[] Phydefability;
	/// <summary>
	/// 法术攻击（资质）
	///</summary>
	public int[] Magatkability;
	/// <summary>
	/// 抗性（资质）
	///</summary>
	public int[] Magdefability;
	/// <summary>
	/// 防御值（资质）
	///</summary>
	public int[] Fulldefability;
	/// <summary>
	/// 破防值（资质）
	///</summary>
	public int[] Breakdefability;
	/// <summary>
	/// 生命值
	///</summary>
	public int Hp;
	/// <summary>
	/// 物理攻击
	///</summary>
	public int Phyatk;
	/// <summary>
	/// 护甲
	///</summary>
	public int Phydef;
	/// <summary>
	/// 法术攻击
	///</summary>
	public int Magatk;
	/// <summary>
	/// 抗性
	///</summary>
	public int Magdef;
	/// <summary>
	/// 武器物理攻击下限
	///</summary>
	public int Weaponphyatkmin;
	/// <summary>
	/// 武器物理攻击上限
	///</summary>
	public int Weaponphyatkmax;
	/// <summary>
	/// 武器法术攻击下限
	///</summary>
	public int Weaponmagatkmin;
	/// <summary>
	/// 武器法术攻击上限
	///</summary>
	public int Weaponmagatkmax;
	/// <summary>
	/// 防御值
	///</summary>
	public int Fulldef;
	/// <summary>
	/// 破防值
	///</summary>
	public int Breakdef;
	/// <summary>
	/// 防御出现率
	///</summary>
	public float Defappear;
	/// <summary>
	/// 物理伤害加成率
	///</summary>
	public float Addphyhurt;
	/// <summary>
	/// 物理伤害减免率
	///</summary>
	public float Decphyhurt;
	/// <summary>
	/// 抗石化率
	///</summary>
	public float Petrifaction;
	/// <summary>
	/// 击晕率
	///</summary>
	public float Stun;
	/// <summary>
	/// 防晕率
	///</summary>
	public float Prestun;
	/// <summary>
	/// 回避率
	///</summary>
	public float Dodge;
	/// <summary>
	/// 命中率
	///</summary>
	public float Hit;
	/// <summary>
	/// 暴击率
	///</summary>
	public float Crit;
	/// <summary>
	/// 抗暴率
	///</summary>
	public float Precrit;
	/// <summary>
	/// 暴击伤害加成率
	///</summary>
	public float Addcrithurt;
	/// <summary>
	/// 暴击伤害减免率
	///</summary>
	public float Deccrithurt;
	/// <summary>
	/// 法术伤害加成率
	///</summary>
	public float Addmaghurt;
	/// <summary>
	/// 法术伤害减免率
	///</summary>
	public float Decmaghurt;
	/// <summary>
	/// 元素抗性率（风）
	///</summary>
	public float Windres;
	/// <summary>
	/// 元素抗性率（火）
	///</summary>
	public float Fireres;
	/// <summary>
	/// 元素抗性率（雷）
	///</summary>
	public float Thunderres;
	/// <summary>
	/// 元素抗性率（冰）
	///</summary>
	public float Iceres;
	/// <summary>
	/// 元素加强率（风）
	///</summary>
	public float Wind;
	/// <summary>
	/// 元素加强率（火）
	///</summary>
	public float Fire;
	/// <summary>
	/// 元素加强率（雷）
	///</summary>
	public float Thunder;
	/// <summary>
	/// 元素加强率（冰）
	///</summary>
	public float Ice;
	/// <summary>
	/// 该状态被击触发掉落CT珠子
	///</summary>
	public int TriggerStateCT;
	/// <summary>
	/// CT珠子每次掉落概率|数量|获得CT值
	///</summary>
	public int[] CTcontent;
	/// <summary>
	/// 掉落CT资源ID
	///</summary>
	public int CTResources;
	/// <summary>
	/// 该状态下概率掉落EN珠子
	///</summary>
	public int TriggerStateEn;
	/// <summary>
	/// EN珠子每次掉落概率|获得en值
	///</summary>
	public int[] ENcontent;
	/// <summary>
	/// 掉落EN资源ID
	///</summary>
	public int EnResources;
	/// <summary>
	/// 该状态被击触发掉落经验珠子
	///</summary>
	public int EXPtriggerState;
	/// <summary>
	/// 经验珠子每次掉落概率|数量|获得经验值
	///</summary>
	public int[] EXPexpcontent;
	/// <summary>
	/// 掉落经验资源ID
	///</summary>
	public int EXPResources;
	/// <summary>
	/// 性别
	///</summary>
	public int Gentle;
	/// <summary>
	/// 探索成功率
	///</summary>
	public int ExploreSuccess;
	/// <summary>
	/// 探索技能
	///</summary>
	public int[] ExploreSkill;
	/// <summary>
	/// 旋转角度
	///</summary>
	public int Rotation;

	public override void Read(){
		Id=ReadInt();
		Name=ReadString();
		Constellation=ReadInt();
		ResourceId=ReadInt();
		Desc=ReadString();
		ActorSentence=ReadString();
		Lv=ReadInt();
		Star=ReadInt();
		Quality=ReadInt();
		Class=ReadInt();
		ClassLv=ReadInt();
		Intensify=ReadInt();
		Profession=ReadInt();
		ActorHurtID=ReadInt();
		Type=ReadInt();
		Fragment=ReadArrayInt();
		ResidualPage=ReadInt();
		Pieces=ReadArrayInt();
		Aiid=ReadInt();
		Leaderskill=ReadInt();
		Dropid=ReadInt();
		Weight=ReadInt();
		Weighthight=ReadInt();
		Hight=ReadArrayInt();
		LossDurable=ReadArrayInt();
		DieSlow=ReadArrayInt();
		Hpability=ReadArrayInt();
		Phyatkability=ReadArrayInt();
		Phydefability=ReadArrayInt();
		Magatkability=ReadArrayInt();
		Magdefability=ReadArrayInt();
		Fulldefability=ReadArrayInt();
		Breakdefability=ReadArrayInt();
		Hp=ReadInt();
		Phyatk=ReadInt();
		Phydef=ReadInt();
		Magatk=ReadInt();
		Magdef=ReadInt();
		Weaponphyatkmin=ReadInt();
		Weaponphyatkmax=ReadInt();
		Weaponmagatkmin=ReadInt();
		Weaponmagatkmax=ReadInt();
		Fulldef=ReadInt();
		Breakdef=ReadInt();
		Defappear=ReadFloat();
		Addphyhurt=ReadFloat();
		Decphyhurt=ReadFloat();
		Petrifaction=ReadFloat();
		Stun=ReadFloat();
		Prestun=ReadFloat();
		Dodge=ReadFloat();
		Hit=ReadFloat();
		Crit=ReadFloat();
		Precrit=ReadFloat();
		Addcrithurt=ReadFloat();
		Deccrithurt=ReadFloat();
		Addmaghurt=ReadFloat();
		Decmaghurt=ReadFloat();
		Windres=ReadFloat();
		Fireres=ReadFloat();
		Thunderres=ReadFloat();
		Iceres=ReadFloat();
		Wind=ReadFloat();
		Fire=ReadFloat();
		Thunder=ReadFloat();
		Ice=ReadFloat();
		TriggerStateCT=ReadInt();
		CTcontent=ReadArrayInt();
		CTResources=ReadInt();
		TriggerStateEn=ReadInt();
		ENcontent=ReadArrayInt();
		EnResources=ReadInt();
		EXPtriggerState=ReadInt();
		EXPexpcontent=ReadArrayInt();
		EXPResources=ReadInt();
		Gentle=ReadInt();
		ExploreSuccess=ReadInt();
		ExploreSkill=ReadArrayInt();
		Rotation=ReadInt();

	}
	
	public override void Write (){
		WriteInt(Id);
		WriteString(Name);
		WriteInt(Constellation);
		WriteInt(ResourceId);
		WriteString(Desc);
		WriteString(ActorSentence);
		WriteInt(Lv);
		WriteInt(Star);
		WriteInt(Quality);
		WriteInt(Class);
		WriteInt(ClassLv);
		WriteInt(Intensify);
		WriteInt(Profession);
		WriteInt(ActorHurtID);
		WriteInt(Type);
		WriteArrayInt(Fragment);
		WriteInt(ResidualPage);
		WriteArrayInt(Pieces);
		WriteInt(Aiid);
		WriteInt(Leaderskill);
		WriteInt(Dropid);
		WriteInt(Weight);
		WriteInt(Weighthight);
		WriteArrayInt(Hight);
		WriteArrayInt(LossDurable);
		WriteArrayInt(DieSlow);
		WriteArrayInt(Hpability);
		WriteArrayInt(Phyatkability);
		WriteArrayInt(Phydefability);
		WriteArrayInt(Magatkability);
		WriteArrayInt(Magdefability);
		WriteArrayInt(Fulldefability);
		WriteArrayInt(Breakdefability);
		WriteInt(Hp);
		WriteInt(Phyatk);
		WriteInt(Phydef);
		WriteInt(Magatk);
		WriteInt(Magdef);
		WriteInt(Weaponphyatkmin);
		WriteInt(Weaponphyatkmax);
		WriteInt(Weaponmagatkmin);
		WriteInt(Weaponmagatkmax);
		WriteInt(Fulldef);
		WriteInt(Breakdef);
		WriteFloat(Defappear);
		WriteFloat(Addphyhurt);
		WriteFloat(Decphyhurt);
		WriteFloat(Petrifaction);
		WriteFloat(Stun);
		WriteFloat(Prestun);
		WriteFloat(Dodge);
		WriteFloat(Hit);
		WriteFloat(Crit);
		WriteFloat(Precrit);
		WriteFloat(Addcrithurt);
		WriteFloat(Deccrithurt);
		WriteFloat(Addmaghurt);
		WriteFloat(Decmaghurt);
		WriteFloat(Windres);
		WriteFloat(Fireres);
		WriteFloat(Thunderres);
		WriteFloat(Iceres);
		WriteFloat(Wind);
		WriteFloat(Fire);
		WriteFloat(Thunder);
		WriteFloat(Ice);
		WriteInt(TriggerStateCT);
		WriteArrayInt(CTcontent);
		WriteInt(CTResources);
		WriteInt(TriggerStateEn);
		WriteArrayInt(ENcontent);
		WriteInt(EnResources);
		WriteInt(EXPtriggerState);
		WriteArrayInt(EXPexpcontent);
		WriteInt(EXPResources);
		WriteInt(Gentle);
		WriteInt(ExploreSuccess);
		WriteArrayInt(ExploreSkill);
		WriteInt(Rotation);

	}

	public System.Object Clone()  
	{
		return this.MemberwiseClone();
	}
}

public partial class T_ActorMgr{
	public Dictionary<int,T_Actor> itemData = new Dictionary<int, T_Actor>();
	//public Dictionary<int,List<T_Actor>> groupData = new Dictionary<int,List<T_Actor>>();
	public int size;

	public T_ActorMgr(){
		//ReadConfig ();
	}

	public IEnumerator ReadConfig(){
		string path = PathUtil.StreamingPath("Config/T_Actor.bytes");
        Loger.Info(path);
		WWW data = new WWW (path);
		yield return data;
		MemoryStream stream = new MemoryStream (data.bytes);
		stream.Position = 0;
		
		T_Actor byteObj = new T_Actor ();
		byteObj.SetStream (stream);
		size = byteObj.ReadInt ();
		
		for (int i=0; i<size; i++) {
			T_Actor item = new T_Actor();
			item.Deserialization(stream);
			
	//		if(!groupData.ContainsKey()){
	//			groupData[] = new List<T_Actor>();
	//		}
	//		groupData[].Add(item);

	      if(itemData.ContainsKey(item.Id))Loger.Error("T_Actor is Repeat KEY = "+item.Id);
	      itemData[item.Id]=item;
		}

		Loger.Info ("T_Actor Config load Complete, size:"+size);
	}

	public T_Actor GetData(int Id){
		int id=Id;
		if (itemData.ContainsKey (id)) {
			return itemData[id];
		}
  		//WorldMgr.ShowMessageLog("T_Actor not find >> id:"+id);
		Loger.Error ("T_Actor not find >> id:"+id);
		return null;
	}
	
	//public List<T_Actor> GetGroup(){
	//	
	//	if (groupData.ContainsKey (id)) {
	//		return groupData[id];
	//	}
  	//	//WorldMgr.ShowMessageLog("T_Actor not find >> id:"+id);
	//	Loger.Error ("T_Actor not find Group >> id:"+id);
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
