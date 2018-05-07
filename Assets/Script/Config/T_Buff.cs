using UnityEngine;
using System.IO;
using System.Collections;
using System.Collections.Generic;

public partial class T_Buff : ByteBase {

	/// <summary>
	/// 唯一ID
	///</summary>
	public int Id;
	/// <summary>
	/// BUFF名称
	///</summary>
	public string Name;
	/// <summary>
	/// BUFF描述
	///</summary>
	public string Des;
	/// <summary>
	/// 生效条件描述
	///</summary>
	public string StartDes;
	/// <summary>
	/// 消失条件描述
	///</summary>
	public string EndDes;
	/// <summary>
	/// UI图标
	///</summary>
	public string UiIcon;
	/// <summary>
	/// BUFF图标
	///</summary>
	public string Icon;
	/// <summary>
	/// 是否为异常状态buff
	///</summary>
	public int UnusualBuff;
	/// <summary>
	/// Buff图标显示优先级显示
	///</summary>
	public int DisplayPriority;
	/// <summary>
	/// 增减buff显示
	///</summary>
	public string AddReduce;
	/// <summary>
	/// Buff创建条件
	///</summary>
	public int[] SetUp;
	/// <summary>
	/// Buff挂载目标
	///</summary>
	public int[] MountTarget;
	/// <summary>
	/// 只对指定角色挂载
	///</summary>
	public int[] AppointRole;
	/// <summary>
	/// 是否显示Buff名称
	///</summary>
	public int ShowName;
	/// <summary>
	/// 战斗中是否显示buff图标
	///</summary>
	public int ShowIcon;
	/// <summary>
	/// buff文字资源
	///</summary>
	public string BuffResource;
	/// <summary>
	/// 类型
	///</summary>
	public int Type;
	/// <summary>
	/// 组ID
	///</summary>
	public int Grounp;
	/// <summary>
	/// 同类型组ID关系
	///</summary>
	public int[] GrounpType;
	/// <summary>
	/// 覆盖优先级
	///</summary>
	public int[] Priority;
	/// <summary>
	/// 叠加处理
	///</summary>
	public int[] Superposition;
	/// <summary>
	/// 效果1
	///</summary>
	public int[] BuffEff1;
	/// <summary>
	/// 效果1生效
	///</summary>
	public int[] Trigger1;
	/// <summary>
	/// 效果2
	///</summary>
	public int[] BuffEff2;
	/// <summary>
	/// 效果2生效
	///</summary>
	public int[] Trigger2;
	/// <summary>
	/// 效果3
	///</summary>
	public int[] BuffEff3;
	/// <summary>
	/// 效果3生效
	///</summary>
	public int[] Trigger3;
	/// <summary>
	/// 消失条件
	///</summary>
	public int[] Disappear;
	/// <summary>
	/// BUFF触发特效ID
	///</summary>
	public int[] BuffEff;
	/// <summary>
	/// BUFF触发动作
	///</summary>
	public string StartAction;
	/// <summary>
	/// BUFF持续特效ID
	///</summary>
	public int[] BuffConEff;
	/// <summary>
	/// BUFF持续动作ID
	///</summary>
	public string ContinuedAction;
	/// <summary>
	/// BUFF消失特效ID
	///</summary>
	public int[] BuffEndEff;
	/// <summary>
	/// BUFF结束动作
	///</summary>
	public string EndAction;
	/// <summary>
	/// Buff类型
	///</summary>
	public int BuffType;
	/// <summary>
	/// 是否可清除
	///</summary>
	public int IfClear;
	/// <summary>
	/// 技能Cd
	///</summary>
	public int[] BuffCd;
	/// <summary>
	/// 是否可清除
	///</summary>
	public int DieSet;
	/// <summary>
	/// 是否飘字
	///</summary>
	public int ResultsShow;
	/// <summary>
	/// buff类型
	///</summary>
	public int SpeciallBuff;

