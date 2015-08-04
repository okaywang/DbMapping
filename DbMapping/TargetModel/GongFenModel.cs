using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TargetModel
{
    public class GongFenModel : ITargetModel
    {
        ///// <summary>			
        ///// ID ID ID VARCHAR2(32) 字符			
        ///// </summary>			
        //public string ID { get; set; }

        public int ID { get; set; }

        /// <summary>			
        /// 编码 CODE CODE VARCHAR2(32) 字符			
        /// </summary>			
        public string CODE { get; set; }

        /// <summary>			
        /// 化验时间 HYSJ HYSJ DATE 日期时间			
        /// </summary>			
        public DateTime HYSJ { get; set; }

        /// <summary>			
        /// 化验员 HYY HYY VARCHAR2(32) 字符			
        /// </summary>			
        public string HYY { get; set; }

        /// <summary>			
        /// 化验仪器 HYYQ HYYQ VARCHAR2(32) 字符			
        /// </summary>			
        public string HYYQ { get; set; }

        /// <summary>			
        /// 内水煤样质量 NSMYZL NSMYZL NUMBER(15,6) 浮点			
        /// </summary>			
        public decimal NSMYZL { get; set; }

        /// <summary>			
        /// 内水检验结果Mad NSJYJG_MAD NSJYJG_MAD NUMBER(15,6) 浮点			
        /// </summary>			
        public decimal NSJYJG_MAD { get; set; }

        /// <summary>			
        /// 灰分煤样质量 HFYYZL HFYYZL NUMBER(15,6) 浮点			
        /// </summary>			
        public decimal HFYYZL { get; set; }

        /// <summary>			
        /// 灰分检验结果Aad HFJYJG_AAD HFJYJG_AAD NUMBER(15,6) 浮点			
        /// </summary>			
        public decimal HFJYJG_AAD { get; set; }

        /// <summary>			
        /// 挥发分煤样质量 HFFMYZL HFFMYZL NUMBER(15,6) 浮点			
        /// </summary>			
        public decimal HFFMYZL { get; set; }

        /// <summary>			
        /// 挥发分检验结果Vad HFFJYJG_VAD HFFJYJG_VAD NUMBER(15,6) 浮点			
        /// </summary>			
        public decimal HFFJYJG_VAD { get; set; }

        /// <summary>			
        /// 干燥无灰基挥发分（Vdaf） GZWHJHFF_VDAF GZWHJHFF_VDAF NUMBER(15,6) 浮点			
        /// </summary>			
        public decimal GZWHJHFF_VDAF { get; set; }

        /// <summary>			
        /// 空干基固定碳含量（Fcad） KGJGDTHL_FCAD KGJGDTHL_FCAD NUMBER(15,6) 浮点			
        /// </summary>			
        public decimal KGJGDTHL_FCAD { get; set; }

        /// <summary>			
        /// 氢含量（Had） QHL_HAD QHL_HAD NUMBER(15,6) 浮点			
        /// </summary>			
        public decimal QHL_HAD { get; set; }

        /// <summary>			
        /// Had HAD HAD NUMBER(15,6) 浮点			
        /// </summary>			
        public decimal HAD { get; set; }

        /// <summary>			
        /// Fcad FCAD FCAD NUMBER(15,6) 浮点			
        /// </summary>			
        public string FCAD { get; set; }

        /// <summary>			
        /// Vdaf VDAF VDAF NUMBER(15,6) 浮点			
        /// </summary>			
        public decimal VDAF { get; set; }

        /// <summary>			
        /// 数据类型 DATATYPE DATATYPE VARCHAR2(32) 字符			
        /// </summary>			
        public string DATATYPE { get; set; }

        /// <summary>			
        /// 是否平均 IFAVG IFAVG boolean bool			
        /// </summary>			
        public bool IFAVG { get; set; }

        /// <summary>			
        /// 上传时间 SCSJ SCSJ DATE 日期时间			
        /// </summary>			
        public string SCSJ { get; set; }

        /// <summary>			
        /// 焦渣号 JZH JZH VARCHAR2(32) 字符			
        /// </summary>			
        public string JZH { get; set; }

        /// <summary>			
        /// 字符备用1 ZFBY1 ZFBY1 VARCHAR2(32) 字符			
        /// </summary>			
        public string ZFBY1 { get; set; }

        /// <summary>			
        /// 字符备用2 ZFBY2 ZFBY2 VARCHAR2(32) 字符			
        /// </summary>			
        public string ZFBY2 { get; set; }

        /// <summary>			
        /// 字符备用3 ZFBY3 ZFBY3 VARCHAR2(32) 字符			
        /// </summary>			
        public string ZFBY3 { get; set; }

        /// <summary>			
        /// 字符备用4 ZFBY4 ZFBY4 VARCHAR2(32) 字符			
        /// </summary>			
        public string ZFBY4 { get; set; }

        /// <summary>			
        /// 字符备用5 ZFBY5 ZFBY5 VARCHAR2(32) 字符			
        /// </summary>			
        public string ZFBY5 { get; set; }

        /// <summary>			
        /// 字符备用6 ZFBY6 ZFBY6 VARCHAR2(32) 字符			
        /// </summary>			
        public string ZFBY6 { get; set; }

        /// <summary>			
        /// 字符备用7 ZFBY7 ZFBY7 VARCHAR2(32) 字符			
        /// </summary>			
        public string ZFBY7 { get; set; }

        /// <summary>			
        /// 字符备用8 ZFBY8 ZFBY8 VARCHAR2(32) 字符			
        /// </summary>			
        public string ZFBY8 { get; set; }

        /// <summary>			
        /// 字符备用9 ZFBY9 ZFBY9 VARCHAR2(32) 字符			
        /// </summary>			
        public string ZFBY9 { get; set; }

        /// <summary>			
        /// 字符备用10 ZFBY10 ZFBY10 VARCHAR2(32) 字符			
        /// </summary>			
        public string ZFBY10 { get; set; }

        /// <summary>			
        /// 时间备用1 SJBY1 SJBY1 DATE 日期时间			
        /// </summary>			
        public DateTime? SJBY1 { get; set; }

        /// <summary>			
        /// 时间备用2 SJBY2 SJBY2 DATE 日期时间			
        /// </summary>			
        public DateTime? SJBY2 { get; set; }

        /// <summary>			
        /// 时间备用3 SJBY3 SJBY3 DATE 日期时间			
        /// </summary>			
        public DateTime? SJBY3 { get; set; }

        /// <summary>			
        /// float备用1 FLOATBY1 FLOATBY1 NUMBER(15,6) 浮点			
        /// </summary>			
        public decimal? FLOATBY1 { get; set; }

        /// <summary>			
        /// float备用2 FLOATBY2 FLOATBY2 NUMBER(15,6) 浮点			
        /// </summary>			
        public decimal? FLOATBY2 { get; set; }

        /// <summary>			
        /// float备用3 FLOATBY3 FLOATBY3 NUMBER(15,6) 浮点			
        /// </summary>			
        public decimal? FLOATBY3 { get; set; }

        /// <summary>			
        /// float备用4 FLOATBY4 FLOATBY4 NUMBER(15,6) 浮点			
        /// </summary>			
        public decimal? FLOATBY4 { get; set; }

        /// <summary>			
        /// float备用5 FLOATBY5 FLOATBY5 NUMBER(15,6) 浮点			
        /// </summary>			
        public decimal? FLOATBY5 { get; set; }

        /// <summary>			
        /// float备用6 FLOATBY6 FLOATBY6 NUMBER(15,6) 浮点			
        /// <summary>			
        public decimal? FLOATBY6 { get; set; }
    }
}
