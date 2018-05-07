using UnityEngine;
using System.IO;
using System.Collections;
using System.Collections.Generic;

public partial class T_Skill : ByteBase {
	public enum STATETYPE{
		NONE		=	0,
		STAND		=	1,
		WALK		=	2,
		DIE		=	3,
		HURT		=	4,
		DEFENSE		=	5,
		HURT_FLY		=	6,
		HURT_DOWN		=	7,
		JUMP_BACK		=	8,
		PARRY		=	9,
		SLEEP		=	10,
		CLIMB		=	11,
		HURT_FRONT		=	12,
		SKILL		=	13,
		DEFENSE_HURT		=	14,
		DIE_DOWN		=	15,
		DOWN_HURT		=	16,
		DOWN_IDLE		=	17,
		JUMP_FRONT		=	18,
		COUNTERATTACK		=	19,
		DEFENSE_BREAK		=	20,
		DEFENSE_REBOUND		=	21,
		UPSKY		=	22,
		DOWNLAND		=	23,
		HIDE		=	24,
		CELEBRATION		=	25,
		CELEBRATION_STAND		=	26,
		ENTER		=	27,
		ATTK_WAIT		=	28,
		HURT_WAIT		=	29,
		STAND_WAIT		=	30,
		DODE_WAIT		=	31,
		RUNAWAY		=	32,
		SUMMONED_SHOW		=	33,
		SUMMONED_HIDE		=	34,
		JUMP_ATTACK		=	35,
		JUMP		=	36,
		FIND_PATH		=	37,
		RELIVE		=	38,
		STUN		=	39,
		READY_SUPER		=	40,
		LEISURE		=	41,
		PETRIFACTION		=	42,
	}

	/// <summary>
	/// Id
	///</summary>
	public int Id;
	/// <summary>
	/// 动作名
	///</summary>
	public string Name;
	/// <summary>
	/// 图标
	///</summary>
	public string Icon;
	/// <summary>
	/// 界面图标
	///</summary>
	public string UIIcon;
	/// <summary>
	/// 技能描述
	///</summary>
	public string Describe;
	/// <summary>
	/// 状态
	///</summary>
	public STATETYPE StateType;
	/// <summary>
	/// 技能类型
	///</summary>
	public int Skilltype;
	/// <summary>
	/// 是否为主技能
	///</summary>
	public int MasterSkill;
	/// <summary>
	/// 技能伤害类型
	///</summary>
	public int SkillHurtType;
	/// <summary>
	/// 播放动作
	///</summary>
	public string[] Animation;
	/// <summary>
	/// 动作时长
	///</summary>
	public int[] Time;
	/// <summary>
	/// 连接技能
	///</summary>
	public int NextSkill;
	/// <summary>
	/// 可连招时间
	///</summary>
	public int NewSkillTime;
	/// <summary>
	/// 移动速度
	///</summary>
	public int Speed;
	/// <summary>
	/// 释放距离
	///</summary>
	public int Distance;
	/// <summary>
	/// 强制位移
	///</summary>
	public int[] Displacement;
	/// <summary>
	/// 技能移动
	///</summary>
	public int[] SkillMove;
	/// <summary>
	/// 施法特效
	///</summary>
	public int[] Effect;
	/// <summary>
	/// 飞行特效
	///</summary>
	public int[] FlyEffect;
	/// <summary>
	/// 飞行返回特效
	///</summary>
	public int[] FlyBackEffect;
	/// <summary>
	/// 结束旋转
	///</summary>
	public int EndRotating;
	/// <summary>
	/// 目标范围
	///</summary>
	public int[] Scope;
	/// <summary>
	/// 受击类型
	///</summary>
	public int Hurt;
	/// <summary>
	/// 受击特效
	///</summary>
	public int[] HurtEffect;
	/// <summary>
	/// 受击音效
	///</summary>
	public int Hitsound;
	/// <summary>
	/// 暴击音效
	///</summary>
	public int CritSound;
	/// <summary>
	/// 格挡音效
	///</summary>
	public int BlockSound;
	/// <summary>
	/// 受击震动
	///</summary>
	public int[] Shake;
	/// <summary>
	/// 摄像机
	///</summary>
	public int CameraAnima;
	/// <summary>
	/// 触发慢放状态
	///</summary>
	public int[] SlowState;
	/// <summary>
	/// 慢动作
	///</summary>
	public int[] Slow;
	/// <summary>
	/// 是否隐藏背景/角色
	///</summary>
	public int Hidden;
	/// <summary>
	/// 被防御能打断动作
	///</summary>
	public int DefenseEndAction;
	/// <summary>
	/// 攻击判定时间点
	///</summary>
	public int HitTime;
	/// <summary>
	/// 攻击判定类型
	///</summary>
	public int AtkType;
	/// <summary>
	/// 飞行物是否穿透
	///</summary>
	public int AtkThrough;
	/// <summary>
	/// 攻击角度/宽度
	///</summary>
	public int AtkSize1;
	/// <summary>
	/// 攻击半径/高
	///</summary>
	public int AtkSize2;
	/// <summary>
	/// 攻击仰角
	///</summary>
	public int AtkAngle;
	/// <summary>
	/// 攻击起始位置
	///</summary>
	public int[] AtkStart;
	/// <summary>
	/// 触发震屏状态
	///</summary>
	public int[] ScreenState;
	/// <summary>
	/// 震屏摄像机移动坐标X,Y,Z
	///</summary>
	public int[] MovingCoordinates;
	/// <summary>
	/// 被击角色闪白参数
	///</summary>
	public int[] TwinkleWhite;
	/// <summary>
	/// 播放语音
	///</summary>
	public int ActorSound;
	/// <summary>
	/// 播放音效
	///</summary>
	public int SoundId;
	/// <summary>
	/// 技能效果
	///</summary>
	public int[] BuffId;
	/// <summary>
	/// 敌方技能效果
	///</summary>
	public int EnemySkill;
	/// <summary>
	/// 我方技能效果
	///</summary>
	public int SelfSkill;
	/// <summary>
	/// 群攻目标点坐标
	///</summary>
	public int[] AttackCoordinate;
	/// <summary>
	/// 技能AI作用参数
	///</summary>
	public int[] SkillAIValue;

