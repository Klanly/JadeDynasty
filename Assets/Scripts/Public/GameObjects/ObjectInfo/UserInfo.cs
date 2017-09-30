﻿using DashFireSpatial;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace DashFire
{
    public class GfxUserInfo
    {
        public int m_ActorId;
        public string m_Nick;
        public int m_Level;
        public int m_HeroId;
    }

    public class UserInfo : CharacterInfo
    {
        private string m_NickName = "";
        private float m_Scale = 1.0f;

        private int[] m_AiEquipment = null;
        private int[] m_AiAttackSkill = null;
        private int[] m_AiMoveSkill = null;
        private int[] m_AiControlSkill = null;
        private int[] m_AiSelfAssitSkill = null;
        private int[] m_AiTeamAssitSkill = null;

        private UserAiStateInfo m_AiStateInfo = new UserAiStateInfo();
        private CombatStatisticInfo m_CombatStatisticInfo = new CombatStatisticInfo();

        private string m_IndicatorEffect = "Effects/Monster/Campaign_Wild/04_SilverShield/6_Mon_SSLas_Laser_01";
        private float m_IndicatorDis = 10.0f;
        private int m_IndicatorActor = 0;

        private Vector3 m_RevivePoint;

        public UserInfo(int id):base(id)
        {
            m_SpaceObject = new SpaceObjectImpl(this, SpatialObjType.kUser);
            m_CastUserInfo = this;
        }

        public void InitId(int id)
        {
            m_Id = id;
        }

        public float Scale
        {
            get { return m_Scale; }
        }

        public string GetIndicatorModel()
        {
            return m_IndicatorEffect;
        }

        public string GetNickName()
        {
            return m_NickName;
        }

        public void SetNickName(string nickname)
        {
            m_NickName = nickname;
        }

        public float IndicatorDis
        {
            get { return m_IndicatorDis; }
        }

        public CombatStatisticInfo GetCombatStatisticInfo()
        {
            return m_CombatStatisticInfo;
        }

        public UserAiStateInfo GetAiStateInfo()
        {
            return m_AiStateInfo;
        }

        public Vector3 RevivePoint
        {
            get { return m_RevivePoint; }
            set { m_RevivePoint = value; }
        }

        public void Reset()
        {
            //未实现
            //ResetCharacterInfo();
            //GetAiStateInfo().Reset();
            //GetCombatStatisticInfo().Reset();

            //m_Money = 0;
        }

        public void LoadData(int resId)
        {
            SetLinkId(resId);
            m_LevelupConfig = PlayerConfigProvider.Instance.GetPlayerLevelupConfigById(resId);
            Data_PlayerConfig playerData = PlayerConfigProvider.Instance.GetPlayerConfigById(resId);
            if (null != playerData)
            {
                SetName(playerData.m_Name);
                SetModel(playerData.m_Model);
                SetActionList(playerData.m_ActionList);

                m_AiEquipment = playerData.m_AiEquipment;
                m_AiAttackSkill = playerData.m_AiAttackSkill;
                m_AiMoveSkill = playerData.m_AiMoveSkill;
                m_AiControlSkill = playerData.m_AiControlSkill;
                m_AiSelfAssitSkill = playerData.m_AiSelfAssitSkill;
                m_AiTeamAssitSkill = playerData.m_AiTeamAssitSkill;

                GetAiStateInfo().AiLogic = playerData.m_AiLogic;//设置ai类型
                m_Scale = playerData.m_Scale;
                AvoidanceRadius = playerData.m_AvoidanceRadius;
                Shape = new Circle(new Vector3(0, 0, 0), playerData.m_Radius);

                ViewRange = playerData.m_ViewRange;
                ReleaseTime = playerData.m_ReleaseTime;
                SuperArmor = playerData.m_SuperArmor;

                int hp = (int)playerData.m_AttrData.GetAddHpMax(0, 0);
                int energy = (int)playerData.m_AttrData.GetAddEpMax(0, 0);
                float moveSpeed = playerData.m_AttrData.GetAddSpd(0, 0);
                float walkSpeed = playerData.m_AttrData.GetAddWalkSpd(0, 0);
                float runSpeed = playerData.m_AttrData.GetAddRunSpd(0, 0);
                int hpMax = (int)playerData.m_AttrData.GetAddHpMax(0, 0);
                int energyMax = (int)playerData.m_AttrData.GetAddEpMax(0, 0);
                float hpRecover = playerData.m_AttrData.GetAddHpRecover(0, 0);
                float energyRecover = playerData.m_AttrData.GetAddEpRecover(0, 0);
                int attackBase = (int)playerData.m_AttrData.GetAddAd(0, 0);
                int aDefenceBase = (int)playerData.m_AttrData.GetAddADp(0, 0);
                int mDefenceBase = (int)playerData.m_AttrData.GetAddMDp(0, 0);
                float critical = playerData.m_AttrData.GetAddCri(0, 0);
                float criticalPow = playerData.m_AttrData.GetAddPow(0, 0);
                float criticalBackHitPow = playerData.m_AttrData.GetAddBackHitPow(0, 0);
                float criticalCrackPow = playerData.m_AttrData.GetAddCrackPow(0, 0);
                float fireDam = playerData.m_AttrData.GetAddFireDam(0, 0);
                float fireErd = playerData.m_AttrData.GetAddFireErd(0, 0);
                float iceDam = playerData.m_AttrData.GetAddIceDam(0, 0);
                float iceErd = playerData.m_AttrData.GetAddIceErd(0, 0);
                float poisonDam = playerData.m_AttrData.GetAddPoisonDam(0, 0);
                float poisonErd = playerData.m_AttrData.GetAddPoisonErd(0, 0);
                float weight = playerData.m_AttrData.GetAddWeight(0, 0);
                float rps = playerData.m_AttrData.GetAddRps(0, 0);
                float attackRange = playerData.m_AttrData.GetAddAttackRange(0, 0);

                m_Combat2IdleTime = playerData.m_Combat2IdleTime;
                m_Combat2IdleSkill = playerData.m_Combat2IdleSkill;
                m_Idle2CombatWeaponMoves = playerData.m_Idle2CombatWeaponMoves;
                m_IndicatorEffect = playerData.m_IndicatorEffect;
                m_IndicatorDis = playerData.m_IndicatorShowDis;

                GetBaseProperty().SetMoveSpeed(Operate_Type.OT_Absolute, moveSpeed);
                GetBaseProperty().SetWalkSpeed(Operate_Type.OT_Absolute, walkSpeed);
                GetBaseProperty().SetRunSpeed(Operate_Type.OT_Absolute, runSpeed);
                GetBaseProperty().SetHpMax(Operate_Type.OT_Absolute, hpMax);
                GetBaseProperty().SetRageMax(Operate_Type.OT_Absolute, (int)playerData.m_AttrData.GetAddRageMax(0, 0));
                GetBaseProperty().SetEnergyMax(Operate_Type.OT_Absolute, energyMax);
                GetBaseProperty().SetHpRecover(Operate_Type.OT_Absolute, hpRecover);
                GetBaseProperty().SetEnergyRecover(Operate_Type.OT_Absolute, energyRecover);
                GetBaseProperty().SetAttackBase(Operate_Type.OT_Absolute, attackBase);
                GetBaseProperty().SetADefenceBase(Operate_Type.OT_Absolute, aDefenceBase);
                GetBaseProperty().SetMDefenceBase(Operate_Type.OT_Absolute, mDefenceBase);
                GetBaseProperty().SetCritical(Operate_Type.OT_Absolute, critical);
                GetBaseProperty().SetCriticalPow(Operate_Type.OT_Absolute, criticalPow);
                GetBaseProperty().SetCriticalBackHitPow(Operate_Type.OT_Absolute, criticalBackHitPow);
                GetBaseProperty().SetCriticalCrackPow(Operate_Type.OT_Absolute, criticalCrackPow);
                GetBaseProperty().SetFireDamage(Operate_Type.OT_Absolute, fireDam);
                GetBaseProperty().SetFireERD(Operate_Type.OT_Absolute, fireErd);
                GetBaseProperty().SetIceDamage(Operate_Type.OT_Absolute, iceDam);
                GetBaseProperty().SetIceERD(Operate_Type.OT_Absolute, iceErd);
                GetBaseProperty().SetPoisonDamage(Operate_Type.OT_Absolute, poisonDam);
                GetBaseProperty().SetPoisonERD(Operate_Type.OT_Absolute, poisonErd);
                GetBaseProperty().SetWeight(Operate_Type.OT_Absolute, weight);
                GetBaseProperty().SetRps(Operate_Type.OT_Absolute, rps);
                GetBaseProperty().SetAttackRange(Operate_Type.OT_Absolute, attackRange);

                // 技能数据
                if (GetSkillStateInfo().GetAllSkill().Count <= 0)
                {
                    foreach (int id in playerData.m_PreSkillList)
                    {
                        GetSkillStateInfo().AddSkill(new SkillInfo(id, 1));
                    }
                    foreach (int id in playerData.m_FixedSkillList)
                    {
                        GetSkillStateInfo().AddSkill(new SkillInfo(id, 1));
                    }
                }

                UserAttrCalculator.Calc(this);

                SetHp(Operate_Type.OT_Absolute, GetActualProperty().HpMax);
                SetRage(Operate_Type.OT_Absolute, 0);
                SetEnergy(Operate_Type.OT_Absolute, GetActualProperty().EnergyMax);

                m_Cross2StandTime = playerData.m_Cross2StandTime;
                m_Cross2RunTime = playerData.m_Cross2RunTime;
            }
        }
    }
}
