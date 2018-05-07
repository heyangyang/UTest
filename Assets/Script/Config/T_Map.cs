using UnityEngine;
using System.IO;
using System.Collections;
using System.Collections.Generic;

public partial class T_Map : ByteBase {
	public enum MAPTYPE{
		NONE		=	0,
		Login		=	1,
		Main		=	2,
		Pve		=	3,
		Pvp		=	4,
		CreateRole		=	5,
		World		=	6,
		Pvr		=	7,
	}

	/// <summary>
	/// ID
	///</summary>
	public int Id;
	/// <summary>
	/// 地图名称
	///</summary>
	public string Name;
	/// <summary>
	/// 场景名
	///</summary>
	public string SceneName;
	/// <summary>
	/// 地图类型
	///</summary>
	public MAPTYPE MapType;
	/// <summary>
	/// 显示场景层数
	///</summary>
	public int[] NumberScene;
	/// <summary>
	/// 背景音乐
	///</summary>
	public int[] BGMid;
	/// <summary>
	/// 是否允许暂停功能
	///</summary>
	public int Suspend;
	/// <summary>
	/// 音乐是否循环
	///</summary>
	public int Loop;
	/// <summary>
	/// 天空高度值
	///</summary>
	public int SkyHeight;
	/// <summary>
	/// 天空损耗指定值播特效
	///</summary>
	public int[] SkyEffect;
	/// <summary>
	/// 地面负高度值
	///</summary>
	public int LandHeight;
	/// <summary>
	/// 地面损耗指定值播特效
	///</summary>
	public int[] GroundEffect;
	/// <summary>
	/// 击飞目标锁定高度
	///</summary>
	public int LockHeight;
	/// <summary>
	/// 角色击飞镜头旋转高度
	///</summary>
	public int MoveHeight;
	/// <summary>
	/// 底层场景高度
	///</summary>
	public int DownHeight;
	/// <summary>
	/// 中层场景高度
	///</summary>
	public int MinHeight;
	/// <summary>
	/// 顶层场景高度
	///</summary>
	public int TopHeight;
	/// <summary>
	/// 破天效果buff
	///</summary>
	public int[] SkyResult;
	/// <summary>
	/// 破天效果获得能量值
	///</summary>
	public int[] SkyResultTwo;
	/// <summary>
	/// 破地效果
	///</summary>
	public int[] GroundResult;
	/// <summary>
	/// 破地效果获得能量值
	///</summary>
	public int[] GroundResultTwo;
	/// <summary>
	/// PVE已方是否先手
	///</summary>
	public int FirstAttack;
	/// <summary>
	/// 先手CT加成
	///</summary>
	public int[] GainOne;
	/// <summary>
	/// 后手CT加成
	///</summary>
	public int[] GainTwo;
	/// <summary>
	/// PVP先手获得buff
	///</summary>
	public int[] FirstGainBuff;
	/// <summary>
	/// PVP后手获得buff
	///</summary>
	public int[] AfterGainBuff;

	public override void Read(){
		Id=ReadInt();
		Name=ReadString();
		SceneName=ReadString();
		MapType=(MAPTYPE)ReadShort();
		NumberScene=ReadArrayInt();
		BGMid=ReadArrayInt();
		Suspend=ReadInt();
		Loop=ReadInt();
		SkyHeight=ReadInt();
		SkyEffect=ReadArrayInt();
		LandHeight=ReadInt();
		GroundEffect=ReadArrayInt();
		LockHeight=ReadInt();
		MoveHeight=ReadInt();
		DownHeight=ReadInt();
		MinHeight=ReadInt();
		TopHeight=ReadInt();
		SkyResult=ReadArrayInt();
		SkyResultTwo=ReadArrayInt();
		GroundResult=ReadArrayInt();
		GroundResultTwo=ReadArrayInt();
		FirstAttack=ReadInt();
		GainOne=ReadArrayInt();
		GainTwo=ReadArrayInt();
		FirstGainBuff=ReadArrayInt();
		AfterGainBuff=ReadArrayInt();

	}
	
	public override void Write (){
		WriteInt(Id);
		WriteString(Name);
		WriteString(SceneName);
		WriteShort((short)MapType);
		WriteArrayInt(NumberScene);
		WriteArrayInt(BGMid);
		WriteInt(Suspend);
		WriteInt(Loop);
		WriteInt(SkyHeight);
		WriteArrayInt(SkyEffect);
		WriteInt(LandHeight);
		WriteArrayInt(GroundEffect);
		WriteInt(LockHeight);
		WriteInt(MoveHeight);
		WriteInt(DownHeight);
		WriteInt(MinHeight);
		WriteInt(TopHeight);
		WriteArrayInt(SkyResult);
		WriteArrayInt(SkyResultTwo);
		WriteArrayInt(GroundResult);
		WriteArrayInt(GroundResultTwo);
		WriteInt(FirstAttack);
		WriteArrayInt(GainOne);
		WriteArrayInt(GainTwo);
		WriteArrayInt(FirstGainBuff);
		WriteArrayInt(AfterGainBuff);

	}

	public System.Object Clone()  
	{
		return this.MemberwiseClone();
	}
}

public partial class T_MapMgr{
	public Dictionary<int,T_Map> itemData = new Dictionary<int, T_Map>();
	//public Dictionary<int,List<T_Map>> groupData = new Dictionary<int,List<T_Map>>();
	public int size;

	public T_MapMgr(){
		//ReadConfig ();
	}

	public IEnumerator ReadConfig(){
		string path = PathUtil.StreamingPath("Config/T_Map.bytes");
		WWW data = new WWW (path);
		yield return data;
		MemoryStream stream = new MemoryStream (data.bytes);
		stream.Position = 0;
		
		T_Map byteObj = new T_Map ();
		byteObj.SetStream (stream);
		size = byteObj.ReadInt ();
		
		for (int i=0; i<size; i++) {
			T_Map item = new T_Map();
			item.Deserialization(stream);
			
	//		if(!groupData.ContainsKey()){
	//			groupData[] = new List<T_Map>();
	//		}
	//		groupData[].Add(item);

	      if(itemData.ContainsKey(item.Id))Loger.Error("T_Map is Repeat KEY = "+item.Id);
	      itemData[item.Id]=item;
		}

		Loger.Info ("T_Map Config load Complete, size:"+size);
	}

	public T_Map GetData(int Id){
		int id=Id;
		if (itemData.ContainsKey (id)) {
			return itemData[id];
		}
  		//WorldMgr.ShowMessageLog("T_Map not find >> id:"+id);
		Loger.Error ("T_Map not find >> id:"+id);
		return null;
	}
	
	//public List<T_Map> GetGroup(){
	//	
	//	if (groupData.ContainsKey (id)) {
	//		return groupData[id];
	//	}
  	//	//WorldMgr.ShowMessageLog("T_Map not find >> id:"+id);
	//	Loger.Error ("T_Map not find Group >> id:"+id);
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
