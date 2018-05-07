using UnityEngine;
using System.IO;
using System.Collections;
using System.Collections.Generic;

public partial class T_Etc : ByteBase {
	public enum ID{
		NONE		=	0,
		TestButton		=	1,
		AttackShape		=	2,
		CtMax		=	3,
		EnMax		=	4,
		EnSlowlyTime		=	5,
		EnSlowlySpeed		=	6,
		EnSlowlyFirstSpeed		=	7,
		EnSlowlyAddSpeed		=	8,
		EnSlowlyMaxSpeed		=	9,
		SkillEn		=	10,
		FulldefDelay		=	11,
		FulldefRecover		=	12,
		GameLimt		=	13,
		GameLimtTime		=	14,
		PVEGameLimtTime		=	15,
		i10016		=	16,
		i10017		=	17,
		i10018		=	18,
		ExpAdd		=	19,
		PhyBreakdef		=	20,
		PhyInclined		=	21,
		MagBreakdef		=	22,
		MagInclined		=	23,
		AtkDelay		=	24,
		CameraValue		=	25,
		BlueSoul		=	26,
		RedSoul		=	27,
		GravityAcceleration		=	28,
		HitStop		=	29,
		JumpLimit		=	30,
		FallTime		=	31,
		CameraMove		=	32,
		LightBallFly		=	33,
		CameraMoveSmooth		=	34,
		BigSkillCancel		=	35,
		HpRemind		=	36,
		GhostDelayiTime		=	37,
		GhostInitiaV		=	38,
		GhostAccelerate		=	39,
		DodgeTime		=	40,
		DeathEffects		=	41,
		ButtonSound		=	42,
		upRate		=	43,
		downRate		=	44,
		UPEffects		=	45,
		DOWNEffects		=	46,
		crashEffects		=	47,
		PhysicalStrength		=	48,
		CelebrateAction		=	49,
		WinCamera		=	50,
		PVPAttackSection		=	51,
		HighAttack		=	52,
		LowAttack		=	53,
		PVPLevelSection		=	54,
		HighLevel		=	55,
		LowLevel		=	56,
		ExpNum		=	57,
		CityCameraBase		=	58,
		CityCameraLimit		=	59,
		CityBornPosition		=	60,
		ChangeNameDiamonds		=	61,
		SkyBreak		=	62,
		MailKeepingTime		=	63,
		ReadMailTime		=	64,
		MailWordsLimit		=	65,
		MailLimit		=	66,
		PartsOpen		=	67,
		FriendsLimit		=	68,
		RecentContect		=	69,
		GiveFriendPoint		=	70,
		GetFriendPoint		=	71,
		FriendPointOneTime		=	72,
		GodWeaponMaxRank		=	73,
		MailFee		=	74,
		DropType		=	75,
		TransEffect		=	76,
		CallEffect		=	77,
		CallPositionOne		=	78,
		CallPositionTwo		=	79,
		N_R_SR_SSR		=	80,
		N_R_SR		=	81,
		N_R_SSR		=	82,
		N_SR_SSR		=	83,
		N_SSR		=	84,
		N_SR		=	85,
		N		=	86,
		R_SR_SSR		=	87,
		R_SSR		=	88,
		R_SR		=	89,
		R		=	90,
		SR_SSR		=	91,
		SR		=	92,
		SSR		=	93,
		WorldChatLV		=	94,
		SellChatLV		=	95,
		GuardChatLV		=	96,
		ChatCD		=	97,
		SellChatCD		=	98,
		GuardChatCD		=	99,
		PersonChatCD		=	100,
		CoinEnterLimit		=	101,
		SelectEffect		=	102,
		CounterEffect		=	103,
		TowerChance		=	104,
		TeamWarning		=	105,
		RoleWarning		=	106,
		HolyOpenLevel		=	107,
		HolyRoleLevel		=	108,
		HolyConvertExp		=	109,
		MainCityHighLimit		=	110,
		MainCityLowLimit		=	111,
		AreaTypeTLbasePos		=	112,
		AreaTypeTRbasePos		=	113,
		AreaTypeBLbasePos		=	114,
		AreaTypeBRbasePos		=	115,
		AreaName		=	116,
		AreaTime		=	117,
		GuarderPosition		=	118,
		OpenTechnica		=	119,
		PvpOpenRank		=	120,
		PvpTime		=	121,
		PvpRewardTime		=	122,
		PvpRewardNumber		=	123,
		PvpRefresh		=	124,
		PvpAttendNumber		=	125,
		PvpSeasonTime		=	126,
		PvpInitialScore		=	127,
		InvitePkValidTime		=	128,
		PlayerRole		=	129,
		 OpenTechnical		=	130,
		UsePropEffect		=	131,
		UseSkillEffect		=	132,
		UsePropRiseTime		=	133,
		UsePropStopTime		=	134,
		UsePropFlyTime		=	135,
		ResourceAwardActor		=	136,
		Sound_UI_Close		=	137,
		Sound_UI_Return		=	138,
		Sound_Email_Prompt		=	139,
		Sound_Chat_Prompt		=	140,
		Sound_Spread2		=	141,
		Sound_AlbumUnlock		=	142,
		Sound_CommonSureBtnId		=	143,
		Sound_CommonCancelBtnId		=	144,
		BreakSkyEffect		=	145,
		BreakGroundEffect		=	146,
		FloatEffect		=	147,
		SevendayModel		=	148,
		CityleisureTime		=	149,
		TaskOpenLevel		=	150,
		ExploreTaskTimeRefresh		=	151,
		ExtraExploreTask		=	152,
		ExploreDiamondConsume		=	153,
		ExploreSkillSuccess		=	154,
		FriendsReferenceLV		=	155,
		MaxTeamLV		=	156,
		ExploreFriendUnlog		=	157,
		MainCityCameraMinYAngle		=	158,
		MainCityCameraMaxYAngle		=	159,
		WorldCamera		=	160,
		RankingListOpenLv		=	161,
		World_Role_Move_Speed		=	162,
		SpellBreakResult		=	163,
		PhysicsBreakResult		=	164,
		CardPrice		=	165,
		AbsoluteCard_A		=	166,
		AbsoluteCard_B		=	167,
		Strength_Rank		=	168,
		LoadingSpeed		=	169,
		DialogSave		=	170,
		ChannelBusyStage		=	171,
		BuffParameter1		=	172,
		BuffParameter2		=	173,
		GodWeaponItemExp		=	174,
		UsePropSelectEffect		=	175,
		PlayerTeamAutoModeDefaultAIGroup		=	176,
		PlayerTeamAutoModeDefaultAIGroupPVP		=	177,
		AutoFightOpenRank		=	178,
		LeadLeisureTime		=	179,
		TimeToGetPower		=	180,
		FreeChanceForCardpool		=	181,
		CDForCardpool		=	182,
		FirstFightParagraph		=	183,
		FirstFightScene		=	184,
		NextRecharge		=	185,
		RechargeTime		=	186,
		SuppressionCoefficient		=	187,
		BossWarOpen		=	188,
		CTPlus		=	189,
		ENPlus		=	190,
		FairyLossRate		=	191,
		GameSpeed		=	192,
		FairyPricePreMintue		=	193,
		ExpeditionChallengeTime		=	194,
		ExpeditionRoleRank		=	195,
		ExpeditionRefreshCost		=	196,
		BossWarReduceTimePreDiamond		=	197,
		AthleticSettlementTime		=	198,
		AthleticChallengeReward		=	199,
		AthleticChallengeNumber		=	200,
		AthleticPurchaseConsumption		=	201,
		AthleticRefreshTime		=	202,
		AthleticBattleMap		=	203,
	}

