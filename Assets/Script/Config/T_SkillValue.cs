using UnityEngine;
using System.IO;
using System.Collections;
using System.Collections.Generic;

public partial class T_SkillValue : ByteBase {

	/// <summary>
	/// Id
	///</summary>
	public int Id;
	/// <summary>
	/// 动作名
	///</summary>
	public string Name;
	/// <summary>
	/// 增加EN
	///</summary>
	public int AddEN;
	/// <summary>
	/// 增加连击数
	///</summary>
	public int AddHits;
	/// <summary>
	/// 增加CT
	///</summary>
	public int AddCT;
	/// <summary>
	/// 扣除ap
	///</summary>
	public int DelAP;
	/// <summary>
	/// 垂直能量
	///</summary>
	public int VerPower;
	/// <summary>
	/// 水平位移
	///</summary>
	public int[] HorPower;
	/// <summary>
	/// 技能在战斗可用次数
	///</summary>
	public int UseLimit;
	/// <summary>
	/// 冷却回合数
	///</summary>
	public int MagicCd;
	/// <summary>
	/// 物理攻击加成系数
	///</summary>
	public float PhyAtkPer;
	/// <summary>
	/// 法术攻击加成系数
	///</summary>
	public float MagAtkPer;
	/// <summary>
	/// 物理伤害系数
	///</summary>
	public float PhyHurtPer;
	/// <summary>
	/// 风元素伤害系数
	///</summary>
	public float WindHurtPer;
	/// <summary>
	/// 火元素伤害系数
	///</summary>
	public float FireHurtPer;
	/// <summary>
	/// 雷元素伤害系数
	///</summary>
	public float ThunderHurtPer;
	/// <summary>
	/// 冰元素伤害系数
	///</summary>
	public float IceHurtPer;
	/// <summary>
	/// 百分比伤害类型
	///</summary>
	public int PerHurtType;
	/// <summary>
	/// 百分比伤害系数
	///</summary>
	public float PerHurt;
	/// <summary>
	/// 回血系数
	///</summary>
	public float RecPer;
	/// <summary>
	/// 百分比回血类型
	///</summary>
	public int PerRecType;
	/// <summary>
	/// 百分比回血系数
	///</summary>
	public float PerRec;
	/// <summary>
	/// 技能暴击率
	///</summary>
	public float SkillCrit;
	/// <summary>
	/// 技能暴击加成率
	///</summary>
	public float SkillCritAdd;
	/// <summary>
	/// 物理吸血系数
	///</summary>
	public float PhyVamPer;
	/// <summary>
	/// 风元素法术吸血系数
	///</summary>
	public float WindVamPer;
	/// <summary>
	/// 火元素法术吸血系数
	///</summary>
	public float FireVamPer;
	/// <summary>
	/// 雷元素法术吸血系数
	///</summary>
	public float ThunderVamPer;
	/// <summary>
	/// 冰元素法术吸血系数
	///</summary>
	public float IceVamPer;
	/// <summary>
	/// 百分比伤害吸血系数
	///</summary>
	public float PerVam;
	/// <summary>
	/// 可破地
	///</summary>
	public bool GetGround;

	public override void Read(){
		Id=ReadInt();
		Name=ReadString();
		AddEN=ReadInt();
		AddHits=ReadInt();
		AddCT=ReadInt();
		DelAP=ReadInt();
		VerPower=ReadInt();
		HorPower=ReadArrayInt();
		UseLimit=ReadInt();
		MagicCd=ReadInt();
		PhyAtkPer=ReadFloat();
		MagAtkPer=ReadFloat();
		PhyHurtPer=ReadFloat();
		WindHurtPer=ReadFloat();
		FireHurtPer=ReadFloat();
		ThunderHurtPer=ReadFloat();
		IceHurtPer=ReadFloat();
		PerHurtType=ReadInt();
		PerHurt=ReadFloat();
		RecPer=ReadFloat();
		PerRecType=ReadInt();
		PerRec=ReadFloat();
		SkillCrit=ReadFloat();
		SkillCritAdd=ReadFloat();
		PhyVamPer=ReadFloat();
		WindVamPer=ReadFloat();
		FireVamPer=ReadFloat();
		ThunderVamPer=ReadFloat();
		IceVamPer=ReadFloat();
		PerVam=ReadFloat();
		GetGround=ReadBool();

	}
	
	public override void Write (){
		WriteInt(Id);
		WriteString(Name);
		WriteInt(AddEN);
		WriteInt(AddHits);
		WriteInt(AddCT);
		WriteInt(DelAP);
		WriteInt(VerPower);
		WriteArrayInt(HorPower);
		WriteInt(UseLimit);
		WriteInt(MagicCd);
		WriteFloat(PhyAtkPer);
		WriteFloat(MagAtkPer);
		WriteFloat(PhyHurtPer);
		WriteFloat(WindHurtPer);
		WriteFloat(FireHurtPer);
		WriteFloat(ThunderHurtPer);
		WriteFloat(IceHurtPer);
		WriteInt(PerHurtType);
		WriteFloat(PerHurt);
		WriteFloat(RecPer);
		WriteInt(PerRecType);
		WriteFloat(PerRec);
		WriteFloat(SkillCrit);
		WriteFloat(SkillCritAdd);
		WriteFloat(PhyVamPer);
		WriteFloat(WindVamPer);
		WriteFloat(FireVamPer);
		WriteFloat(ThunderVamPer);
		WriteFloat(IceVamPer);
		WriteFloat(PerVam);
		WriteBool(GetGround);

	}

	public System.Object Clone()  
	{
		return this.MemberwiseClone();
	}
}

public partial class T_SkillValueMgr{
	public Dictionary<int,T_SkillValue> itemData = new Dictionary<int, T_SkillValue>();
	//public Dictionary<int,List<T_SkillValue>> groupData = new Dictionary<int,List<T_SkillValue>>();
	public int size;

	public T_SkillValueMgr(){
		//ReadConfig ();
	}

	public IEnumerator ReadConfig(){
		string path = PathUtil.StreamingPath("Config/T_SkillValue.bytes");
		WWW data = new WWW (path);
		yield return data;
		MemoryStream stream = new MemoryStream (data.bytes);
		stream.Position = 0;
		
		T_SkillValue byteObj = new T_SkillValue ();
		byteObj.SetStream (stream);
		size = byteObj.ReadInt ();
		
		for (int i=0; i<size; i++) {
			T_SkillValue item = new T_SkillValue();
			item.Deserialization(stream);
			
	//		if(!groupData.ContainsKey()){
	//			groupData[] = new List<T_SkillValue>();
	//		}
	//		groupData[].Add(item);

	      if(itemData.ContainsKey(item.Id))Loger.Error("T_SkillValue is Repeat KEY = "+item.Id);
	      itemData[item.Id]=item;
		}

		Loger.Info ("T_SkillValue Config load Complete, size:"+size);
	}

	public T_SkillValue GetData(int Id){
		int id=Id;
		if (itemData.ContainsKey (id)) {
			return itemData[id];
		}
  		//WorldMgr.ShowMessageLog("T_SkillValue not find >> id:"+id);
		Loger.Error ("T_SkillValue not find >> id:"+id);
		return null;
	}
	
	//public List<T_SkillValue> GetGroup(){
	//	
	//	if (groupData.ContainsKey (id)) {
	//		return groupData[id];
	//	}
  	//	//WorldMgr.ShowMessageLog("T_SkillValue not find >> id:"+id);
	//	Loger.Error ("T_SkillValue not find Group >> id:"+id);
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
