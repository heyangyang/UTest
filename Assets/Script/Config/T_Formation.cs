using UnityEngine;
using System.IO;
using System.Collections;
using System.Collections.Generic;

public partial class T_Formation : ByteBase {

	/// <summary>
	/// Id
	///</summary>
	public int Id;
	/// <summary>
	/// 阵型名称
	///</summary>
	public string Name;
	/// <summary>
	/// 阵型类型
	///</summary>
	public int Type;
	/// <summary>
	/// 阵型描述
	///</summary>
	public string Description;
	/// <summary>
	/// 敌我位置
	///</summary>
	public int Location;
	/// <summary>
	/// 1号位规则
	///</summary>
	public int[] RuleOne;
	/// <summary>
	/// 2号位规则
	///</summary>
	public int[] RuleTwo;
	/// <summary>
	/// 3号位规则
	///</summary>
	public int[] RuleThree;
	/// <summary>
	/// 4号位规则
	///</summary>
	public int[] RuleFour;
	/// <summary>
	/// 5号位规则
	///</summary>
	public int[] RuleFive;
	/// <summary>
	/// 6号位规则
	///</summary>
	public int[] RuleSix;
	/// <summary>
	/// 7号位规则
	///</summary>
	public int[] RuleSeven;
	/// <summary>
	/// 8号位规则
	///</summary>
	public int[] RuleEight;
	/// <summary>
	/// 9号位规则
	///</summary>
	public int[] RuleNine;
	/// <summary>
	/// 1号位置
	///</summary>
	public int[] NumOne;
	/// <summary>
	/// 2号位置
	///</summary>
	public int[] NumTwo;
	/// <summary>
	/// 3号位置
	///</summary>
	public int[] NumThree;
	/// <summary>
	/// 4号位置
	///</summary>
	public int[] NumFour;
	/// <summary>
	/// 5号位置
	///</summary>
	public int[] NumFive;
	/// <summary>
	/// 6号位置
	///</summary>
	public int[] NumSix;
	/// <summary>
	/// 7号位置
	///</summary>
	public int[] NumSeven;
	/// <summary>
	/// 8号位置
	///</summary>
	public int[] NumEight;
	/// <summary>
	/// 9号位置
	///</summary>
	public int[] NumNine;
	/// <summary>
	/// 守护神坐标
	///</summary>
	public int[] Guarder;
	/// <summary>
	/// 防战位置
	///</summary>
	public int GuardPos;
	/// <summary>
	/// 前排位置
	///</summary>
	public int[] FrontPos;
	/// <summary>
	/// 后排位置
	///</summary>
	public int[] BackPos;
	/// <summary>
	/// 位号顺序
	///</summary>
	public int[] PosOrder;

	public override void Read(){
		Id=ReadInt();
		Name=ReadString();
		Type=ReadInt();
		Description=ReadString();
		Location=ReadInt();
		RuleOne=ReadArrayInt();
		RuleTwo=ReadArrayInt();
		RuleThree=ReadArrayInt();
		RuleFour=ReadArrayInt();
		RuleFive=ReadArrayInt();
		RuleSix=ReadArrayInt();
		RuleSeven=ReadArrayInt();
		RuleEight=ReadArrayInt();
		RuleNine=ReadArrayInt();
		NumOne=ReadArrayInt();
		NumTwo=ReadArrayInt();
		NumThree=ReadArrayInt();
		NumFour=ReadArrayInt();
		NumFive=ReadArrayInt();
		NumSix=ReadArrayInt();
		NumSeven=ReadArrayInt();
		NumEight=ReadArrayInt();
		NumNine=ReadArrayInt();
		Guarder=ReadArrayInt();
		GuardPos=ReadInt();
		FrontPos=ReadArrayInt();
		BackPos=ReadArrayInt();
		PosOrder=ReadArrayInt();

	}
	
	public override void Write (){
		WriteInt(Id);
		WriteString(Name);
		WriteInt(Type);
		WriteString(Description);
		WriteInt(Location);
		WriteArrayInt(RuleOne);
		WriteArrayInt(RuleTwo);
		WriteArrayInt(RuleThree);
		WriteArrayInt(RuleFour);
		WriteArrayInt(RuleFive);
		WriteArrayInt(RuleSix);
		WriteArrayInt(RuleSeven);
		WriteArrayInt(RuleEight);
		WriteArrayInt(RuleNine);
		WriteArrayInt(NumOne);
		WriteArrayInt(NumTwo);
		WriteArrayInt(NumThree);
		WriteArrayInt(NumFour);
		WriteArrayInt(NumFive);
		WriteArrayInt(NumSix);
		WriteArrayInt(NumSeven);
		WriteArrayInt(NumEight);
		WriteArrayInt(NumNine);
		WriteArrayInt(Guarder);
		WriteInt(GuardPos);
		WriteArrayInt(FrontPos);
		WriteArrayInt(BackPos);
		WriteArrayInt(PosOrder);

	}

	public System.Object Clone()  
	{
		return this.MemberwiseClone();
	}
}

public partial class T_FormationMgr{
	public Dictionary<int,T_Formation> itemData = new Dictionary<int, T_Formation>();
	//public Dictionary<int,List<T_Formation>> groupData = new Dictionary<int,List<T_Formation>>();
	public int size;

	public T_FormationMgr(){
		//ReadConfig ();
	}

	public IEnumerator ReadConfig(){
		string path = PathUtil.StreamingPath("Config/T_Formation.bytes");
		WWW data = new WWW (path);
		yield return data;
		MemoryStream stream = new MemoryStream (data.bytes);
		stream.Position = 0;
		
		T_Formation byteObj = new T_Formation ();
		byteObj.SetStream (stream);
		size = byteObj.ReadInt ();
		
		for (int i=0; i<size; i++) {
			T_Formation item = new T_Formation();
			item.Deserialization(stream);
			
	//		if(!groupData.ContainsKey()){
	//			groupData[] = new List<T_Formation>();
	//		}
	//		groupData[].Add(item);

	      if(itemData.ContainsKey(item.Id))Loger.Error("T_Formation is Repeat KEY = "+item.Id);
	      itemData[item.Id]=item;
		}

		Loger.Info ("T_Formation Config load Complete, size:"+size);
	}

	public T_Formation GetData(int Id){
		int id=Id;
		if (itemData.ContainsKey (id)) {
			return itemData[id];
		}
  		//WorldMgr.ShowMessageLog("T_Formation not find >> id:"+id);
		Loger.Error ("T_Formation not find >> id:"+id);
		return null;
	}
	
	//public List<T_Formation> GetGroup(){
	//	
	//	if (groupData.ContainsKey (id)) {
	//		return groupData[id];
	//	}
  	//	//WorldMgr.ShowMessageLog("T_Formation not find >> id:"+id);
	//	Loger.Error ("T_Formation not find Group >> id:"+id);
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
