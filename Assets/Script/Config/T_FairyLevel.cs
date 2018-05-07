using UnityEngine;
using System.IO;
using System.Collections;
using System.Collections.Generic;

public partial class T_FairyLevel : ByteBase {

	/// <summary>
	/// 索引ID
	///</summary>
	public int ID;
	/// <summary>
	/// 妖精的ID
	///</summary>
	public int FairyId;
	/// <summary>
	/// 等级
	///</summary>
	public int Level;
	/// <summary>
	/// 培养值
	///</summary>
	public int LevLimit;
	/// <summary>
	/// 增加属性
	///</summary>
	public int[] AddProperty;

	public override void Read(){
		ID=ReadInt();
		FairyId=ReadInt();
		Level=ReadInt();
		LevLimit=ReadInt();
		AddProperty=ReadArrayInt();

	}
	
	public override void Write (){
		WriteInt(ID);
		WriteInt(FairyId);
		WriteInt(Level);
		WriteInt(LevLimit);
		WriteArrayInt(AddProperty);

	}

	public System.Object Clone()  
	{
		return this.MemberwiseClone();
	}
}

public partial class T_FairyLevelMgr{
	public Dictionary<string,T_FairyLevel> itemData = new Dictionary<string, T_FairyLevel>();
	public Dictionary<int,List<T_FairyLevel>> groupData = new Dictionary<int,List<T_FairyLevel>>();
	public int size;

	public T_FairyLevelMgr(){
		//ReadConfig ();
	}

	public IEnumerator ReadConfig(){
		string path = PathUtil.StreamingPath("Config/T_FairyLevel.bytes");
		WWW data = new WWW (path);
		yield return data;
		MemoryStream stream = new MemoryStream (data.bytes);
		stream.Position = 0;
		
		T_FairyLevel byteObj = new T_FairyLevel ();
		byteObj.SetStream (stream);
		size = byteObj.ReadInt ();
		
		for (int i=0; i<size; i++) {
			T_FairyLevel item = new T_FairyLevel();
			item.Deserialization(stream);
			
			if(!groupData.ContainsKey(item.FairyId)){
				groupData[item.FairyId] = new List<T_FairyLevel>();
			}
			groupData[item.FairyId].Add(item);

	      if(itemData.ContainsKey(item.ID.ToString()+'_'+item.FairyId.ToString()))Loger.Error("T_FairyLevel is Repeat KEY = "+item.ID.ToString()+'_'+item.FairyId.ToString());
	      itemData[item.ID.ToString()+'_'+item.FairyId.ToString()]=item;
		}

		Loger.Info ("T_FairyLevel Config load Complete, size:"+size);
	}

	public T_FairyLevel GetData(int ID,int FairyId){
		string id=ID.ToString()+'_'+FairyId.ToString();
		if (itemData.ContainsKey (id)) {
			return itemData[id];
		}
  		//WorldMgr.ShowMessageLog("T_FairyLevel not find >> id:"+id);
		Loger.Error ("T_FairyLevel not find >> id:"+id);
		return null;
	}
	
	public List<T_FairyLevel> GetGroup(int FairyId){
		int id=FairyId;
		if (groupData.ContainsKey (id)) {
			return groupData[id];
		}
  		//WorldMgr.ShowMessageLog("T_FairyLevel not find >> id:"+id);
		Loger.Error ("T_FairyLevel not find Group >> id:"+id);
		return null;
	}

	public bool HasData(int ID,int FairyId){
		string id=ID.ToString()+'_'+FairyId.ToString();
		return itemData.ContainsKey (id);
	}
	
	public bool HasList(int FairyId){
		int id=FairyId;
		return groupData.ContainsKey (id);
	}
}
