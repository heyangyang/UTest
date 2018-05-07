using System.Collections;
using UnityEngine;

public class ConfigManager
{

    public static T_ActorMgr Actor;

    public static T_ActorResMgr ActorRes;

    public static T_ActorWeaponMgr ActorWeapon;

    public static T_AiMgr Ai;

    public static T_BuffMgr Buff;

    public static T_DropMgr Drop;

    public static T_EffectMgr Effect;

    public static T_EtcMgr Etc;

    public static T_FairyMgr Fairy;

    public static T_FairyLevelMgr FairyLevel;

    public static T_FairyRankMgr FairyRank;

    public static T_FairyResMgr FairyRes;

    public static T_FormationMgr Formation;

    public static T_HairMgr Hair;

    public static T_HairColorMgr HairColor;

    public static T_LanguageMgr Language;

    public static T_MapMgr Map;

    public static T_MapPveMgr MapPve;

    public static T_MonsterGroupMgr MonsterGroup;

    public static T_SkillMgr Skill;

    public static T_SkillKeyMgr SkillKey;

    public static T_SkillValueMgr SkillValue;

    public static T_UIConfigMgr UIConfig;


	public static IEnumerator Init()
	{
        Actor= new T_ActorMgr() ;
        CoroutineTool.StartCoroutine((Actor.ReadConfig()));

        ActorRes= new T_ActorResMgr() ;
        CoroutineTool.StartCoroutine((ActorRes.ReadConfig()));

        ActorWeapon= new T_ActorWeaponMgr() ;
        CoroutineTool.StartCoroutine((ActorWeapon.ReadConfig()));

        Ai= new T_AiMgr() ;
        CoroutineTool.StartCoroutine((Ai.ReadConfig()));

        Buff= new T_BuffMgr() ;
        CoroutineTool.StartCoroutine((Buff.ReadConfig()));

        Drop= new T_DropMgr() ;
        CoroutineTool.StartCoroutine((Drop.ReadConfig()));

        Effect= new T_EffectMgr() ;
        CoroutineTool.StartCoroutine((Effect.ReadConfig()));

        Etc= new T_EtcMgr() ;
        CoroutineTool.StartCoroutine((Etc.ReadConfig()));

        Fairy= new T_FairyMgr() ;
        CoroutineTool.StartCoroutine((Fairy.ReadConfig()));

        FairyLevel= new T_FairyLevelMgr() ;
        CoroutineTool.StartCoroutine((FairyLevel.ReadConfig()));

        FairyRank= new T_FairyRankMgr() ;
        CoroutineTool.StartCoroutine((FairyRank.ReadConfig()));

        FairyRes= new T_FairyResMgr() ;
        CoroutineTool.StartCoroutine((FairyRes.ReadConfig()));

        Formation= new T_FormationMgr() ;
        CoroutineTool.StartCoroutine((Formation.ReadConfig()));

        Hair= new T_HairMgr() ;
        CoroutineTool.StartCoroutine((Hair.ReadConfig()));

        HairColor= new T_HairColorMgr() ;
        CoroutineTool.StartCoroutine((HairColor.ReadConfig()));

        Language= new T_LanguageMgr() ;
        CoroutineTool.StartCoroutine((Language.ReadConfig()));

        Map= new T_MapMgr() ;
        CoroutineTool.StartCoroutine((Map.ReadConfig()));

        MapPve= new T_MapPveMgr() ;
        CoroutineTool.StartCoroutine((MapPve.ReadConfig()));

        MonsterGroup= new T_MonsterGroupMgr() ;
        CoroutineTool.StartCoroutine((MonsterGroup.ReadConfig()));

        Skill= new T_SkillMgr() ;
        CoroutineTool.StartCoroutine((Skill.ReadConfig()));

        SkillKey= new T_SkillKeyMgr() ;
        CoroutineTool.StartCoroutine((SkillKey.ReadConfig()));

        SkillValue= new T_SkillValueMgr() ;
        CoroutineTool.StartCoroutine((SkillValue.ReadConfig()));

        UIConfig= new T_UIConfigMgr() ;
        CoroutineTool.StartCoroutine((UIConfig.ReadConfig()));

        yield return null;
	}
}
