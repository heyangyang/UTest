using UnityEngine;
using System.IO;
using System.Collections;
using System.Collections.Generic;

public partial class T_FairyRes : ByteBase {

	/// <summary>
	/// 妖精ID
	///</summary>
	public int Id;
	/// <summary>
	/// 妖精模型
	///</summary>
	public string ModelRole;
	/// <summary>
	/// 妖精名称
	///</summary>
	public string Name;
	/// <summary>
	/// 模型缩放比
	///</summary>
	public int ScaleRatio;
	/// <summary>
	/// 模型位置
	///</summary>
	public int[] Position;
	/// <summary>
	/// 界面模型旋转角度
	///</summary>
	public int[] Rotation;
	/// <summary>
	/// UI特效
	///</summary>
	public string UiEffect;
	/// <summary>
	/// 待机动作
	///</summary>
	public string DefaultAction;
	/// <summary>
	/// 主城待机动作
	///</summary>
	public string CityAction1;
	/// <summary>
	/// 主城休闲动作
	///</summary>
	public string CityAction2;
	/// <summary>
	/// 攻击动作
	///</summary>
	public string[] AtkAction;
	/// <summary>
	/// 动作时长
	///</summary>
	public int[] AtkTime;
	/// <summary>
	/// 主城模型旋转角度
	///</summary>
	public int[] CRotation;

	public override void Read(){
		Id=ReadInt();
		ModelRole=ReadString();
		Name=ReadString();
		ScaleRatio=ReadInt();
		Position=ReadArrayInt();
		Rotation=ReadArrayInt();
		UiEffect=ReadString();
		DefaultAction=ReadString();
		CityAction1=ReadString();
		CityAction2=ReadString();
		AtkAction=ReadArrayStr();
		AtkTime=ReadArrayInt();
		CRotation=ReadArrayInt();

	}
	
	public override void Write (){
		WriteInt(Id);
		WriteString(ModelRole);
		WriteString(Name);
		WriteInt(ScaleRatio);
		WriteArrayInt(Position);
		WriteArrayInt(Rotation);
		WriteString(UiEffect);
		WriteString(DefaultAction);
		WriteString(CityAction1);
		WriteString(CityAction2);
		WriteArrayStr(AtkAction);
		WriteArrayInt(AtkTime);
		WriteArrayInt(CRotation);

	}

	public System.Object Clone()  
	{
		return this.MemberwiseClone();
	}
}

public partial class T_FairyResMgr{
	public Dictionary<int,T_FairyRes> itemData = new Dictionary<int, T_FairyRes>();
	//public Dictionary<int,List<T_FairyRes>> groupData = new Dictionary<int,List<T_FairyRes>>();
	public int size;

	public T_FairyResMgr(){
		//ReadConfig ();
	}

	public IEnumerator ReadConfig(){
		string path = PathUtil.StreamingPath("Config/T_FairyRes.bytes");
		WWW data = new WWW (path);
		yield return data;
		MemoryStream stream = new MemoryStream (data.bytes);
		stream.Position = 0;
		
		T_FairyRes byteObj = new T_FairyRes ();
		byteObj.SetStream (stream);
		size = byteObj.ReadInt ();
		
		for (int i=0; i<size; i++) {
			T_FairyRes item = new T_FairyRes();
			item.Deserialization(stream);
			
	//		if(!groupData.ContainsKey()){
	//			groupData[] = new List<T_FairyRes>();
	//		}
	//		groupData[].Add(item);

	      if(itemData.ContainsKey(item.Id))Loger.Error("T_FairyRes is Repeat KEY = "+item.Id);
	      itemData[item.Id]=item;
		}

		Loger.Info ("T_FairyRes Config load Complete, size:"+size);
	}

	public T_FairyRes GetData(int Id){
		int id=Id;
		if (itemData.ContainsKey (id)) {
			return itemData[id];
		}
  		//WorldMgr.ShowMessageLog("T_FairyRes not find >> id:"+id);
		Loger.Error ("T_FairyRes not find >> id:"+id);
		return null;
	}
	
	//public List<T_FairyRes> GetGroup(){
	//	
	//	if (groupData.ContainsKey (id)) {
	//		return groupData[id];
	//	}
  	//	//WorldMgr.ShowMessageLog("T_FairyRes not find >> id:"+id);
	//	Loger.Error ("T_FairyRes not find Group >> id:"+id);
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
