using UnityEngine;
using System.IO;
using System.Collections;
using System.Collections.Generic;

public partial class T_Fairy : ByteBase {

	/// <summary>
	/// 妖精的ID
	///</summary>
	public int FairyId;
	/// <summary>
	/// 名字
	///</summary>
	public string Name;
	/// <summary>
	/// 品质
	///</summary>
	public int Quality;
	/// <summary>
	/// 初始等级
	///</summary>
	public int Level;
	/// <summary>
	/// 初始星级
	///</summary>
	public int Star;
	/// <summary>
	/// 属相
	///</summary>
	public int Constellation;
	/// <summary>
	/// 技能id
	///</summary>
	public int Skill;
	/// <summary>
	/// 升星概率
	///</summary>
	public int Prob;
	/// <summary>
	/// 初始经验
	///</summary>
	public int InitialExp;
	/// <summary>
	/// 图像
	///</summary>
	public string Picture;
	/// <summary>
	/// 图标
	///</summary>
	public string Icon;
	/// <summary>
	/// 模型配置ID
	///</summary>
	public int ResID;

	public override void Read(){
		FairyId=ReadInt();
		Name=ReadString();
		Quality=ReadInt();
		Level=ReadInt();
		Star=ReadInt();
		Constellation=ReadInt();
		Skill=ReadInt();
		Prob=ReadInt();
		InitialExp=ReadInt();
		Picture=ReadString();
		Icon=ReadString();
		ResID=ReadInt();

	}
	
	public override void Write (){
		WriteInt(FairyId);
		WriteString(Name);
		WriteInt(Quality);
		WriteInt(Level);
		WriteInt(Star);
		WriteInt(Constellation);
		WriteInt(Skill);
		WriteInt(Prob);
		WriteInt(InitialExp);
		WriteString(Picture);
		WriteString(Icon);
		WriteInt(ResID);

	}

	public System.Object Clone()  
	{
		return this.MemberwiseClone();
	}
}

public partial class T_FairyMgr{
	public Dictionary<int,T_Fairy> itemData = new Dictionary<int, T_Fairy>();
	//public Dictionary<int,List<T_Fairy>> groupData = new Dictionary<int,List<T_Fairy>>();
	public int size;

	public T_FairyMgr(){
		//ReadConfig ();
	}

	public IEnumerator ReadConfig(){
		string path = PathUtil.StreamingPath("Config/T_Fairy.bytes");
		WWW data = new WWW (path);
		yield return data;
		MemoryStream stream = new MemoryStream (data.bytes);
		stream.Position = 0;
		
		T_Fairy byteObj = new T_Fairy ();
		byteObj.SetStream (stream);
		size = byteObj.ReadInt ();
		
		for (int i=0; i<size; i++) {
			T_Fairy item = new T_Fairy();
			item.Deserialization(stream);
			
	//		if(!groupData.ContainsKey()){
	//			groupData[] = new List<T_Fairy>();
	//		}
	//		groupData[].Add(item);

	      if(itemData.ContainsKey(item.FairyId))Loger.Error("T_Fairy is Repeat KEY = "+item.FairyId);
	      itemData[item.FairyId]=item;
		}

		Loger.Info ("T_Fairy Config load Complete, size:"+size);
	}

	public T_Fairy GetData(int FairyId){
		int id=FairyId;
		if (itemData.ContainsKey (id)) {
			return itemData[id];
		}
  		//WorldMgr.ShowMessageLog("T_Fairy not find >> id:"+id);
		Loger.Error ("T_Fairy not find >> id:"+id);
		return null;
	}
	
	//public List<T_Fairy> GetGroup(){
	//	
	//	if (groupData.ContainsKey (id)) {
	//		return groupData[id];
	//	}
  	//	//WorldMgr.ShowMessageLog("T_Fairy not find >> id:"+id);
	//	Loger.Error ("T_Fairy not find Group >> id:"+id);
	//	return null;
	//}

	public bool HasData(int FairyId){
		int id=FairyId;
		return itemData.ContainsKey (id);
	}
	
	//public bool HasList(){
	//	
	//	return groupData.ContainsKey (id);
	//}
}
