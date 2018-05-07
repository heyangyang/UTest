using UnityEngine;
using System.IO;
using System.Collections;
using System.Collections.Generic;

public partial class T_ActorRes : ByteBase {

	/// <summary>
	/// 资源唯一ID
	///</summary>
	public int Id;
	/// <summary>
	/// 战斗角色模型
	///</summary>
	public string ModelRole;
	/// <summary>
	/// 右手武器模型
	///</summary>
	public string[] WeaponModel;
	/// <summary>
	/// 左手武器模型
	///</summary>
	public string[] LeftWeapon;
	/// <summary>
	/// 战斗进阶右神器
	///</summary>
	public string[] WeaponModelTwo;
	/// <summary>
	/// 战斗进阶左神器
	///</summary>
	public string[] LeftGodWeapon;
	/// <summary>
	/// 神器系统显示模型右手
	///</summary>
	public string[] WeaponModel2;
	/// <summary>
	/// 神器系统显示模型左手
	///</summary>
	public string[] LeftWeapon2;
	/// <summary>
	/// 进阶后神器显示模型右手
	///</summary>
	public string[] WeaponModelTwo2;
	/// <summary>
	/// 进阶后神器显示模型左手
	///</summary>
	public string[] LeftGodWeapon2;
	/// <summary>
	/// 神器每一阶的原画
	///</summary>
	public string[] WeaponPic;
	/// <summary>
	/// 武器动画类型
	///</summary>
	public int WeaponType;
	/// <summary>
	/// 模型缩放倍数
	///</summary>
	public int ZoomRatio;
	/// <summary>
	/// 角色头像
	///</summary>
	public string IconRole;
	/// <summary>
	/// 角色大招立绘
	///</summary>
	public string SkillPicture;
	/// <summary>
	/// 角色立绘
	///</summary>
	public string HeroPicture;
	/// <summary>
	/// 角色性别
	///</summary>
	public int ActorGender;
	/// <summary>
	/// 死亡特效
	///</summary>
	public int EffDead;
	/// <summary>
	/// 受击特效
	///</summary>
	public int RffHit;
	/// <summary>
	/// 受击碰撞半径
	///</summary>
	public int Impact;
	/// <summary>
	/// 选中特效缩放倍数
	///</summary>
	public int SelectZoom;
	/// <summary>
	/// 阴影缩放倍数
	///</summary>
	public int ShadowZoom;
	/// <summary>
	/// 模型高度
	///</summary>
	public int Height;
	/// <summary>
	/// 展示动作
	///</summary>
	public string[] ShowAction;
	/// <summary>
	/// 阵型待机动作
	///</summary>
	public string TeamAction;
	/// <summary>
	/// 预加载特效资源
	///</summary>
	public int[] PreloadingEffect;
	/// <summary>
	/// 预加载音效资源
	///</summary>
	public int[] PreloadingSound;
	/// <summary>
	/// 死亡音效
	///</summary>
	public int SoundDead;
	/// <summary>
	/// 受击语音
	///</summary>
	public int HitVoice;
	/// <summary>
	/// 昏迷语音
	///</summary>
	public int ComaVoice;
	/// <summary>
	/// 受击音效
	///</summary>
	public int HitSound;
	/// <summary>
	/// 暴击音效
	///</summary>
	public int CritSound;
	/// <summary>
	/// 格挡音效
	///</summary>
	public int BlockSound;
	/// <summary>
	/// 英雄获得通用动作
	///</summary>
	public string[] GetHeroAction;
	/// <summary>
	/// 英雄获得动作旋转参数
	///</summary>
	public int YRotation;
	/// <summary>
	/// 英雄获得动作高度
	///</summary>
	public int GetHeight;
	/// <summary>
	/// 使用道具
	///</summary>
	public int[] ToolPosition;
	/// <summary>
	/// 法师技能释放语音
	///</summary>
	public int[] MagicSkillVoice;
	/// <summary>
	/// 队长战斗开始
	///</summary>
	public int LeaderStartVoice;
	/// <summary>
	/// 庆祝MVP语音
	///</summary>
	public int CelebrateVoice;
	/// <summary>
	/// 击飞语音
	///</summary>
	public int FlyVoice;
	/// <summary>
	/// 击倒语音
	///</summary>
	public int DwonVoice;
	/// <summary>
	/// 爬起语音
	///</summary>
	public int UpVoice;
	/// <summary>
	/// 死亡语音
	///</summary>
	public int DieVoice;
	/// <summary>
	/// 防御受击语音
	///</summary>
	public int DefenseVoice;
	/// <summary>
	/// 破防语音
	///</summary>
	public int BrokenVoice;
	/// <summary>
	/// 被防御反弹语音
	///</summary>
	public int DefensiveVoice;
	/// <summary>
	/// 闪避语音
	///</summary>
	public int BlockVoice;
	/// <summary>
	/// 大招语音
	///</summary>
	public int BigVoice;
	/// <summary>
	/// 抽卡阵型图鉴语音
	///</summary>
	public int EnterVoice;
	/// <summary>
	/// 调戏语音
	///</summary>
	public int FlirtVoice;
	/// <summary>
	/// 举起语音
	///</summary>
	public int LiftVoice;
	/// <summary>
	/// 投掷语音
	///</summary>
	public int ThrowVoice;
	/// <summary>
	/// 角色通用动作特效
	///</summary>
	public string[] GeneralEffect;
	/// <summary>
	/// 守护妖精缩放比例
	///</summary>
	public int FairySclae;
	/// <summary>
	/// 大招名称资源
	///</summary>
	public string BigSkillName;
	/// <summary>
	/// 阵形替换标志位置
	///</summary>
	public int SignPos;