	public override void Read(){
		Id=ReadInt();
		Name=ReadString();
		Icon=ReadString();
		UIIcon=ReadString();
		Describe=ReadString();
		StateType=(STATETYPE)ReadShort();
		Skilltype=ReadInt();
		MasterSkill=ReadInt();
		SkillHurtType=ReadInt();
		Animation=ReadArrayStr();
		Time=ReadArrayInt();
		NextSkill=ReadInt();
		NewSkillTime=ReadInt();
		Speed=ReadInt();
		Distance=ReadInt();
		Displacement=ReadArrayInt();
		SkillMove=ReadArrayInt();
		Effect=ReadArrayInt();
		FlyEffect=ReadArrayInt();
		FlyBackEffect=ReadArrayInt();
		EndRotating=ReadInt();
		Scope=ReadArrayInt();
		Hurt=ReadInt();
		HurtEffect=ReadArrayInt();
		Hitsound=ReadInt();
		CritSound=ReadInt();
		BlockSound=ReadInt();
		Shake=ReadArrayInt();
		CameraAnima=ReadInt();
		SlowState=ReadArrayInt();
		Slow=ReadArrayInt();
		Hidden=ReadInt();
		DefenseEndAction=ReadInt();
		HitTime=ReadInt();
		AtkType=ReadInt();
		AtkThrough=ReadInt();
		AtkSize1=ReadInt();
		AtkSize2=ReadInt();
		AtkAngle=ReadInt();
		AtkStart=ReadArrayInt();
		ScreenState=ReadArrayInt();
		MovingCoordinates=ReadArrayInt();
		TwinkleWhite=ReadArrayInt();
		ActorSound=ReadInt();
		SoundId=ReadInt();
		BuffId=ReadArrayInt();
		EnemySkill=ReadInt();
		SelfSkill=ReadInt();
		AttackCoordinate=ReadArrayInt();
		SkillAIValue=ReadArrayInt();

	}
	
