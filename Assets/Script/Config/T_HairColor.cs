using UnityEngine;
using System.IO;
using System.Collections;
using System.Collections.Generic;

public partial class T_HairColor : ByteBase {

	/// <summary>
	/// 颜色ID
	///</summary>
	public int Id;
	/// <summary>
	/// 颜色组
	///</summary>
	public int Group;
	/// <summary>
	/// 模型颜色
	///</summary>
	public int[] ModelColor;
	/// <summary>
	/// 面板展示颜色
	///</summary>
	public int[] PanelColor;

	public override void Read(){
		Id=ReadInt();
		Group=ReadInt();
		ModelColor=ReadArrayInt();
		PanelColor=ReadArrayInt();

	}
	
	public override void Write (){
		WriteInt(Id);
		WriteInt(Group);
		WriteArrayInt(ModelColor);
		WriteArrayInt(PanelColor);

	}

	public System.Object Clone()  
	{
		return this.MemberwiseClone();
	}
}

public partial class T_HairColorMgr{
	public Dictionary<string,T_HairColor> itemData = new Dictionary<string, T_HairColor>();
	public Dictionary<int,List<T_HairColor>> groupData = new Dictionary<int,List<T_HairColor>>();
	public int size;

	public T_HairColorMgr(){
		//ReadConfig ();
	}

	public IEnumerator ReadConfig(){
		string path = PathUtil.StreamingPath("Config/T_HairColor.bytes");
		WWW data = new WWW (path);
		yield return data;
		MemoryStream stream = new MemoryStream (data.bytes);
		stream.Position = 0;
		
		T_HairColor byteObj = new T_HairColor ();
		byteObj.SetStream (stream);
		size = byteObj.ReadInt ();
		
		for (int i=0; i<size; i++) {
			T_HairColor item = new T_HairColor();
			item.Deserialization(stream);
			
			if(!groupData.ContainsKey(item.Group)){
				groupData[item.Group] = new List<T_HairColor>();
			}
			groupData[item.Group].Add(item);

	      if(itemData.ContainsKey(item.Id.ToString()+'_'+item.Group.ToString()))Loger.Error("T_HairColor is Repeat KEY = "+item.Id.ToString()+'_'+item.Group.ToString());
	      itemData[item.Id.ToString()+'_'+item.Group.ToString()]=item;
		}

		Loger.Info ("T_HairColor Config load Complete, size:"+size);
	}

	public T_HairColor GetData(int Id,int Group){
		string id=Id.ToString()+'_'+Group.ToString();
		if (itemData.ContainsKey (id)) {
			return itemData[id];
		}
  		//WorldMgr.ShowMessageLog("T_HairColor not find >> id:"+id);
		Loger.Error ("T_HairColor not find >> id:"+id);
		return null;
	}
	
	public List<T_HairColor> GetGroup(int Group){
		int id=Group;
		if (groupData.ContainsKey (id)) {
			return groupData[id];
		}
  		//WorldMgr.ShowMessageLog("T_HairColor not find >> id:"+id);
		Loger.Error ("T_HairColor not find Group >> id:"+id);
		return null;
	}

	public bool HasData(int Id,int Group){
		string id=Id.ToString()+'_'+Group.ToString();
		return itemData.ContainsKey (id);
	}
	
	public bool HasList(int Group){
		int id=Group;
		return groupData.ContainsKey (id);
	}
}
