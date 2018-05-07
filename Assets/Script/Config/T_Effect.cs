using UnityEngine;
using System.IO;
using System.Collections;
using System.Collections.Generic;

public partial class T_Effect : ByteBase {

	/// <summary>
	/// Id
	///</summary>
	public int Id;
	/// <summary>
	/// 特效名
	///</summary>
	public string Name;
	/// <summary>
	/// 资源名称
	///</summary>
	public string[] Resource;
	/// <summary>
	/// 动作名
	///</summary>
	public string Action;
	/// <summary>
	/// 挂点
	///</summary>
	public string Pos;
	/// <summary>
	/// 受击者挂点
	///</summary>
	public string BPos;
	/// <summary>
	/// 是否跟随减速
	///</summary>
	public bool IsSlow;
	/// <summary>
	/// 动作结束后移除
	///</summary>
	public bool IsRemove;
	/// <summary>
	/// 是否跟随
	///</summary>
	public bool IsFollow;
	/// <summary>
	/// 特效时长
	///</summary>
	public int Time;
	/// <summary>
	/// 特效限制数
	///</summary>
	public int Limit;
	/// <summary>
	/// 是否是摄像机
	///</summary>
	public bool IsCamera;
	/// <summary>
	/// 特效缩放倍数
	///</summary>
	public int ZoomRatio;
	/// <summary>
	/// 是否角色立绘
	///</summary>
	public bool WhetherPicture;

	public override void Read(){
		Id=ReadInt();
		Name=ReadString();
		Resource=ReadArrayStr();
		Action=ReadString();
		Pos=ReadString();
		BPos=ReadString();
		IsSlow=ReadBool();
		IsRemove=ReadBool();
		IsFollow=ReadBool();
		Time=ReadInt();
		Limit=ReadInt();
		IsCamera=ReadBool();
		ZoomRatio=ReadInt();
		WhetherPicture=ReadBool();

	}
	
	public override void Write (){
		WriteInt(Id);
		WriteString(Name);
		WriteArrayStr(Resource);
		WriteString(Action);
		WriteString(Pos);
		WriteString(BPos);
		WriteBool(IsSlow);
		WriteBool(IsRemove);
		WriteBool(IsFollow);
		WriteInt(Time);
		WriteInt(Limit);
		WriteBool(IsCamera);
		WriteInt(ZoomRatio);
		WriteBool(WhetherPicture);

	}

	public System.Object Clone()  
	{
		return this.MemberwiseClone();
	}
}

public partial class T_EffectMgr{
	public Dictionary<int,T_Effect> itemData = new Dictionary<int, T_Effect>();
	//public Dictionary<int,List<T_Effect>> groupData = new Dictionary<int,List<T_Effect>>();
	public int size;

	public T_EffectMgr(){
		//ReadConfig ();
	}

	public IEnumerator ReadConfig(){
		string path = PathUtil.StreamingPath("Config/T_Effect.bytes");
		WWW data = new WWW (path);
		yield return data;
		MemoryStream stream = new MemoryStream (data.bytes);
		stream.Position = 0;
		
		T_Effect byteObj = new T_Effect ();
		byteObj.SetStream (stream);
		size = byteObj.ReadInt ();
		
		for (int i=0; i<size; i++) {
			T_Effect item = new T_Effect();
			item.Deserialization(stream);
			
	//		if(!groupData.ContainsKey()){
	//			groupData[] = new List<T_Effect>();
	//		}
	//		groupData[].Add(item);

	      if(itemData.ContainsKey(item.Id))Loger.Error("T_Effect is Repeat KEY = "+item.Id);
	      itemData[item.Id]=item;
		}

		Loger.Info ("T_Effect Config load Complete, size:"+size);
	}

	public T_Effect GetData(int Id){
		int id=Id;
		if (itemData.ContainsKey (id)) {
			return itemData[id];
		}
  		//WorldMgr.ShowMessageLog("T_Effect not find >> id:"+id);
		Loger.Error ("T_Effect not find >> id:"+id);
		return null;
	}
	
	//public List<T_Effect> GetGroup(){
	//	
	//	if (groupData.ContainsKey (id)) {
	//		return groupData[id];
	//	}
  	//	//WorldMgr.ShowMessageLog("T_Effect not find >> id:"+id);
	//	Loger.Error ("T_Effect not find Group >> id:"+id);
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