	/// <summary>
	/// Id
	///</summary>
	public ID Id;
	/// <summary>
	/// 说明
	///</summary>
	public string Desc;
	/// <summary>
	/// int参数
	///</summary>
	public int IntValue;
	/// <summary>
	/// int[]参数
	///</summary>
	public int[] IntArray;

	public override void Read(){
		Id=(ID)ReadShort();
		Desc=ReadString();
		IntValue=ReadInt();
		IntArray=ReadArrayInt();

	}
	
	public override void Write (){
		WriteShort((short)Id);
		WriteString(Desc);
		WriteInt(IntValue);
		WriteArrayInt(IntArray);

	}

	public System.Object Clone()  
	{
		return this.MemberwiseClone();
	}
}

public partial class T_EtcMgr{
	public Dictionary<short,T_Etc> itemData = new Dictionary<short, T_Etc>();
	//public Dictionary<int,List<T_Etc>> groupData = new Dictionary<int,List<T_Etc>>();
	public int size;

	public T_EtcMgr(){
		//ReadConfig ();
	}

	public IEnumerator ReadConfig(){
		string path = PathUtil.StreamingPath("Config/T_Etc.bytes");
		WWW data = new WWW (path);
		yield return data;
		MemoryStream stream = new MemoryStream (data.bytes);
		stream.Position = 0;
		
		T_Etc byteObj = new T_Etc ();
		byteObj.SetStream (stream);
		size = byteObj.ReadInt ();
		
		for (int i=0; i<size; i++) {
			T_Etc item = new T_Etc();
			item.Deserialization(stream);
			
	//		if(!groupData.ContainsKey()){
	//			groupData[] = new List<T_Etc>();
	//		}
	//		groupData[].Add(item);

	      if(itemData.ContainsKey((short)item.Id))Loger.Error("T_Etc is Repeat KEY = "+(short)item.Id);
	      itemData[(short)item.Id]=item;
		}

		Loger.Info ("T_Etc Config load Complete, size:"+size);
	}

	public T_Etc GetData(T_Etc.ID Id){
		short id=(short)Id;
		if (itemData.ContainsKey (id)) {
			return itemData[id];
		}
  		//WorldMgr.ShowMessageLog("T_Etc not find >> id:"+id);
		Loger.Error ("T_Etc not find >> id:"+id);
		return null;
	}
	
	//public List<T_Etc> GetGroup(){
	//	
	//	if (groupData.ContainsKey (id)) {
	//		return groupData[id];
	//	}
  	//	//WorldMgr.ShowMessageLog("T_Etc not find >> id:"+id);
	//	Loger.Error ("T_Etc not find Group >> id:"+id);
	//	return null;
	//}

	public bool HasData(T_Etc.ID Id){
		short id=(short)Id;
		return itemData.ContainsKey (id);
	}
	
	//public bool HasList(){
	//	
	//	return groupData.ContainsKey (id);
	//}
}