	public override void Read(){
		Id=ReadInt();
		ModelRole=ReadString();
		WeaponModel=ReadArrayStr();
		LeftWeapon=ReadArrayStr();
		WeaponModelTwo=ReadArrayStr();
		LeftGodWeapon=ReadArrayStr();
		WeaponModel2=ReadArrayStr();
		LeftWeapon2=ReadArrayStr();
		WeaponModelTwo2=ReadArrayStr();
		LeftGodWeapon2=ReadArrayStr();
		WeaponPic=ReadArrayStr();
		WeaponType=ReadInt();
		ZoomRatio=ReadInt();
		IconRole=ReadString();
		SkillPicture=ReadString();
		HeroPicture=ReadString();
		ActorGender=ReadInt();
		EffDead=ReadInt();
		RffHit=ReadInt();
		Impact=ReadInt();
		SelectZoom=ReadInt();
		ShadowZoom=ReadInt();
		Height=ReadInt();
		ShowAction=ReadArrayStr();
		TeamAction=ReadString();
		PreloadingEffect=ReadArrayInt();
		PreloadingSound=ReadArrayInt();
		SoundDead=ReadInt();
		HitVoice=ReadInt();
		ComaVoice=ReadInt();
		HitSound=ReadInt();
		CritSound=ReadInt();
		BlockSound=ReadInt();
		GetHeroAction=ReadArrayStr();
		YRotation=ReadInt();
		GetHeight=ReadInt();
		ToolPosition=ReadArrayInt();
		MagicSkillVoice=ReadArrayInt();
		LeaderStartVoice=ReadInt();
		CelebrateVoice=ReadInt();
		FlyVoice=ReadInt();
		DwonVoice=ReadInt();
		UpVoice=ReadInt();
		DieVoice=ReadInt();
		DefenseVoice=ReadInt();
		BrokenVoice=ReadInt();
		DefensiveVoice=ReadInt();
		BlockVoice=ReadInt();
		BigVoice=ReadInt();
		EnterVoice=ReadInt();
		FlirtVoice=ReadInt();
		LiftVoice=ReadInt();
		ThrowVoice=ReadInt();
		GeneralEffect=ReadArrayStr();
		FairySclae=ReadInt();
		BigSkillName=ReadString();
		SignPos=ReadInt();

	}
	
