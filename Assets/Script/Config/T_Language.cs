using UnityEngine;
using System.IO;
using System.Collections;
using System.Collections.Generic;

public partial class T_Language : ByteBase {

	/// <summary>
	/// 提示Id
	///</summary>
	public int Id;
	/// <summary>
	/// 相关提示语
	///</summary>
	public string Prompt;

	public override void Read(){
		Id=ReadInt();
		Prompt=ReadString();

	}
	
	public override void Write (){
		WriteInt(Id);
		WriteString(Prompt);

	}

	public System.Object Clone()  
	{
		return this.MemberwiseClone();
	}
}

public partial class T_LanguageMgr{
	public Dictionary<int,T_Language> itemData = new Dictionary<int, T_Language>();
	//public Dictionary<int,List<T_Language>> groupData = new Dictionary<int,List<T_Language>>();
	public int size;

	public T_LanguageMgr(){
		//ReadConfig ();
	}

	public IEnumerator ReadConfig(){
		string path = PathUtil.StreamingPath("Config/T_Language.bytes");
		WWW data = new WWW (path);
		yield return data;
		MemoryStream stream = new MemoryStream (data.bytes);
		stream.Position = 0;
		
		T_Language byteObj = new T_Language ();
		byteObj.SetStream (stream);
		size = byteObj.ReadInt ();
		
		for (int i=0; i<size; i++) {
			T_Language item = new T_Language();
			item.Deserialization(stream);
			
	//		if(!groupData.ContainsKey()){
	//			groupData[] = new List<T_Language>();
	//		}
	//		groupData[].Add(item);

	      if(itemData.ContainsKey(item.Id))Loger.Error("T_Language is Repeat KEY = "+item.Id);
	      itemData[item.Id]=item;
		}

		Loger.Info ("T_Language Config load Complete, size:"+size);
	}

	public T_Language GetData(int Id){
		int id=Id;
		if (itemData.ContainsKey (id)) {
			return itemData[id];
		}
  		//WorldMgr.ShowMessageLog("T_Language not find >> id:"+id);
		Loger.Error ("T_Language not find >> id:"+id);
		return null;
	}
	
	//public List<T_Language> GetGroup(){
	//	
	//	if (groupData.ContainsKey (id)) {
	//		return groupData[id];
	//	}
  	//	//WorldMgr.ShowMessageLog("T_Language not find >> id:"+id);
	//	Loger.Error ("T_Language not find Group >> id:"+id);
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
