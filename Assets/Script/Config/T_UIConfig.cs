using UnityEngine;
using System.IO;
using System.Collections;
using System.Collections.Generic;

public partial class T_UIConfig : ByteBase {
	public enum E_UI{
		NONE		=	0,
		WIN_LOGIN		=	1,
		WIN_LOADING		=	2,
		TIP_SYSTEM		=	3,
		WIN_BATTLE		=	4,
		WIN_GOODINFO		=	5,
		WIN_BATTLE_USE		=	6,
		WIN_BATTLE_BIGSKILL		=	7,
		WIN_SETTLEMENT_LOSE		=	8,
		WIN_SETTLEMENT_WIN		=	9,
		WIN_SYSTEMONEALERT		=	10,
		WIN_SYSTEMTWOALERT		=	11,
		WIN_WAITLOADINGVIEW		=	12,
		WIN_MAINCITY		=	13,
		WIN_MATCHING		=	14,
		WIN_RoleView		=	15,
		WIN_SkillTip		=	16,
		TIP_BATTLESUSPENDVIEW		=	17,
		WIN_RoleUpgradeStarUpView		=	18,
		WIN_FORMATIONVIEW		=	19,
		WIN_BACKPACKVIEW		=	20,
		TIP_BatchTip		=	21,
		WIN_BUFFVIEW		=	22,
		TIP_BattleStrategyView		=	23,
		TIP_BattleBuffSupportView		=	24,
		WIN_ROLEBASEVIEW		=	25,
		WIN_CHANGEDNAMEVIEW		=	26,
		WIN_RoleEquipUpgradeStage		=	27,
		TIP_FightValueChangeTip		=	28,
		TIP_SellSuccessTip		=	29,
		TIP_SystemDescView		=	30,
		WIN_ArtifactSkillShowView		=	31,
		WIN_ArtifactPromptView		=	32,
		TIP_SkillLearnTip		=	33,
		WIN_FRIENDSVIEW		=	34,
		WIN_MailView		=	35,
		WIN_FRIENDAPPLYVIEW		=	36,
		TIP_BATTLE_POSITION		=	37,
		TIP_MailGet		=	38,
		WIN_FRIEND_LIST_VIEW		=	39,
		TIP_MailConsumeGold		=	40,
		TIP_MailSelGold		=	41,
		WIN_BackpackSelect		=	42,
		WIN_RunesView		=	43,
		WIN_CHAT_VIEW		=	44,
		Win_RunePageRenameView		=	45,
		TIP_RuneUnlock		=	46,
		TIP_RuneShow		=	47,
		WIN_GuarderView		=	48,
		TIP_GMVIEW		=	49,
		TIP_GMBTN		=	50,
		WIN_USERINFO_VIEW		=	51,
		TIP_SHOW_LOADING		=	52,
		WIN_TOWER_REWARD_VIEW		=	53,
		WIN_ENDLESSTOWER_VIEW		=	54,
		TIP_ShelvesTip		=	55,
		TIP_GuarderRankUp		=	56,
		WIN_TOWER_START_VIEW		=	57,
		TIP_MiNiItemTip		=	58,
		TIP_RuleTip		=	59,
		WIN_ALERT_ROLEINFO_VIEW		=	60,
		PLOT_VIEW		=	61,
		WIN_CardMainView2		=	62,
		WIN_HeroCardListView2		=	63,
		WIN_HeroCardDetailView2		=	64,
		WIN_ItemCardDetailView		=	65,
		WIN_ExchangeCardView2		=	66,
		TIP_CardConfirmView2		=	67,
		TIP_CardDecomposeView2		=	68,
		TIP_CardExchangeSuccessView2		=	69,
		WIN_CardLotteryView		=	70,
		WIN_LotteryResultShowView		=	71,
		WIN_LotteryModelTest		=	72,
		WIN_CardPoolReplenishView		=	73,
		PLOT_HISTROY_VIEW		=	74,
		WIN_FairyMainView		=	75,
		WIN_FairyTrainView		=	76,
		WIN_AnnounceMentView		=	77,
		TIP_FairyActive		=	78,
		TIP_FairyRankUp		=	79,
		WIN_ConsumptionTip		=	80,
		WIN_ObtainTip		=	81,
		TIP_FairyBinding		=	82,
		WIN_AlbumView		=	83,
		WIN_PVPMatchView		=	84,
		WIN_AlbumDetailView		=	85,
		WIN_ResWarView		=	86,
		WIN_ResWarEnterView		=	87,
		WIN_ResWarSweepView		=	88,
		WIN_HeroSourceTip		=	89,
		WIN_ItemSourceTip		=	90,
		WIN_CreateRole		=	91,
		WIN_GameWindow		=	92,
		WIN_ReConnect		=	93,
		WIN_Recover		=	94,
		WIN_PVPMatchHistroyView		=	95,
		WIN_PVPRankingAwardView		=	96,
		WIN_PVPLoading		=	97,
		WIN_InvitePKView		=	98,
		WIN_SevenDayView		=	99,
		WIN_OpenServer		=	100,
		WIN_PVPMatchDanUpView		=	101,
		WIN_TaskMainView		=	102,
		WIN_ExploreView		=	103,
		WIN_ExplorationTaskView		=	104,
		WIN_ExplorationFormationView		=	105,
		WIN_ExploreDropView		=	106,
		WIN_ExploreExplainView		=	107,
		WIN_ExploreTaskRewardView		=	108,
		WIN_ExhibitionRoomView		=	109,
		WIN_ExhibitItemView		=	110,
		WIN_NewExhibitAreaView		=	111,
		WIN_SettlementDataView		=	112,
		WIN_TaskInteractiveView		=	113,
		WIN_DailyMainView		=	114,
		TIP_TutorialPositonTool		=	115,
		TIP_Tutorial		=	116,
		TIP_TutorialOpen		=	117,
		TIP_TutorialEffect		=	118,
		TIP_TutorialBattleView		=	119,
		TIP_ChestContentTip		=	120,
		WIN_RankRewardView		=	121,
		WIN_RankView		=	122,
		WIN_WorldView		=	123,
		WIN_WorldChapterView		=	124,
		WIN_WorldChapterRewardView		=	125,
		WIN_WorldMapView		=	126,
		WIN_RoleSkillActiveView		=	127,
		WIN_GuarderTotalPropertyView		=	128,
		WIN_BeStrengthenView		=	129,
		WIN_SuggestionView		=	130,
		WIN_ResBuyView		=	131,
		WIN_CdKeyView		=	132,
		WIN_ResBuyGoodsView		=	133,
		TIP_ChannelSelectView		=	134,
		WIN_ServerListView		=	135,
		TIP_LevelUpView		=	136,
		WIN_WardroleView		=	137,
		WIN_YzSdkLoginView		=	138,
		WIN_SurveyView		=	139,
		WIN_RoleSkillRelateView		=	140,
		WIN_WorldChapterActiveView		=	141,
		WIN_CommonModelView		=	142,
		WIN_ShoppingMallView		=	143,
		WIN_ShoppingMallBuyView		=	144,
		WIN_ShoppingMallSkinBuyView		=	145,
		WIN_ShoppingMallSkinShow		=	146,
		WIN_RechargeMainView		=	147,
		WIN_FirstRechargeView		=	148,
		WIN_ContinueReChargeView		=	149,
		WIN_SignInView		=	150,
		WIN_BattleBubbleView		=	151,
		WIN_DungeonsMainView		=	152,
		WIN_DungeonsChapterView		=	153,
		WIN_DungeonsChapterRewardTipView		=	154,
		WIN_DungeonsMapView		=	155,
		WIN_SettingView		=	156,
		WIN_SettingHeadView		=	157,
		TIP_YzSdkPayView		=	158,
		WIN_TitleView		=	159,
		WIN_HaloView		=	160,
		WIN_ActivityView		=	161,
		WIN_BossWarMainView		=	162,
		TIP_BossWarStrategy		=	163,
		TIP_BossWarDamageAward		=	164,
		TIP_BossWarKillAward		=	165,
		Tip_DungeonsMapResetNum		=	166,
		TIP_SkillLearnView		=	167,
		WIN_TrainingCampView		=	168,
		WIN_TrainingCampInfoView		=	169,
		WIN_BeStrengthenSubListView		=	170,
		WIN_BeStrengthenGetwayView		=	171,
		WIN_OfflinePVPView		=	172,
		WIN_OfflinePVPRankAwardView		=	173,
		WIN_OfflinePVPFightRecordView		=	174,
		WIN_OfflinePVPFormationPreview		=	175,
		TIP_ItemPreview		=	176,
		TIP_PVPMatchMiniTip		=	177,
		WIN_ExpeditionView		=	178,
		WIN_ExpeditionEnterView		=	179,
		WIN_CreateRole2D		=	180,
		WIN_StartMovie		=	181,
		WIN_ArtifactCultureView		=	182,
		WIN_EnvelopView		=	183,
		WIN_EnvelopAwardView		=	184,
	}
	public enum UI_TYPE{
		NONE		=	0,
		UNDER_WIN		=	1,
		WIN		=	2,
		TIP		=	3,
		TOP		=	4,
	}