	public override void Read(){
		Id=ReadInt();
		Name=ReadString();
		Des=ReadString();
		StartDes=ReadString();
		EndDes=ReadString();
		UiIcon=ReadString();
		Icon=ReadString();
		UnusualBuff=ReadInt();
		DisplayPriority=ReadInt();
		AddReduce=ReadString();
		SetUp=ReadArrayInt();
		MountTarget=ReadArrayInt();
		AppointRole=ReadArrayInt();
		ShowName=ReadInt();
		ShowIcon=ReadInt();
		BuffResource=ReadString();
		Type=ReadInt();
		Grounp=ReadInt();
		GrounpType=ReadArrayInt();
		Priority=ReadArrayInt();
		Superposition=ReadArrayInt();
		BuffEff1=ReadArrayInt();
		Trigger1=ReadArrayInt();
		BuffEff2=ReadArrayInt();
		Trigger2=ReadArrayInt();
		BuffEff3=ReadArrayInt();
		Trigger3=ReadArrayInt();
		Disappear=ReadArrayInt();
		BuffEff=ReadArrayInt();
		StartAction=ReadString();
		BuffConEff=ReadArrayInt();
		ContinuedAction=ReadString();
		BuffEndEff=ReadArrayInt();
		EndAction=ReadString();
		BuffType=ReadInt();
		IfClear=ReadInt();
		BuffCd=ReadArrayInt();
		DieSet=ReadInt();
		ResultsShow=ReadInt();
		SpeciallBuff=ReadInt();

	}
	
	public override void Write (){
		WriteInt(Id);
		WriteString(Name);
		WriteString(Des);
		WriteString(StartDes);
		WriteString(EndDes);
		WriteString(UiIcon);
		WriteString(Icon);
		WriteInt(UnusualBuff);
		WriteInt(DisplayPriority);
		WriteString(AddReduce);
		WriteArrayInt(SetUp);
		WriteArrayInt(MountTarget);
		WriteArrayInt(AppointRole);
		WriteInt(ShowName);
		WriteInt(ShowIcon);
		WriteString(BuffResource);
		WriteInt(Type);
		WriteInt(Grounp);
		WriteArrayInt(GrounpType);
		WriteArrayInt(Priority);
		WriteArrayInt(Superposition);
		WriteArrayInt(BuffEff1);
		WriteArrayInt(Trigger1);
		WriteArrayInt(BuffEff2);
		WriteArrayInt(Trigger2);
		WriteArrayInt(BuffEff3);
		WriteArrayInt(Trigger3);
		WriteArrayInt(Disappear);
		WriteArrayInt(BuffEff);
		WriteString(StartAction);
		WriteArrayInt(BuffConEff);
		WriteString(ContinuedAction);
		WriteArrayInt(BuffEndEff);
		WriteString(EndAction);
		WriteInt(BuffType);
		WriteInt(IfClear);
		WriteArrayInt(BuffCd);
		WriteInt(DieSet);
		WriteInt(ResultsShow);
		WriteInt(SpeciallBuff);

	}

	public System.Object Clone()  
	{
		return this.MemberwiseClone();
	}
}

public partial class T_BuffMgr{
	public Dictionary<int,T_Buff> itemData = new Dictionary<int, T_Buff>();
	//public Dictionary<int,List<T_Buff>> groupData = new Dictionary<int,List<T_Buff>>();
	public int size;

	public T_BuffMgr(){
		//ReadConfig ();
	}

	public IEnumerator ReadConfig(){
		string path = PathUtil.StreamingPath("Config/T_Buff.bytes");
		WWW data = new WWW (path);
		yield return data;
		MemoryStream stream = new MemoryStream (data.bytes);
		stream.Position = 0;
		
		T_Buff byteObj = new T_Buff ();
		byteObj.SetStream (stream);
		size = byteObj.ReadInt ();
		
		for (int i=0; i<size; i++) {
			T_Buff item = new T_Buff();
			item.Deserialization(stream);
			
	//		if(!groupData.ContainsKey()){
	//			groupData[] = new List<T_Buff>();
	//		}
	//		groupData[].Add(item);

	      if(itemData.ContainsKey(item.Id))Loger.Error("T_Buff is Repeat KEY = "+item.Id);
	      itemData[item.Id]=item;
		}

		Loger.Info ("T_Buff Config load Complete, size:"+size);
	}

	public T_Buff GetData(int Id){
		int id=Id;
		if (itemData.ContainsKey (id)) {
			return itemData[id];
		}
  		//WorldMgr.ShowMessageLog("T_Buff not find >> id:"+id);
		Loger.Error ("T_Buff not find >> id:"+id);
		return null;
	}
	
	//public List<T_Buff> GetGroup(){
	//	
	//	if (groupData.ContainsKey (id)) {
	//		return groupData[id];
	//	}
  	//	//WorldMgr.ShowMessageLog("T_Buff not find >> id:"+id);
	//	Loger.Error ("T_Buff not find Group >> id:"+id);
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
