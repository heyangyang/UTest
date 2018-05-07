using UnityEngine;
using System.IO;
using System.Collections;
using System.Collections.Generic;

public partial class T_ActorWeapon : ByteBase {

	/// <summary>
	/// 角色ID
	///</summary>
	public int Id;
	/// <summary>
	/// 初始阶级
	///</summary>
	public int Class;
	/// <summary>
	/// 初始等级
	///</summary>
	public int Lv;
	/// <summary>
	/// 武器物理攻击下限系数
	///</summary>
	public int Weaponphyatkmin;
	/// <summary>
	/// 武器物理攻击上限系数
	///</summary>
	public int Weaponphyatkmax;
	/// <summary>
	/// 武器法术攻击下限系数
	///</summary>
	public int Weaponmagatkmin;
	/// <summary>
	/// 武器法术攻击上限系数
	///</summary>
	public int Weaponmagatkmax;

	public override void Read(){
		Id=ReadInt();
		Class=ReadInt();
		Lv=ReadInt();
		Weaponphyatkmin=ReadInt();
		Weaponphyatkmax=ReadInt();
		Weaponmagatkmin=ReadInt();
		Weaponmagatkmax=ReadInt();

	}
	
	public override void Write (){
		WriteInt(Id);
		WriteInt(Class);
		WriteInt(Lv);
		WriteInt(Weaponphyatkmin);
		WriteInt(Weaponphyatkmax);
		WriteInt(Weaponmagatkmin);
		WriteInt(Weaponmagatkmax);

	}

	public System.Object Clone()  
	{
		return this.MemberwiseClone();
	}
}

public partial class T_ActorWeaponMgr{
	public Dictionary<int,T_ActorWeapon> itemData = new Dictionary<int, T_ActorWeapon>();
	//public Dictionary<int,List<T_ActorWeapon>> groupData = new Dictionary<int,List<T_ActorWeapon>>();
	public int size;

	public T_ActorWeaponMgr(){
		//ReadConfig ();
	}

	public IEnumerator ReadConfig(){
		string path = PathUtil.StreamingPath("Config/T_ActorWeapon.bytes");
		WWW data = new WWW (path);
		yield return data;
		MemoryStream stream = new MemoryStream (data.bytes);
		stream.Position = 0;
		
		T_ActorWeapon byteObj = new T_ActorWeapon ();
		byteObj.SetStream (stream);
		size = byteObj.ReadInt ();
		
		for (int i=0; i<size; i++) {
			T_ActorWeapon item = new T_ActorWeapon();
			item.Deserialization(stream);
			
	//		if(!groupData.ContainsKey()){
	//			groupData[] = new List<T_ActorWeapon>();
	//		}
	//		groupData[].Add(item);

	      if(itemData.ContainsKey(item.Id))Loger.Error("T_ActorWeapon is Repeat KEY = "+item.Id);
	      itemData[item.Id]=item;
		}

		Loger.Info ("T_ActorWeapon Config load Complete, size:"+size);
	}

	public T_ActorWeapon GetData(int Id){
		int id=Id;
		if (itemData.ContainsKey (id)) {
			return itemData[id];
		}
  		//WorldMgr.ShowMessageLog("T_ActorWeapon not find >> id:"+id);
		Loger.Error ("T_ActorWeapon not find >> id:"+id);
		return null;
	}
	
	//public List<T_ActorWeapon> GetGroup(){
	//	
	//	if (groupData.ContainsKey (id)) {
	//		return groupData[id];
	//	}
  	//	//WorldMgr.ShowMessageLog("T_ActorWeapon not find >> id:"+id);
	//	Loger.Error ("T_ActorWeapon not find Group >> id:"+id);
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