	/// <summary>
	/// Id
	///</summary>
	public E_UI E_ui;
	/// <summary>
	/// 名字
	///</summary>
	public string Name;
	/// <summary>
	/// 预制
	///</summary>
	public string Prefab;
	/// <summary>
	/// 类型
	///</summary>
	public UI_TYPE UI_Type;
	/// <summary>
	/// 脚本
	///</summary>
	public string Script;
	/// <summary>
	/// 是否支持多开（0：不支持，1：支持）
	///</summary>
	public bool MoreOpen;
	/// <summary>
	/// 清空时不被清除
	///</summary>
	public int NotBeClear;
	/// <summary>
	/// 显示背景(0:不显示,1:显示遮罩,2:显示模糊背景,3:隐藏自身以下的UI)
	///</summary>
	public int ShowMask;
	/// <summary>
	/// 是否有点击特效（0没有，1有）
	///</summary>
	public bool IsTouchEffect;
	/// <summary>
	/// 是否关闭WorldCamera(0 不关闭，1关闭)
	///</summary>
	public bool IsCloseWorldCamera;
	/// <summary>
	/// 是否显示Loading
	///</summary>
	public bool IsShowLoading;
	/// <summary>
	/// 顶层过滤
	///</summary>
	public bool TopFilter;
	/// <summary>
	/// 小朋友的特殊处理
	///</summary>
	public bool Special;
	/// <summary>
	/// 背景音乐
	///</summary>
	public int BGM;
	/// <summary>
	/// 开启音效
	///</summary>
	public int SE;
	/// <summary>
	/// 动画
	///</summary>
	public int IsAnimation;