	public override void Write (){
		WriteInt(Id);
		WriteString(ModelRole);
		WriteArrayStr(WeaponModel);
		WriteArrayStr(LeftWeapon);
		WriteArrayStr(WeaponModelTwo);
		WriteArrayStr(LeftGodWeapon);
		WriteArrayStr(WeaponModel2);
		WriteArrayStr(LeftWeapon2);
		WriteArrayStr(WeaponModelTwo2);
		WriteArrayStr(LeftGodWeapon2);
		WriteArrayStr(WeaponPic);
		WriteInt(WeaponType);
		WriteInt(ZoomRatio);
		WriteString(IconRole);
		WriteString(SkillPicture);
		WriteString(HeroPicture);
		WriteInt(ActorGender);
		WriteInt(EffDead);
		WriteInt(RffHit);
		WriteInt(Impact);
		WriteInt(SelectZoom);
		WriteInt(ShadowZoom);
		WriteInt(Height);
		WriteArrayStr(ShowAction);
		WriteString(TeamAction);
		WriteArrayInt(PreloadingEffect);
		WriteArrayInt(PreloadingSound);
		WriteInt(SoundDead);
		WriteInt(HitVoice);
		WriteInt(ComaVoice);
		WriteInt(HitSound);
		WriteInt(CritSound);
		WriteInt(BlockSound);
		WriteArrayStr(GetHeroAction);
		WriteInt(YRotation);
		WriteInt(GetHeight);
		WriteArrayInt(ToolPosition);
		WriteArrayInt(MagicSkillVoice);
		WriteInt(LeaderStartVoice);
		WriteInt(CelebrateVoice);
		WriteInt(FlyVoice);
		WriteInt(DwonVoice);
		WriteInt(UpVoice);
		WriteInt(DieVoice);
		WriteInt(DefenseVoice);
		WriteInt(BrokenVoice);
		WriteInt(DefensiveVoice);
		WriteInt(BlockVoice);
		WriteInt(BigVoice);
		WriteInt(EnterVoice);
		WriteInt(FlirtVoice);
		WriteInt(LiftVoice);
		WriteInt(ThrowVoice);
		WriteArrayStr(GeneralEffect);
		WriteInt(FairySclae);
		WriteString(BigSkillName);
		WriteInt(SignPos);

	}

	public System.Object Clone()  
	{
		return this.MemberwiseClone();
	}
}

public partial class T_ActorResMgr{
	public Dictionary<int,T_ActorRes> itemData = new Dictionary<int, T_ActorRes>();
	//public Dictionary<int,List<T_ActorRes>> groupData = new Dictionary<int,List<T_ActorRes>>();
	public int size;

	public T_ActorResMgr(){
		//ReadConfig ();
	}

	public IEnumerator ReadConfig(){
		string path = PathUtil.StreamingPath("Config/T_ActorRes.bytes");
		WWW data = new WWW (path);
		yield return data;
		MemoryStream stream = new MemoryStream (data.bytes);
		stream.Position = 0;
		
		T_ActorRes byteObj = new T_ActorRes ();
		byteObj.SetStream (stream);
		size = byteObj.ReadInt ();
		
		for (int i=0; i<size; i++) {
			T_ActorRes item = new T_ActorRes();
			item.Deserialization(stream);
			
	//		if(!groupData.ContainsKey()){
	//			groupData[] = new List<T_ActorRes>();
	//		}
	//		groupData[].Add(item);

	      if(itemData.ContainsKey(item.Id))Loger.Error("T_ActorRes is Repeat KEY = "+item.Id);
	      itemData[item.Id]=item;
		}

		Loger.Info ("T_ActorRes Config load Complete, size:"+size);
	}

	public T_ActorRes GetData(int Id){
		int id=Id;
		if (itemData.ContainsKey (id)) {
			return itemData[id];
		}
  		//WorldMgr.ShowMessageLog("T_ActorRes not find >> id:"+id);
		Loger.Error ("T_ActorRes not find >> id:"+id);
		return null;
	}
	
	//public List<T_ActorRes> GetGroup(){
	//	
	//	if (groupData.ContainsKey (id)) {
	//		return groupData[id];
	//	}
  	//	//WorldMgr.ShowMessageLog("T_ActorRes not find >> id:"+id);
	//	Loger.Error ("T_ActorRes not find Group >> id:"+id);
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
