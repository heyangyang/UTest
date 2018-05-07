using UnityEngine;
using System.IO;
using System.Collections;
using System.Collections.Generic;

public partial class T_MonsterGroup : ByteBase {

	/// <summary>
	/// 怪物组ID
	///</summary>
	public int Id;
	/// <summary>
	/// 怪物组内容
	///</summary>
	public int[] MonsterGroup;
	/// <summary>
	/// 阵型
	///</summary>
	public int Formation;
	/// <summary>
	/// 怪物组用AI组ID
	///</summary>
	public int AIGroupID;

	public override void Read(){
		Id=ReadInt();
		MonsterGroup=ReadArrayInt();
		Formation=ReadInt();
		AIGroupID=ReadInt();

	}
	
	public override void Write (){
		WriteInt(Id);
		WriteArrayInt(MonsterGroup);
		WriteInt(Formation);
		WriteInt(AIGroupID);

	}

	public System.Object Clone()  
	{
		return this.MemberwiseClone();
	}
}

public partial class T_MonsterGroupMgr{
	public Dictionary<int,T_MonsterGroup> itemData = new Dictionary<int, T_MonsterGroup>();
	//public Dictionary<int,List<T_MonsterGroup>> groupData = new Dictionary<int,List<T_MonsterGroup>>();
	public int size;

	public T_MonsterGroupMgr(){
		//ReadConfig ();
	}

	public IEnumerator ReadConfig(){
		string path = PathUtil.StreamingPath("Config/T_MonsterGroup.bytes");
		WWW data = new WWW (path);
		yield return data;
		MemoryStream stream = new MemoryStream (data.bytes);
		stream.Position = 0;
		
		T_MonsterGroup byteObj = new T_MonsterGroup ();
		byteObj.SetStream (stream);
		size = byteObj.ReadInt ();
		
		for (int i=0; i<size; i++) {
			T_MonsterGroup item = new T_MonsterGroup();
			item.Deserialization(stream);
			
	//		if(!groupData.ContainsKey()){
	//			groupData[] = new List<T_MonsterGroup>();
	//		}
	//		groupData[].Add(item);

	      if(itemData.ContainsKey(item.Id))Loger.Error("T_MonsterGroup is Repeat KEY = "+item.Id);
	      itemData[item.Id]=item;
		}

		Loger.Info ("T_MonsterGroup Config load Complete, size:"+size);
	}

	public T_MonsterGroup GetData(int Id){
		int id=Id;
		if (itemData.ContainsKey (id)) {
			return itemData[id];
		}
  		//WorldMgr.ShowMessageLog("T_MonsterGroup not find >> id:"+id);
		Loger.Error ("T_MonsterGroup not find >> id:"+id);
		return null;
	}
	
	//public List<T_MonsterGroup> GetGroup(){
	//	
	//	if (groupData.ContainsKey (id)) {
	//		return groupData[id];
	//	}
  	//	//WorldMgr.ShowMessageLog("T_MonsterGroup not find >> id:"+id);
	//	Loger.Error ("T_MonsterGroup not find Group >> id:"+id);
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