	public override void Write (){
		WriteInt(Id);
		WriteString(Name);
		WriteString(Icon);
		WriteString(UIIcon);
		WriteString(Describe);
		WriteShort((short)StateType);
		WriteInt(Skilltype);
		WriteInt(MasterSkill);
		WriteInt(SkillHurtType);
		WriteArrayStr(Animation);
		WriteArrayInt(Time);
		WriteInt(NextSkill);
		WriteInt(NewSkillTime);
		WriteInt(Speed);
		WriteInt(Distance);
		WriteArrayInt(Displacement);
		WriteArrayInt(SkillMove);
		WriteArrayInt(Effect);
		WriteArrayInt(FlyEffect);
		WriteArrayInt(FlyBackEffect);
		WriteInt(EndRotating);
		WriteArrayInt(Scope);
		WriteInt(Hurt);
		WriteArrayInt(HurtEffect);
		WriteInt(Hitsound);
		WriteInt(CritSound);
		WriteInt(BlockSound);
		WriteArrayInt(Shake);
		WriteInt(CameraAnima);
		WriteArrayInt(SlowState);
		WriteArrayInt(Slow);
		WriteInt(Hidden);
		WriteInt(DefenseEndAction);
		WriteInt(HitTime);
		WriteInt(AtkType);
		WriteInt(AtkThrough);
		WriteInt(AtkSize1);
		WriteInt(AtkSize2);
		WriteInt(AtkAngle);
		WriteArrayInt(AtkStart);
		WriteArrayInt(ScreenState);
		WriteArrayInt(MovingCoordinates);
		WriteArrayInt(TwinkleWhite);
		WriteInt(ActorSound);
		WriteInt(SoundId);
		WriteArrayInt(BuffId);
		WriteInt(EnemySkill);
		WriteInt(SelfSkill);
		WriteArrayInt(AttackCoordinate);
		WriteArrayInt(SkillAIValue);

	}

	public System.Object Clone()  
	{
		return this.MemberwiseClone();
	}
}

public partial class T_SkillMgr{
	public Dictionary<int,T_Skill> itemData = new Dictionary<int, T_Skill>();
	//public Dictionary<int,List<T_Skill>> groupData = new Dictionary<int,List<T_Skill>>();
	public int size;

	public T_SkillMgr(){
		//ReadConfig ();
	}

	public IEnumerator ReadConfig(){
		string path = PathUtil.StreamingPath("Config/T_Skill.bytes");
		WWW data = new WWW (path);
		yield return data;
		MemoryStream stream = new MemoryStream (data.bytes);
		stream.Position = 0;
		
		T_Skill byteObj = new T_Skill ();
		byteObj.SetStream (stream);
		size = byteObj.ReadInt ();
		
		for (int i=0; i<size; i++) {
			T_Skill item = new T_Skill();
			item.Deserialization(stream);
			
	//		if(!groupData.ContainsKey()){
	//			groupData[] = new List<T_Skill>();
	//		}
	//		groupData[].Add(item);

	      if(itemData.ContainsKey(item.Id))Loger.Error("T_Skill is Repeat KEY = "+item.Id);
	      itemData[item.Id]=item;
		}

		Loger.Info ("T_Skill Config load Complete, size:"+size);
	}

	public T_Skill GetData(int Id){
		int id=Id;
		if (itemData.ContainsKey (id)) {
			return itemData[id];
		}
  		//WorldMgr.ShowMessageLog("T_Skill not find >> id:"+id);
		Loger.Error ("T_Skill not find >> id:"+id);
		return null;
	}
	
	//public List<T_Skill> GetGroup(){
	//	
	//	if (groupData.ContainsKey (id)) {
	//		return groupData[id];
	//	}
  	//	//WorldMgr.ShowMessageLog("T_Skill not find >> id:"+id);
	//	Loger.Error ("T_Skill not find Group >> id:"+id);
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
