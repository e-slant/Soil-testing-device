using smartAgriculture.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smartAgriculture.Models
{
    public class suggest: NotifyBase
    {
        private float _NitrogenFertilizer=0;
        private float _PhosphorusFertilizer=0;
        private float _PotassiumFertilizer=0;
        private float _WateringAmount=0;
        private string _Crop1="未分析";
        private string _Crop2="未分析";
        private string _Crop3 = "未分析";
        private string _Crop4 = "未分析";
        private string _Crop5 = "未分析";
        private float _Crop1_Rate = 0;
        private float _Crop2_Rate = 0;
        private float _Crop3_Rate = 0;
        private float _Crop4_Rate = 0;
        private float _Crop5_Rate = 0;
        private string _Croptype;
        public float NitrogenFertilizer
        {
            get { return _NitrogenFertilizer; }
            set { _NitrogenFertilizer = value; this.NotifyChanged(); }
        }
        public string Croptype
        {
            get { return _Croptype; }
            set { _Croptype = value; this.NotifyChanged(); }
        }
        public float PhosphorusFertilizer
        {
            get { return _PhosphorusFertilizer; }
            set { _PhosphorusFertilizer = value; this.NotifyChanged(); }
        }
        public float PotassiumFertilizer
        {
            get { return _PotassiumFertilizer; }
            set { _PotassiumFertilizer = value; this.NotifyChanged(); }
        }
        public float WateringAmount
        {
            get { return _WateringAmount; }
            set { _WateringAmount = value; this.NotifyChanged(); }
        }
        public string Crop1
        {
            get { return _Crop1; }
            set { _Crop1 = value; this.NotifyChanged(); }
        }
        public string Crop2
        {
            get { return _Crop2; }
            set { _Crop2 = value; this.NotifyChanged(); }
        }
        public string Crop3
        {
            get { return _Crop3; }
            set { _Crop3 = value; this.NotifyChanged(); }
        }
        public string Crop4
        {
            get { return _Crop4; }
            set { _Crop4 = value; this.NotifyChanged(); }
        }
        public string Crop5
        {
            get { return _Crop5; }
            set { _Crop5 = value; this.NotifyChanged(); }
        }
        public float Crop1_Rate
        {
            get { return _Crop1_Rate; }
            set { _Crop1_Rate = value; this.NotifyChanged(); }
        }
        public float Crop2_Rate
        {
            get { return _Crop2_Rate; }
            set { _Crop2_Rate = value; this.NotifyChanged(); }
        }
        public float Crop3_Rate
        {
            get { return _Crop3_Rate; }
            set { _Crop3_Rate = value; this.NotifyChanged(); }
        }
        public float Crop4_Rate
        {
            get { return _Crop4_Rate; }
            set { _Crop4_Rate = value; this.NotifyChanged(); }
        }
        public float Crop5_Rate
        {
            get { return _Crop5_Rate; }
            set { _Crop5_Rate = value; this.NotifyChanged(); }
        }
    }
}
