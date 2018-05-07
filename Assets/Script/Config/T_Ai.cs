using UnityEngine;
using System.IO;
using System.Collections;
using System.Collections.Generic;

public partial class T_Ai : ByteBase {

	/// <summary>
	/// id
	///</summary>
	public int Id;
	/// <summary>
	/// 优先级
	///</summary>
	public int Priority;
	/// <summary>
	/// 目标
	///</summary>
	public int Target;
	/// <summary>
	/// 条件
	///</summary>
	public int Condition;
	/// <summary>
	/// 条件参数
	///</summary>
	public int[] CondParams;
	/// <summary>
	/// 行为
	///</summary>
	public int Action;
	/// <summary>
	/// 行为参数
	///</summary>
	public int[] ActParams;
	/// <summary>
	/// 备注
	///</summary>
	public string Desc;

	public override void Read(){
		Id=ReadInt();
		Priority=ReadInt();
		Target=ReadInt();
		Condition=ReadInt();
		CondParams=ReadArrayInt();
		Action=ReadInt();
		ActParams=ReadArrayInt();
		Desc=ReadString();

	}
	
	public override void Write (){
		WriteInt(Id);
		WriteInt(Priority);
		WriteInt(Target);
		WriteInt(Condition);
		WriteArrayInt(CondParams);
		WriteInt(Action);
		WriteArrayInt(ActParams);
		WriteString(Desc);

	}

	public System.Object Clone()  
	{
		return this.MemberwiseClone();
	}
}

public partial class T_AiMgr{
	//public Dictionary<int,T_Ai> itemData = new Dictionary<int, T_Ai>();
	public Dictionary<int,List<T_Ai>> groupData = new Dictionary<int,List<T_Ai>>();
	public int size;

	public T_AiMgr(){
		//ReadConfig ();
	}

	public IEnumerator ReadConfig(){
		string path = PathUtil.StreamingPath("Config/T_Ai.bytes");
		WWW data = new WWW (path);
		yield return data;
		MemoryStream stream = new MemoryStream (data.bytes);
		stream.Position = 0;
		
		T_Ai byteObj = new T_Ai ();
		byteObj.SetStream (stream);
		size = byteObj.ReadInt ();
		
		for (int i=0; i<size; i++) {
			T_Ai item = new T_Ai();
			item.Deserialization(stream);
			
			if(!groupData.ContainsKey(item.Id)){
				groupData[item.Id] = new List<T_Ai>();
			}
			groupData[item.Id].Add(item);

	//      if(itemData.ContainsKey(item.Id))Loger.Error("T_Ai is Repeat KEY = "+item.Id);
	//      itemData[item.Id]=item;
		}

		Loger.Info ("T_Ai Config load Complete, size:"+size);
	}

	//public T_Ai GetData(int Id){
	//	int id=Id;
	//	if (itemData.ContainsKey (id)) {
	//		return itemData[id];
	//	}
  	//	//WorldMgr.ShowMessageLog("T_Ai not find >> id:"+id);
	//	Loger.Error ("T_Ai not find >> id:"+id);
	//	return null;
	//}
	
	public List<T_Ai> GetGroup(int Id){
		int id=Id;
		if (groupData.ContainsKey (id)) {
			return groupData[id];
		}
  		//WorldMgr.ShowMessageLog("T_Ai not find >> id:"+id);
		Loger.Error ("T_Ai not find Group >> id:"+id);
		return null;
	}

	//public bool HasData(int Id){
	//	int id=Id;
	//	return itemData.ContainsKey (id);
	//}
	
	public bool HasList(int Id){
		int id=Id;
		return groupData.ContainsKey (id);
	}
}
