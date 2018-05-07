using UnityEngine;
using System.IO;
using System.Collections;
using System.Collections.Generic;

public partial class T_SkillKey : ByteBase {

	/// <summary>
	/// 角色ID
	///</summary>
	public int Id;
	/// <summary>
	/// 角色名
	///</summary>
	public string Name;
	/// <summary>
	/// 决定技
	///</summary>
	public int[] DecisionSkill;
	/// <summary>
	/// 奥义技
	///</summary>
	public int[] Upanishad;
	/// <summary>
	/// 技能
	///</summary>
	public int[] Skill;
	/// <summary>
	/// 非战斗法术【特殊技能】
	///</summary>
	public int[] Magic;
	/// <summary>
	/// 反击技能
	///</summary>
	public int Counterattack;
	/// <summary>
	/// 触发技能
	///</summary>
	public int[] TriggerSkill;
	/// <summary>
	/// 队长技能
	///</summary>
	public int[] CaptainSkill;
	/// <summary>
	/// 混乱技能
	///</summary>
	public int[] ChaosSkill;

	public override void Read(){
		Id=ReadInt();
		Name=ReadString();
		DecisionSkill=ReadArrayInt();
		Upanishad=ReadArrayInt();
		Skill=ReadArrayInt();
		Magic=ReadArrayInt();
		Counterattack=ReadInt();
		TriggerSkill=ReadArrayInt();
		CaptainSkill=ReadArrayInt();
		ChaosSkill=ReadArrayInt();

	}
	
	public override void Write (){
		WriteInt(Id);
		WriteString(Name);
		WriteArrayInt(DecisionSkill);
		WriteArrayInt(Upanishad);
		WriteArrayInt(Skill);
		WriteArrayInt(Magic);
		WriteInt(Counterattack);
		WriteArrayInt(TriggerSkill);
		WriteArrayInt(CaptainSkill);
		WriteArrayInt(ChaosSkill);

	}

	public System.Object Clone()  
	{
		return this.MemberwiseClone();
	}
}

public partial class T_SkillKeyMgr{
	public Dictionary<int,T_SkillKey> itemData = new Dictionary<int, T_SkillKey>();
	//public Dictionary<int,List<T_SkillKey>> groupData = new Dictionary<int,List<T_SkillKey>>();
	public int size;

	public T_SkillKeyMgr(){
		//ReadConfig ();
	}

	public IEnumerator ReadConfig(){
		string path = PathUtil.StreamingPath("Config/T_SkillKey.bytes");
		WWW data = new WWW (path);
		yield return data;
		MemoryStream stream = new MemoryStream (data.bytes);
		stream.Position = 0;
		
		T_SkillKey byteObj = new T_SkillKey ();
		byteObj.SetStream (stream);
		size = byteObj.ReadInt ();
		
		for (int i=0; i<size; i++) {
			T_SkillKey item = new T_SkillKey();
			item.Deserialization(stream);
			
	//		if(!groupData.ContainsKey()){
	//			groupData[] = new List<T_SkillKey>();
	//		}
	//		groupData[].Add(item);

	      if(itemData.ContainsKey(item.Id))Loger.Error("T_SkillKey is Repeat KEY = "+item.Id);
	      itemData[item.Id]=item;
		}

		Loger.Info ("T_SkillKey Config load Complete, size:"+size);
	}

	public T_SkillKey GetData(int Id){
		int id=Id;
		if (itemData.ContainsKey (id)) {
			return itemData[id];
		}
  		//WorldMgr.ShowMessageLog("T_SkillKey not find >> id:"+id);
		Loger.Error ("T_SkillKey not find >> id:"+id);
		return null;
	}
	
	//public List<T_SkillKey> GetGroup(){
	//	
	//	if (groupData.ContainsKey (id)) {
	//		return groupData[id];
	//	}
  	//	//WorldMgr.ShowMessageLog("T_SkillKey not find >> id:"+id);
	//	Loger.Error ("T_SkillKey not find Group >> id:"+id);
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
