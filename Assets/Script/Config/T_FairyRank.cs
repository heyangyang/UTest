using UnityEngine;
using System.IO;
using System.Collections;
using System.Collections.Generic;

public partial class T_FairyRank : ByteBase {

	/// <summary>
	/// ID
	///</summary>
	public int ID;
	/// <summary>
	/// 妖精的ID
	///</summary>
	public int FairyId;
	/// <summary>
	/// 妖精星级
	///</summary>
	public int Star;
	/// <summary>
	/// 模型ID
	///</summary>
	public int Model;
	/// <summary>
	/// 特效ID
	///</summary>
	public int Effect;
	/// <summary>
	/// 技能id
	///</summary>
	public int Skill;

	public override void Read(){
		ID=ReadInt();
		FairyId=ReadInt();
		Star=ReadInt();
		Model=ReadInt();
		Effect=ReadInt();
		Skill=ReadInt();

	}
	
	public override void Write (){
		WriteInt(ID);
		WriteInt(FairyId);
		WriteInt(Star);
		WriteInt(Model);
		WriteInt(Effect);
		WriteInt(Skill);

	}

	public System.Object Clone()  
	{
		return this.MemberwiseClone();
	}
}

public partial class T_FairyRankMgr{
	public Dictionary<string,T_FairyRank> itemData = new Dictionary<string, T_FairyRank>();
	public Dictionary<int,List<T_FairyRank>> groupData = new Dictionary<int,List<T_FairyRank>>();
	public int size;

	public T_FairyRankMgr(){
		//ReadConfig ();
	}

	public IEnumerator ReadConfig(){
		string path = PathUtil.StreamingPath("Config/T_FairyRank.bytes");
		WWW data = new WWW (path);
		yield return data;
		MemoryStream stream = new MemoryStream (data.bytes);
		stream.Position = 0;
		
		T_FairyRank byteObj = new T_FairyRank ();
		byteObj.SetStream (stream);
		size = byteObj.ReadInt ();
		
		for (int i=0; i<size; i++) {
			T_FairyRank item = new T_FairyRank();
			item.Deserialization(stream);
			
			if(!groupData.ContainsKey(item.FairyId)){
				groupData[item.FairyId] = new List<T_FairyRank>();
			}
			groupData[item.FairyId].Add(item);

	      if(itemData.ContainsKey(item.ID.ToString()+'_'+item.FairyId.ToString()))Loger.Error("T_FairyRank is Repeat KEY = "+item.ID.ToString()+'_'+item.FairyId.ToString());
	      itemData[item.ID.ToString()+'_'+item.FairyId.ToString()]=item;
		}

		Loger.Info ("T_FairyRank Config load Complete, size:"+size);
	}

	public T_FairyRank GetData(int ID,int FairyId){
		string id=ID.ToString()+'_'+FairyId.ToString();
		if (itemData.ContainsKey (id)) {
			return itemData[id];
		}
  		//WorldMgr.ShowMessageLog("T_FairyRank not find >> id:"+id);
		Loger.Error ("T_FairyRank not find >> id:"+id);
		return null;
	}
	
	public List<T_FairyRank> GetGroup(int FairyId){
		int id=FairyId;
		if (groupData.ContainsKey (id)) {
			return groupData[id];
		}
  		//WorldMgr.ShowMessageLog("T_FairyRank not find >> id:"+id);
		Loger.Error ("T_FairyRank not find Group >> id:"+id);
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
