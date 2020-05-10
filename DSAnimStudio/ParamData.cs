using Microsoft.Xna.Framework;
using SoulsFormats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSAnimStudio
{
    public abstract class ParamData
    {
        [ParamEntryIgnore()]
        public long ID;
        [ParamEntryIgnore()]
        public string Name;

        public string GetDisplayName()
        {
            return $"{ID}{(!string.IsNullOrWhiteSpace(Name) ? $": {Name}" : "")}";
        }

        public abstract string ParamdefXmlName { get; }

        public virtual void Read(PARAM.Row row)
        {
            foreach (var field in GetType().GetFields())
            {
                if (field.IsStatic)
                {
                    continue;
                }

                if (Attribute.IsDefined(field, typeof(ParamEntryIgnoreAttribute)))
                {
                    continue;
                }

                string entryName = field.Name;

                if (Attribute.IsDefined(field, typeof(ParamEntryCustomNameAttribute)))
                {
                    entryName = ((ParamEntryCustomNameAttribute)Attribute.GetCustomAttribute(field, typeof(ParamEntryCustomNameAttribute))).EntryName;
                }

                if (field.FieldType == typeof(bool))
                {
                    field.SetValue(this, (byte)row[entryName].Value != 0);
                }
                else
                {
                    field.SetValue(this, row[entryName].Value);
                }
            }
        }

        [AttributeUsage(AttributeTargets.Field)]
        public class ParamEntryCustomNameAttribute : Attribute
        {
            public ParamEntryCustomNameAttribute(string entryName)
            {
                EntryName = entryName;
            }

            public string EntryName { get; }
        }

        [AttributeUsage(AttributeTargets.Field)]
        public class ParamEntryIgnoreAttribute : Attribute
        {
        }

        public class AtkParam : ParamData
        {
            public enum HitTypes : byte
            {
                Tip = 0,
                Middle = 1,
                Root = 2,
            }

            public enum DummyPolySource
            {
                None = 0,
                Body = 1,
                RightWeapon = 2,
                LeftWeapon = 3
            }

            public DummyPolySource SuggestedDummyPolySource;

            public struct Hit
            {
                public float Radius;
                public short DmyPoly1;
                public short DmyPoly2;
                public HitTypes HitType;

                const int DS3PairR = 10000;
                const int DS3PairL = 11000;

                const int SDTSomething1 = 10000;
                const int SDTSomething2 = 21000;

                private static int GetFilteredDmyPolyID(ParamData.AtkParam.DummyPolySource dmyFilter, int id)
                {
                    if (id < 0)
                        return -1;

                    int check = id / 1000;
                    id = id % 1000;

                    if (dmyFilter == DummyPolySource.None)
                    {
                        return -1;
                    }
                    else if (dmyFilter == DummyPolySource.Body)
                    {
                        return (check < 10) ? id : -1;
                    }
                    else if (dmyFilter == DummyPolySource.RightWeapon)
                    {
                        return (check == 10 || check == 12) ? id : -1;
                    }
                    else if (dmyFilter == DummyPolySource.LeftWeapon)
                    {
                        return (check == 11 || check == 13 || check == 20) ? id : -1;
                    }

                    return -1;
                }

                public int GetFilteredDmyPoly1(ParamData.AtkParam.DummyPolySource dmyFilter)
                {
                    return GetFilteredDmyPolyID(dmyFilter, DmyPoly1);
                }

                public int GetFilteredDmyPoly2(ParamData.AtkParam.DummyPolySource dmyFilter)
                {
                    return GetFilteredDmyPolyID(dmyFilter, DmyPoly2);
                }

                //public void ShiftDmyPolyIDIntoPlayerWpnDmyPolyID(bool isLeftHand)
                //{
                //    if (DmyPoly1 >= 0 && DmyPoly1 < 10000)
                //    {
                //        var dmy1mod = DmyPoly1 % 1000;
                //        //if (dmy1mod >= 100 && dmy1mod <= 130)
                //        //{
                //        //    DmyPoly1 = (short)(dmy1mod + (isLeftHand ? 11000 : 10000));
                //        //}
                //        DmyPoly1 = (short)(dmy1mod + (isLeftHand ? 11000 : 10000));
                //    }

                //    if (DmyPoly2 >= 0 && DmyPoly2 < 10000)
                //    {
                //        var dmy2mod = DmyPoly2 % 1000;
                //        //if (dmy2mod >= 100 && dmy2mod <= 130)
                //        //{
                //        //    DmyPoly2 = (short)(dmy2mod + (isLeftHand ? 11000 : 10000));
                //        //}
                //        DmyPoly2 = (short)(dmy2mod + (isLeftHand ? 11000 : 10000));
                //    }
                //}

                public Color GetColor()
                {
                    switch (HitType)
                    {
                        case HitTypes.Tip: return new Color(231, 186, 50);
                        case HitTypes.Middle: return new Color(230, 26, 26);
                        case HitTypes.Root: return new Color(26, 26, 230);
                        default: return Color.Fuchsia;
                    }
                }

                public bool IsCapsule => DmyPoly1 >= 0 && DmyPoly2 >= 0 && DmyPoly1 != DmyPoly2;

                public static Hit Read(PARAM.Row row, int hitIndex)
                {
                    string hitEntryFormat = $"Hit{hitIndex}_";

                    T GetRowValue<T>(string entryName)
                    {
                        return (T)row[hitEntryFormat + entryName].Value;
                    }

                    Hit hit = new Hit();
                    hit.Radius = GetRowValue<float>("Radius");
                    hit.DmyPoly1 = GetRowValue<short>("DmyPoly1");
                    hit.DmyPoly2 = GetRowValue<short>("DmyPoly2");
                    hit.HitType = (HitTypes)GetRowValue<byte>("hitType");

                    return hit;
                }
            }

            public Hit[] Hits;

            public short BlowingCorrection;
            public short AtkPhysCorrection;
            public short AtkMagCorrection;
            public short AtkFireCorrection;
            public short AtkThunCorrection;
            public short AtkStamCorrection;
            public short GuardAtkRateCorrection;
            public short GuardBreakCorrection;
            public short AtkThrowEscapeCorrection;
            public short AtkSuperArmorCorrection;
            public short AtkPhys;
            public short AtkMag;
            public short AtkFire;
            public short AtkThun;
            public short AtkStam;
            public short GuardAtkRate;
            public short GuardBreakRate;
            public short AtkSuperArmor;
            public short AtkThrowEscape;
            public short AtkObj;
            public short GuardStaminaCutRate;
            public short GuardRate;
            public short ThrowTypeID;

            // DS3 Only
            public short AtkDarkCorrection;
            public short AtkDark;

            public override string ParamdefXmlName => new[] { GameDataManager.GameTypes.DS1, GameDataManager.GameTypes.BB, GameDataManager.GameTypes.DS1R }.Contains(GameDataManager.GameType) ? "AtkParam.xml" : "ATK_PARAM_ST.xml";

            public Color GetCapsuleColor(Hit hit)
            {
                if (ThrowTypeID > 0)
                {
                    return Color.Cyan;
                }
                else if ((AtkPhys > 0 || AtkMag > 0 || AtkFire > 0 || AtkThun > 0 || AtkDark > 0) ||
                    (AtkPhysCorrection > 0 || AtkMagCorrection > 0 || AtkFireCorrection > 0 || AtkThunCorrection > 0 || AtkDarkCorrection > 0))
                {
                    switch (hit.HitType)
                    {
                        case HitTypes.Tip: return new Color(231, 186, 50);
                        case HitTypes.Middle: return new Color(230, 26, 26);
                        case HitTypes.Root: return new Color(26, 26, 230);
                        default: return Color.Fuchsia;
                    }
                }
                else
                {
                    return Color.DarkGreen;
                }
            }

            public override void Read(PARAM.Row row)
            {
                System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(@"Hit\d+_Radius");

                int hitsCount = row.Cells.Where(cell => regex.IsMatch(cell.Def.InternalName)).Count();

                Hits = new Hit[hitsCount];

                for (int hitIndex = 0; hitIndex < hitsCount; ++hitIndex)
                {
                    Hits[hitIndex] = Hit.Read(row, hitIndex);
                }




                BlowingCorrection = (short)row["Blowing Correction"].Value;
                AtkPhysCorrection = (short)row["AtkPhysCorrection"].Value;
                AtkMagCorrection = (short)row["AtkMagCorrection"].Value;
                AtkFireCorrection = (short)row["AtkFireCorrection"].Value;
                AtkThunCorrection = (short)row["AtkThunCorrection"].Value;
                AtkStamCorrection = (short)row["AtkStamCorrection"].Value;
                GuardAtkRateCorrection = (short)row["GuardAtkRateCorrection"].Value;
                GuardBreakCorrection = (short)row["GuardBreakCorrection"].Value;
                AtkThrowEscapeCorrection = (short)row["AtkThrowEscapeCorrection"].Value;
                AtkSuperArmorCorrection = (short)row["AtkSuperArmorCorrection"].Value;
                AtkPhys = (short)row["AtkPhys"].Value;
                AtkMag = (short)row["AtkMag"].Value;
                AtkFire = (short)row["AtkFire"].Value;
                AtkThun = (short)row["AtkThun"].Value;
                AtkStam = (short)row["AtkStam"].Value;
                GuardAtkRate = (short)row["GuardAtkRate"].Value;
                GuardBreakRate = (short)row["GuardBreakRate"].Value;
                AtkSuperArmor = (short)row["AtkSuperArmor"].Value;
                AtkThrowEscape = (short)row["AtkThrowEscape"].Value;
                AtkObj = (short)row["AtkObj"].Value;
                GuardStaminaCutRate = (short)row["GuardStaminaCutRate"].Value;
                GuardRate = (short)row["GuardRate"].Value;
                ThrowTypeID = (short)row["ThrowTypeID"].Value;


                // DS3 Only
                if (GameDataManager.GameType == GameDataManager.GameTypes.DS3)
                {
                    AtkDarkCorrection = (short)row["atkDarkCorrection"].Value;
                    AtkDark = (short)row["atkDark"].Value;
                }

                SuggestedDummyPolySource = DummyPolySource.None;

                for (int i = 0; i < Hits.Length; i++)
                {
                    if (Hits[i].DmyPoly1 >= 10000 && Hits[i].DmyPoly1 < 11000)
                        SuggestedDummyPolySource = DummyPolySource.RightWeapon;
                    else if (Hits[i].DmyPoly1 >= 11000 && Hits[i].DmyPoly1 < 12000)
                        SuggestedDummyPolySource = DummyPolySource.LeftWeapon;
                    else if (Hits[i].DmyPoly1 >= 12000 && Hits[i].DmyPoly1 < 13000)
                        SuggestedDummyPolySource = DummyPolySource.RightWeapon;
                    else if (Hits[i].DmyPoly1 >= 13000 && Hits[i].DmyPoly1 < 14000)
                        SuggestedDummyPolySource = DummyPolySource.LeftWeapon;
                    else if (Hits[i].DmyPoly1 >= 20000 && Hits[i].DmyPoly1 < 21000)
                        SuggestedDummyPolySource = DummyPolySource.LeftWeapon;
                }

                if (SuggestedDummyPolySource == DummyPolySource.None)
                {
                    if ((byte)row["HitSourceType"].Value == 1)
                    {
                        SuggestedDummyPolySource = DummyPolySource.Body;
                    }
                }
            }
        }




        public class BehaviorParam : ParamData
        {
            public int VariationID;
            public int BehaviorJudgeID;
            public byte EzStateBehaviorType_Old;
            public enum RefTypes : byte
            {
                Attack = 0,
                Bullet = 1,
                SpEffect = 2,
            }
            public RefTypes RefType;
            public int RefID;
            public int SFXVariationID;
            public int Stamina;
            public int MP;
            public byte Category;
            public byte HeroPoint;

            public override string ParamdefXmlName => "BEHAVIOR_PARAM_ST.xml";

            public override void Read(PARAM.Row row)
            {
                VariationID = (int)row["variationId"].Value;
                BehaviorJudgeID = (int)row["behaviorJudgeId"].Value;
                EzStateBehaviorType_Old = (byte)row["ezStateBehaviorType_old"].Value;
                RefType = (RefTypes)(byte)row["refType"].Value;
                RefID = (int)row["refId"].Value;
                SFXVariationID = (int)row["sfxVariationId"].Value;
                Stamina = (int)row["stamina"].Value; ;
                MP = (int)row["mp"].Value;
                Category = (byte)row["category"].Value;
                HeroPoint = (byte)row["heroPoint"].Value;
            }
        }

        public class NpcParam : ParamData
        {
            public int BehaviorVariationID;

            public bool[] DrawMask;

            public override string ParamdefXmlName => "NPC_PARAM_ST.xml";

            public string GetMaskString(Dictionary<int, string> materialsPerMask,
                List<int> masksEnabledOnAllNpcParamsForThisChr)
            {
                if (materialsPerMask.Any(kvp => kvp.Key >= 0))
                {
                    var sb = new StringBuilder();

                    bool isFirst = true;

                    bool nothingInHere = true;

                    foreach (var kvp in materialsPerMask)
                    {
                        if (kvp.Key < 0)
                            continue;

                        if (masksEnabledOnAllNpcParamsForThisChr.Contains(kvp.Key))
                            continue;

                        if (DrawMask[kvp.Key])
                        {
                            if (!isFirst)
                                sb.Append("  ");
                            else
                                isFirst = false;

                            sb.Append($"[{kvp.Value}]");
                            nothingInHere = false;
                        }
                    }

                    if (nothingInHere)
                    {
                        return "";
                    }
                    else
                    {
                        return sb.ToString();
                    }
                }
                else
                {
                    return "";
                }

            }

            public void ApplyMaskToModel(Model mdl)
            {
                for (int i = 0; i < Math.Min(Model.DRAW_MASK_LENGTH, DrawMask.Length); i++)
                {
                    mdl.DrawMask[i] = DrawMask[i];
                    mdl.DefaultDrawMask[i] = DrawMask[i];
                }
            }

            private IEnumerable<bool> CreateDrawMask(PARAM.Row row, int entriesCount)
            {
                for (int modelDispMaskEntryIndex = 0; modelDispMaskEntryIndex < entriesCount; ++modelDispMaskEntryIndex)
                {
                    const string entryNameTemplate = "ModelDispMask";
                    yield return (byte)row[entryNameTemplate + modelDispMaskEntryIndex].Value != 0;
                }
            }

            public override void Read(PARAM.Row row)
            {
                BehaviorVariationID = (int)row["behaviorVariationId"].Value;

                bool isModelDisp32Bit = row.Cells.Any(cell => cell.Def.InternalName == "ModelDispMask16");

                DrawMask = CreateDrawMask(row, isModelDisp32Bit ? 32 : 16).ToArray();
            }
        }

        public enum EquipModelGenders : byte
        {
            Unisex = 0,
            MaleOnly = 1,
            FemaleOnly = 2,
            Both = 3,
            UseMaleForBoth = 4,
        }

        public class EquipParamWeapon : ParamData
        {
            [ParamEntryCustomName("behaviorVariationId")]
            public int BehaviorVariationID;
            [ParamEntryCustomName("equipModelId")]
            public short EquipModelID;
            [ParamEntryCustomName("wepmotionCategory")]
            public byte WepMotionCategory;
            [ParamEntryCustomName("spAtkCategory")]
            public short SpAtkCategory;
            [ParamEntryCustomName("wepAbsorpPosId")]
            public int WepAbsorpPosID = -1;

            public bool IsPairedWeaponDS3 => GameDataManager.GameType == GameDataManager.GameTypes.DS3
                && (DS3PairedSpAtkCategories.Contains(SpAtkCategory) || (WepMotionCategory == 42)) // DS3 Fist weapons
                ;

            public override string ParamdefXmlName => "EQUIP_PARAM_WEAPON_ST.xml";

            public string GetFullPartBndPath()
            {
                var name = GetPartBndName();
                var partsbndPath = $@"{GameDataManager.InterrootPath}\parts\{name}";

                if (System.IO.File.Exists(partsbndPath + ".dcx"))
                    partsbndPath = partsbndPath + ".dcx";

                return partsbndPath;
            }

            public string GetPartBndName()
            {
                return $"WP_A_{EquipModelID:D4}.partsbnd";
            }

            public static readonly int[] DS3PairedSpAtkCategories = new int[]
            {
                137, //Sellsword Twinblades, Warden Twinblades
                138, //Winged Knight Twinaxes
                139, //Dancer's Enchanted Swords
                141, //Brigand Twindaggers
                142, //Gotthard Twinswords
                144, //Onikiri and Ubadachi
                145, //Drang Twinspears
                148, //Drang Hammers
                161, //Farron Greatsword
                232, //Friede's Great Scythe
                236, //Valorheart
                237, //Crow Quills
                250, //Giant Door Shield
                253, //Ringed Knight Paired Greatswords
            };
        }

        public class EquipParamProtector : ParamData
        {
            [ParamEntryCustomName("equipModelID")]
            public short EquipModelID;
            [ParamEntryCustomName("equipModelGender")]
            public EquipModelGenders EquipModelGender;
            [ParamEntryCustomName("headEquip")]
            public bool HeadEquip;
            [ParamEntryCustomName("bodyEquip")]
            public bool BodyEquip;
            [ParamEntryCustomName("armEquip")]
            public bool ArmEquip;
            [ParamEntryCustomName("legEquip")]
            public bool LegEquip;
            [ParamEntryIgnore]
            public List<bool> InvisibleFlags = new List<bool>();

            public override string ParamdefXmlName => "EQUIP_PARAM_PROTECTOR_ST.xml";

            public void ApplyInvisFlagsToMask(ref bool[] mask)
            {
                for (int i = 0; i < InvisibleFlags.Count; i++)
                {
                    if (i > mask.Length)
                        return;

                    if (InvisibleFlags[i])
                        mask[i] = false;
                }
            }

            private string GetPartFileNameStart()
            {
                if (HeadEquip)
                    return "HD";
                else if (BodyEquip)
                    return "BD";
                else if (ArmEquip)
                    return "AM";
                else if (LegEquip)
                    return "LG";
                else
                    return null;
            }

            public string GetFullPartBndPath(bool isFemale)
            {
                var name = GetPartBndName(isFemale);
                var partsbndPath = $@"{GameDataManager.InterrootPath}\parts\{name}";

                if (System.IO.File.Exists(partsbndPath + ".dcx"))
                    partsbndPath = partsbndPath + ".dcx";

                return partsbndPath;
            }

            public string GetPartBndName(bool isFemale)
            {
                string start = GetPartFileNameStart();

                if (start == null)
                    return null;

                switch (EquipModelGender)
                {
                    case EquipModelGenders.Unisex: return $"{start}_A_{EquipModelID:D4}.partsbnd";
                    case EquipModelGenders.MaleOnly:
                    case EquipModelGenders.UseMaleForBoth:
                        return $"{start}_M_{EquipModelID:D4}.partsbnd";
                    case EquipModelGenders.FemaleOnly:
                        return $"{start}_F_{EquipModelID:D4}.partsbnd";
                    case EquipModelGenders.Both:
                        return $"{start}_{(isFemale ? "F" : "M")}_{EquipModelID:D4}.partsbnd";
                }

                return null;
            }

            public override void Read(PARAM.Row row)
            {
                base.Read(row);

                var regex = new System.Text.RegularExpressions.Regex(@"InvisibleFlag\d+");

                var invisibleFlagCells = row.Cells.Where(cell => regex.IsMatch(cell.Def.InternalName)).OrderBy(cell => cell.Def.InternalName).ToArray();

                InvisibleFlags = new List<bool>();

                foreach (var cell in invisibleFlagCells)
                {
                    bool isFlagSet = (byte)cell.Value != 1;

                    InvisibleFlags.Add(isFlagSet);
                }
            }
        }

        public class WepAbsorpPosParam : ParamData
        {
            public enum WepAbsorpPosType
            {
                OneHand0,
                OneHand1,
                OneHand2,
                OneHand3,
                OneHand4,
                OneHand5,
                OneHand6,
                OneHand7,
                BothHand0,
                BothHand1,
                BothHand2,
                BothHand3,
                BothHand4,
                BothHand5,
                BothHand6,
                BothHand7,
                Sheath0,
                Sheath1,
                Sheath2,
                Sheath3,
                Sheath4,
                Sheath5,
                Sheath6,
                Sheath7,
            }

            public Dictionary<WepAbsorpPosType, ushort> AbsorpPos = new Dictionary<WepAbsorpPosType, ushort>();

            public override string ParamdefXmlName => "WEP_ABSORP_POS_PARAM_ST.xml";

            private static Dictionary<WepAbsorpPosType, string> enumValueToEntryName;

            static WepAbsorpPosParam()
            {
                enumValueToEntryName = new Dictionary<WepAbsorpPosType, string>();

                enumValueToEntryName.Add(WepAbsorpPosType.OneHand0, "OneHandDamipolyId0");
                enumValueToEntryName.Add(WepAbsorpPosType.OneHand1, "OneHandDamipolyId1");
                enumValueToEntryName.Add(WepAbsorpPosType.OneHand2, "OneHandDamipolyId2");
                enumValueToEntryName.Add(WepAbsorpPosType.OneHand3, "OneHandDamipolyId3");
                enumValueToEntryName.Add(WepAbsorpPosType.OneHand4, "OneHandDamipolyId4");
                enumValueToEntryName.Add(WepAbsorpPosType.OneHand5, "OneHandDamipolyId5");
                enumValueToEntryName.Add(WepAbsorpPosType.OneHand6, "OneHandDamipolyId6");
                enumValueToEntryName.Add(WepAbsorpPosType.OneHand7, "OneHandDamipolyId7");
                enumValueToEntryName.Add(WepAbsorpPosType.BothHand0, "BothHandDamipolyId0");
                enumValueToEntryName.Add(WepAbsorpPosType.BothHand1, "BothHandDamipolyId1");
                enumValueToEntryName.Add(WepAbsorpPosType.BothHand2, "BothHandDamipolyId2");
                enumValueToEntryName.Add(WepAbsorpPosType.BothHand3, "BothHandDamipolyId3");
                enumValueToEntryName.Add(WepAbsorpPosType.BothHand4, "BothHandDamipolyId4");
                enumValueToEntryName.Add(WepAbsorpPosType.BothHand5, "BothHandDamipolyId5");
                enumValueToEntryName.Add(WepAbsorpPosType.BothHand6, "BothHandDamipolyId6");
                enumValueToEntryName.Add(WepAbsorpPosType.BothHand7, "BothHandDamipolyId7");
                enumValueToEntryName.Add(WepAbsorpPosType.Sheath0, "ShealthDamipolyId0");
                enumValueToEntryName.Add(WepAbsorpPosType.Sheath1, "ShealthDamipolyId1");
                enumValueToEntryName.Add(WepAbsorpPosType.Sheath2, "ShealthDamipolyId2");
                enumValueToEntryName.Add(WepAbsorpPosType.Sheath3, "ShealthDamipolyId3");
                enumValueToEntryName.Add(WepAbsorpPosType.Sheath4, "ShealthDamipolyId4");
                enumValueToEntryName.Add(WepAbsorpPosType.Sheath5, "ShealthDamipolyId5");
                enumValueToEntryName.Add(WepAbsorpPosType.Sheath6, "ShealthDamipolyId6");
                enumValueToEntryName.Add(WepAbsorpPosType.Sheath7, "ShealthDamipolyId7");
            }

            public override void Read(PARAM.Row row)
            {
                foreach (var kvp in enumValueToEntryName)
                {
                    var cell = row[kvp.Value];

                    ushort value;

                    if (cell.Def.DisplayType == PARAMDEF.DefType.s16)
                    {
                        checked
                        {
                            value = (ushort)(short)cell.Value;
                        }
                    }
                    else if (cell.Def.DisplayType == PARAMDEF.DefType.u16)
                    {
                        value = (ushort)cell.Value;
                    }
                    else
                    {
                        throw new NotImplementedException();
                    }

                    AbsorpPos[kvp.Key] = value;
                }
            }
        }

        private static List<bool> ReadBitmask(BinaryReaderEx br, int numBits)
        {
            List<bool> result = new List<bool>(numBits);
            var maskBytes = br.ReadBytes((int)Math.Ceiling(numBits / 8.0f));
            for (int i = 0; i < numBits; i++)
            {
                result.Add((maskBytes[i / 8] & (byte)(1 << (i % 8))) != 0);
            }
            return result;
        }
    }
}