	public override void Read(){
		E_ui=(E_UI)ReadShort();
		Name=ReadString();
		Prefab=ReadString();
		UI_Type=(UI_TYPE)ReadShort();
		Script=ReadString();
		MoreOpen=ReadBool();
		NotBeClear=ReadInt();
		ShowMask=ReadInt();
		IsTouchEffect=ReadBool();
		IsCloseWorldCamera=ReadBool();
		IsShowLoading=ReadBool();
		TopFilter=ReadBool();
		Special=ReadBool();
		BGM=ReadInt();
		SE=ReadInt();
		IsAnimation=ReadInt();

	}
	
	public override void Write (){
		WriteShort((short)E_ui);
		WriteString(Name);
		WriteString(Prefab);
		WriteShort((short)UI_Type);
		WriteString(Script);
		WriteBool(MoreOpen);
		WriteInt(NotBeClear);
		WriteInt(ShowMask);
		WriteBool(IsTouchEffect);
		WriteBool(IsCloseWorldCamera);
		WriteBool(IsShowLoading);
		WriteBool(TopFilter);
		WriteBool(Special);
		WriteInt(BGM);
		WriteInt(SE);
		WriteInt(IsAnimation);

	}

	public System.Object Clone()  
	{
		return this.MemberwiseClone();
	}
}

public partial class T_UIConfigMgr{
	public Dictionary<short,T_UIConfig> itemData = new Dictionary<short, T_UIConfig>();
	//public Dictionary<int,List<T_UIConfig>> groupData = new Dictionary<int,List<T_UIConfig>>();
	public int size;

	public T_UIConfigMgr(){
		//ReadConfig ();
	}

	public IEnumerator ReadConfig(){
		string path = PathUtil.StreamingPath("Config/T_UIConfig.bytes");
		WWW data = new WWW (path);
		yield return data;
		MemoryStream stream = new MemoryStream (data.bytes);
		stream.Position = 0;
		
		T_UIConfig byteObj = new T_UIConfig ();
		byteObj.SetStream (stream);
		size = byteObj.ReadInt ();
		
		for (int i=0; i<size; i++) {
			T_UIConfig item = new T_UIConfig();
			item.Deserialization(stream);
			
	//		if(!groupData.ContainsKey()){
	//			groupData[] = new List<T_UIConfig>();
	//		}
	//		groupData[].Add(item);

	      if(itemData.ContainsKey((short)item.E_ui))Loger.Error("T_UIConfig is Repeat KEY = "+(short)item.E_ui);
	      itemData[(short)item.E_ui]=item;
		}

		Loger.Info ("T_UIConfig Config load Complete, size:"+size);
	}

	public T_UIConfig GetData(T_UIConfig.E_UI E_ui){
		short id=(short)E_ui;
		if (itemData.ContainsKey (id)) {
			return itemData[id];
		}
  		//WorldMgr.ShowMessageLog("T_UIConfig not find >> id:"+id);
		Loger.Error ("T_UIConfig not find >> id:"+id);
		return null;
	}
	
	//public List<T_UIConfig> GetGroup(){
	//	
	//	if (groupData.ContainsKey (id)) {
	//		return groupData[id];
	//	}
  	//	//WorldMgr.ShowMessageLog("T_UIConfig not find >> id:"+id);
	//	Loger.Error ("T_UIConfig not find Group >> id:"+id);
	//	return null;
	//}

	public bool HasData(T_UIConfig.E_UI E_ui){
		short id=(short)E_ui;
		return itemData.ContainsKey (id);
	}
	
	//public bool HasList(){
	//	
	//	return groupData.ContainsKey (id);
	//}
}
