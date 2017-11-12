using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace iMANTRA_BL
{
    public class Company
    {
        private int _compid;
        private bool _neg_pt_bal;
        private DateTime _cuser_dt, _Bkup_dt, _Fin_yr_sta, _Fin_yr_end, _muser_dt, _strat_dt, _end_dt;
        private decimal _rate_dec, _Decimal;
        private string _Remark, _Logo, _Dir_nm, _Pan_no, _Tin_no, _cst_no, _Tds_no, _Phone, _Email, _Comp_nm, _Add1, _Add2, _Add3, _holiday, _curr, _area_nm;
        private string _zone_nm, _city_nm, _zip, _state_nm, _country_nm, _cuser_nm, _Dir_bkup, _muser_nm, _fax, _website, _db_nm, _frm_bakup, _folder_nm, _tran_cd, _Fin_yr;
        private string _range, _division, _collrate, _ecc_no, _iec_no, _rangeadd, _colladd, _DIVADD, _Punch_ln, _ser_tax_no;
        private string _copy_nm, _start_it, _end_it, _start_ac, _end_ac, _pur_credit_per, _module_cd;

        public string Module_cd
        {
            get { return _module_cd; }
            set { _module_cd = value; }
        }

        public string Pur_credit_per
        {
            get { return _pur_credit_per; }
            set { _pur_credit_per = value; }
        }

        public string Copy_nm
        {
            get { return _copy_nm; }
            set { _copy_nm = value; }
        }
        public string Start_ac
        {
            get { return _start_ac; }
            set { _start_ac = value; }
        }
        public string End_ac
        {
            get { return _end_ac; }
            set { _end_ac = value; }
        }
        public string Start_it
        {
            get { return _start_it; }
            set { _start_it = value; }
        }
        public string End_it
        {
            get { return _end_it; }
            set { _end_it = value; }
        }
        public DateTime Start_dt
        {
            get { return _strat_dt; }
            set { _strat_dt = value; }
        }
        public DateTime End_dt
        {
            get { return _end_dt; }
            set { _end_dt = value; }
        }

        public string Cst_no
        {
            get { return _cst_no; }
            set { _cst_no = value; }
        }
        public string Tin_no
        {
            get { return _Tin_no; }
            set { _Tin_no = value; }
        }
        public string Email
        {
            get { return _Email; }
            set { _Email = value; }
        }
        public string Phone
        {
            get { return _Phone; }
            set { _Phone = value; }
        }
        public string Add3
        {
            get { return _Add3; }
            set { _Add3 = value; }
        }
        public string Add2
        {
            get { return _Add2; }
            set { _Add2 = value; }
        }
        public string Add1
        {
            get { return _Add1; }
            set { _Add1 = value; }
        }
        public string Website
        {
            get { return _website; }
            set { _website = value; }
        }
        public string City_nm
        {
            get { return _city_nm; }
            set { _city_nm = value; }
        }
        public string Fax
        {
            get { return _fax; }
            set { _fax = value; }
        }
        public string Zip
        {
            get { return _zip; }
            set { _zip = value; }
        }
        public int Compid
        {
            get { return _compid; }
            set { _compid = value; }
        }
        public string Fin_yr
        {
            get { return _Fin_yr; }
            set { _Fin_yr = value; }
        }
        public string Comp_nm
        {
            get { return _Comp_nm; }
            set { _Comp_nm = value; }
        }
        public string Db_nm
        {
            get { return _db_nm; }
            set { _db_nm = value; }
        }
        public string State_nm
        {
            get { return _state_nm; }
            set { _state_nm = value; }
        }
        public decimal Rate_dec
        {
            get { return _rate_dec; }
            set { _rate_dec = value; }
        }
        public decimal Decimal
        {
            get { return _Decimal; }
            set { _Decimal = value; }
        }
        public DateTime Fin_yr_end
        {
            get { return _Fin_yr_end; }
            set { _Fin_yr_end = value; }
        }
        public DateTime Fin_yr_sta
        {
            get { return _Fin_yr_sta; }
            set { _Fin_yr_sta = value; }
        }
        public string Area_nm
        {
            get { return _area_nm; }
            set { _area_nm = value; }
        }
        public string Curr
        {
            get { return _curr; }
            set { _curr = value; }
        }
        public string Holiday
        {
            get { return _holiday; }
            set { _holiday = value; }
        }
        public string Tds_no
        {
            get { return _Tds_no; }
            set { _Tds_no = value; }
        }
        public string Pan_no
        {
            get { return _Pan_no; }
            set { _Pan_no = value; }
        }
        public string Dir_nm
        {
            get { return _Dir_nm; }
            set { _Dir_nm = value; }
        }
        public string Logo
        {
            get { return _Logo; }
            set { _Logo = value; }
        }
        public string Remark
        {
            get { return _Remark; }
            set { _Remark = value; }
        }
        public string Cuser_nm
        {
            get { return _cuser_nm; }
            set { _cuser_nm = value; }
        }
        public string Zone_nm
        {
            get { return _zone_nm; }
            set { _zone_nm = value; }
        }
        public string Range
        {
            get { return _range; }
            set { _range = value; }
        }
        public string Division
        {
            get { return _division; }
            set { _division = value; }
        }
        public string Collrate
        {
            get { return _collrate; }
            set { _collrate = value; }
        }
        public string Ecc_no
        {
            get { return _ecc_no; }
            set { _ecc_no = value; }
        }
        public string Iec_no
        {
            get { return _iec_no; }
            set { _iec_no = value; }
        }
        public string Rangeadd
        {
            get { return _rangeadd; }
            set { _rangeadd = value; }
        }
        public string Colladd
        {
            get { return _colladd; }
            set { _colladd = value; }
        }
        public string DIVADD
        {
            get { return _DIVADD; }
            set { _DIVADD = value; }
        }
        public string Punch_ln
        {
            get { return _Punch_ln; }
            set { _Punch_ln = value; }
        }
        public string Ser_tax_no
        {
            get { return _ser_tax_no; }
            set { _ser_tax_no = value; }
        }
        public string Folder_nm
        {
            get { return _folder_nm; }
            set { _folder_nm = value; }
        }

        public string Frm_bakup
        {
            get { return _frm_bakup; }
            set { _frm_bakup = value; }
        }

        public string Muser_nm
        {
            get { return _muser_nm; }
            set { _muser_nm = value; }
        }

        public string Dir_bkup
        {
            get { return _Dir_bkup; }
            set { _Dir_bkup = value; }
        }

        public string Country_nm
        {
            get { return _country_nm; }
            set { _country_nm = value; }
        }

        public string Tran_cd
        {
            get { return _tran_cd; }
            set { _tran_cd = value; }
        }
        public DateTime Muser_dt
        {
            get { return _muser_dt; }
            set { _muser_dt = value; }
        }

        public DateTime Bkup_dt
        {
            get { return _Bkup_dt; }
            set { _Bkup_dt = value; }
        }

        public DateTime Cuser_dt
        {
            get { return _cuser_dt; }
            set { _cuser_dt = value; }
        }

        public bool Neg_pt_bal
        {
            get { return _neg_pt_bal; }
            set { _neg_pt_bal = value; }
        }
    }
}
