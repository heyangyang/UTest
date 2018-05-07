using UnityEngine;
using System.IO;
using System.Collections;
using System.Collections.Generic;

public partial class T_Drop : ByteBase {

	/// <summary>
	/// 掉落ID
	///</summary>
	public int Id;
	/// <summary>
	/// 掉落方式
	///</summary>
	public int Type;
	/// <summary>
	/// 掉落几率
	///</summary>
	public float Percent;
	/// <summary>
	/// 掉落内容
	///</summary>
	public int[] Behavior;
	/// <summary>
	/// 道具掉落权重
	///</summary>
	public int[] Random;
	/// <summary>
	/// 掉落类型
	///</summary>
	public int[] DropType;

	public override void Read(){
		Id=ReadInt();
		Type=ReadInt();
		Percent=ReadFloat();
		Behavior=ReadArrayInt();
		Random=ReadArrayInt();
		DropType=ReadArrayInt();

	}
	
	public override void Write (){
		WriteInt(Id);
		WriteInt(Type);
		WriteFloat(Percent);
		WriteArrayInt(Behavior);
		WriteArrayInt(Random);
		WriteArrayInt(DropType);

	}

	public System.Object Clone()  
	{
		return this.MemberwiseClone();
	}
}

public partial class T_DropMgr{
	public Dictionary<int,T_Drop> itemData = new Dictionary<int, T_Drop>();
	//public Dictionary<int,List<T_Drop>> groupData = new Dictionary<int,List<T_Drop>>();
	public int size;

	public T_DropMgr(){
		//ReadConfig ();
	}

	public IEnumerator ReadConfig(){
		string path = PathUtil.StreamingPath("Config/T_Drop.bytes");
		WWW data = new WWW (path);
		yield return data;
		MemoryStream stream = new MemoryStream (data.bytes);
		stream.Position = 0;
		
		T_Drop byteObj = new T_Drop ();
		byteObj.SetStream (stream);
		size = byteObj.ReadInt ();
		
		for (int i=0; i<size; i++) {
			T_Drop item = new T_Drop();
			item.Deserialization(stream);
			
	//		if(!groupData.ContainsKey()){
	//			groupData[] = new List<T_Drop>();
	//		}
	//		groupData[].Add(item);

	      if(itemData.ContainsKey(item.Id))Loger.Error("T_Drop is Repeat KEY = "+item.Id);
	      itemData[item.Id]=item;
		}

		Loger.Info ("T_Drop Config load Complete, size:"+size);
	}

	public T_Drop GetData(int Id){
		int id=Id;
		if (itemData.ContainsKey (id)) {
			return itemData[id];
		}
  		//WorldMgr.ShowMessageLog("T_Drop not find >> id:"+id);
		Loger.Error ("T_Drop not find >> id:"+id);
		return null;
	}
	
	//public List<T_Drop> GetGroup(){
	//	
	//	if (groupData.ContainsKey (id)) {
	//		return groupData[id];
	//	}
  	//	//WorldMgr.ShowMessageLog("T_Drop not find >> id:"+id);
	//	Loger.Error ("T_Drop not find Group >> id:"+id);
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
