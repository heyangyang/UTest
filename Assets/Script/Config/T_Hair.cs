using UnityEngine;
using System.IO;
using System.Collections;
using System.Collections.Generic;

public partial class T_Hair : ByteBase {

	/// <summary>
	/// 发型ID
	///</summary>
	public int Id;
	/// <summary>
	/// 发型性别
	///</summary>
	public int Sexuality;
	/// <summary>
	/// 发型按钮
	///</summary>
	public string HairButton;
	/// <summary>
	/// 发型名字
	///</summary>
	public string Hair;
	/// <summary>
	/// 颜色组
	///</summary>
	public int ColorGroup;
	/// <summary>
	/// 品质
	///</summary>
	public int Quality;
	/// <summary>
	/// 显示名字
	///</summary>
	public string Name;
	/// <summary>
	/// 立绘图
	///</summary>
	public string Picture;
	/// <summary>
	/// 用途
	///</summary>
	public int UseOn;
	/// <summary>
	/// 所属角色
	///</summary>
	public int Character;
	/// <summary>
	/// 使用期限
	///</summary>
	public int Term;
	/// <summary>
	/// 获得途径
	///</summary>
	public int WaysToObtain;
	/// <summary>
	/// 商品ID
	///</summary>
	public int Shop;
	/// <summary>
	/// 获得描述
	///</summary>
	public string Discribe;

	public override void Read(){
		Id=ReadInt();
		Sexuality=ReadInt();
		HairButton=ReadString();
		Hair=ReadString();
		ColorGroup=ReadInt();
		Quality=ReadInt();
		Name=ReadString();
		Picture=ReadString();
		UseOn=ReadInt();
		Character=ReadInt();
		Term=ReadInt();
		WaysToObtain=ReadInt();
		Shop=ReadInt();
		Discribe=ReadString();

	}
	
	public override void Write (){
		WriteInt(Id);
		WriteInt(Sexuality);
		WriteString(HairButton);
		WriteString(Hair);
		WriteInt(ColorGroup);
		WriteInt(Quality);
		WriteString(Name);
		WriteString(Picture);
		WriteInt(UseOn);
		WriteInt(Character);
		WriteInt(Term);
		WriteInt(WaysToObtain);
		WriteInt(Shop);
		WriteString(Discribe);

	}

	public System.Object Clone()  
	{
		return this.MemberwiseClone();
	}
}

public partial class T_HairMgr{
	public Dictionary<int,T_Hair> itemData = new Dictionary<int, T_Hair>();
	//public Dictionary<int,List<T_Hair>> groupData = new Dictionary<int,List<T_Hair>>();
	public int size;

	public T_HairMgr(){
		//ReadConfig ();
	}

	public IEnumerator ReadConfig(){
		string path = PathUtil.StreamingPath("Config/T_Hair.bytes");
		WWW data = new WWW (path);
		yield return data;
		MemoryStream stream = new MemoryStream (data.bytes);
		stream.Position = 0;
		
		T_Hair byteObj = new T_Hair ();
		byteObj.SetStream (stream);
		size = byteObj.ReadInt ();
		
		for (int i=0; i<size; i++) {
			T_Hair item = new T_Hair();
			item.Deserialization(stream);
			
	//		if(!groupData.ContainsKey()){
	//			groupData[] = new List<T_Hair>();
	//		}
	//		groupData[].Add(item);

	      if(itemData.ContainsKey(item.Id))Loger.Error("T_Hair is Repeat KEY = "+item.Id);
	      itemData[item.Id]=item;
		}

		Loger.Info ("T_Hair Config load Complete, size:"+size);
	}

	public T_Hair GetData(int Id){
		int id=Id;
		if (itemData.ContainsKey (id)) {
			return itemData[id];
		}
  		//WorldMgr.ShowMessageLog("T_Hair not find >> id:"+id);
		Loger.Error ("T_Hair not find >> id:"+id);
		return null;
	}
	
	//public List<T_Hair> GetGroup(){
	//	
	//	if (groupData.ContainsKey (id)) {
	//		return groupData[id];
	//	}
  	//	//WorldMgr.ShowMessageLog("T_Hair not find >> id:"+id);
	//	Loger.Error ("T_Hair not find Group >> id:"+id);
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
