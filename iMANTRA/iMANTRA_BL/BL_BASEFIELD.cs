using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data;
using CUSTOM_iMANTRA_BL;

namespace iMANTRA_BL
{
    public class BL_BASEFIELD
    {
        private bool _blnStopItemEnter, _isRule, _locate_fin_yr = false, _ac_post = false, _round_acc = false, _over_cr_dr = false, _isDefAccTranType = false;

        private string _tran_cd, _tran_mode, _behavier_cd, _ref_behaiver_cd, _extra_tbl_nm, _tran_no_nm, _ac_tbl_nm, _alloc_tbl_nm;
        private string _tran_id, tran_type, _primary_id, _main_tbl_nm, _item_tbl_nm, _ref_tbl_nm, _tbl_catalog = "", _ref_type, _defrep_nm, _approve_tbl_nm;
        private string _code, _Tran_nm, _active_frm, _formCondition = "", _acc_narr, _dr_ac_nm, _dr_ac_id, _cr_ac_nm, _cr_ac_id;

        private string _acc_alias_credit, _acc_alias_debit, _acc_alias_dc_header_credit, _acc_alias_dc_header_debit, _acc_alias_dc_credit, _acc_alias_dc_debit, _acc_alias_st;
        private string _Def_Narr, _Tran_no_wid, _Tran_color, _module_cd;
        private string _Pt_type_avail, _bck_entry, _Pt_pop_sel, _Ac_pop_sel, _disp_locate, _stk_effect, _CompId, _due_dt_on, _copies_nm;
        private string _Tran_no_depd, _def_acc, _def_consignee, _def_acc_id, _def_consignee_id;
        private bool _flg_PickUp = true, _isFileAttach = false, _isAmendment = false, _isSchedule = false, _isDeSchedule = false, _isSendMail = false, _isTransferApproval = false, _isReqAuthority = false;

        private bool _block, _auto_tran_no, _Tran_narr, _Pt_narr, _ac_narr, _prod_det, _due_dt, _activate_acc, _prnt_saving, _validity, _curr_date, _T_stax, _PT_stax, _round_stax, _print_once, _edit_tran_no, _round_groamt, _round_asses_amt, _cons, _ac_pt_info, _isDCApp, _isdispPL, _isApprove, _isTransCopy, _isTaxApp, _isTaxRound, _filter_req;

        private int _x_gridAccount = 0, _y_gridAccount = 0, _hgt_gridAccount = 0, _width_gridAccount = 0;

        public int Y_gridAccount
        {
            get { return _y_gridAccount; }
            set { _y_gridAccount = value; }
        }

        public int Hgt_gridAccount
        {
            get { return _hgt_gridAccount; }
            set { _hgt_gridAccount = value; }
        }

        public int Width_gridAccount
        {
            get { return _width_gridAccount; }
            set { _width_gridAccount = value; }
        }

        public int X_gridAccount
        {
            get { return _x_gridAccount; }
            set { _x_gridAccount = value; }
        }

        public bool IsDefAccTranType
        {
            get { return _isDefAccTranType; }
            set { _isDefAccTranType = value; }
        }

        public bool Over_cr_dr
        {
            get { return _over_cr_dr; }
            set { _over_cr_dr = value; }
        }

        public bool Round_acc
        {
            get { return _round_acc; }
            set { _round_acc = value; }
        }

        public bool Ac_post
        {
            get { return _ac_post; }
            set { _ac_post = value; }
        }

        public bool Locate_fin_yr//check current fin yr 
        {
            get { return _locate_fin_yr; }
            set { _locate_fin_yr = value; }
        }
        

        public string Acc_alias_dc_header_debit
        {
            get { return _acc_alias_dc_header_debit; }
            set { _acc_alias_dc_header_debit = value; }
        }

        public string Acc_alias_dc_header_credit
        {
            get { return _acc_alias_dc_header_credit; }
            set { _acc_alias_dc_header_credit = value; }
        }

        public string Acc_alias_st
        {
            get { return _acc_alias_st; }
            set { _acc_alias_st = value; }
        }

        public string Acc_alias_dc_debit
        {
            get { return _acc_alias_dc_debit; }
            set { _acc_alias_dc_debit = value; }
        }

        public string Acc_alias_dc_credit
        {
            get { return _acc_alias_dc_credit; }
            set { _acc_alias_dc_credit = value; }
        }

        public string Acc_alias_debit
        {
            get { return _acc_alias_debit; }
            set { _acc_alias_debit = value; }
        }

        public string Acc_alias_credit
        {
            get { return _acc_alias_credit; }
            set { _acc_alias_credit = value; }
        }


        public string Cr_ac_id
        {
            get { return _cr_ac_id; }
            set { _cr_ac_id = value; }
        }

        public string Cr_ac_nm
        {
            get { return _cr_ac_nm; }
            set { _cr_ac_nm = value; }
        }

        public string Dr_ac_id
        {
            get { return _dr_ac_id; }
            set { _dr_ac_id = value; }
        }

        public string Dr_ac_nm
        {
            get { return _dr_ac_nm; }
            set { _dr_ac_nm = value; }
        }

        public string Acc_narr
        {
            get { return _acc_narr; }
            set { _acc_narr = value; }
        }

        public string FormCondition
        {
            get { return _formCondition; }
            set { _formCondition = value; }
        }

        public string Module_cd
        {
            get { return _module_cd; }
            set { _module_cd = value; }
        }
        public bool IsReqAuthority
        {
            get { return _isReqAuthority; }
            set { _isReqAuthority = value; }
        }

        public bool IsTransferApproval
        {
            get { return _isTransferApproval; }
            set { _isTransferApproval = value; }
        }

        public bool IsSendMail
        {
            get { return _isSendMail; }
            set { _isSendMail = value; }
        }

        protected internal string _variable = "";

        public bool IsDeSchedule
        {
            get { return _isDeSchedule; }
            set { _isDeSchedule = value; }
        }

        public bool IsSchedule
        {
            get { return _isSchedule; }
            set { _isSchedule = value; }
        }

        public bool IsAmendment
        {
            get { return _isAmendment; }
            set { _isAmendment = value; }
        }

        public bool IsFileAttach
        {
            get { return _isFileAttach; }
            set { _isFileAttach = value; }
        }

        public bool Flg_PickUp
        {
            get { return _flg_PickUp; }
            set { _flg_PickUp = value; }
        }

        public string Def_consignee_id
        {
            get { return _def_consignee_id; }
            set { _def_consignee_id = value; }
        }

        public bool IsRule
        {
            get { return _isRule; }
            set { _isRule = value; }
        }
        public string Def_acc_id
        {
            get { return _def_acc_id; }
            set { _def_acc_id = value; }
        }

        public string Def_consignee
        {
            get { return _def_consignee; }
            set { _def_consignee = value; }
        }

        public string Def_acc
        {
            get { return _def_acc; }
            set { _def_acc = value; }
        }

       

        public bool Filter_req
        {
            get { return _filter_req; }
            set { _filter_req = value; }
        }

        public bool Prod_det
        {
            get { return _prod_det; }
            set { _prod_det = value; }
        }

        private string _curr_date_time, _seachfield, _ref_tran_fld, _ac_grp, _tran_mode_type = "regular";
        private BL_MAIN_FIELDS _objLoginUser = new BL_MAIN_FIELDS();
        private BL_TRAN_SET _objBL_TRAN_SET = new BL_TRAN_SET();

        public BL_TRAN_SET ObjBL_TRAN_SET
        {
            get { return _objBL_TRAN_SET; }
            set { _objBL_TRAN_SET = value; }
        }

        private DataSet _dsBASEFIELDMAIN = new DataSet();
        private DataSet _dsBASEFIELDITEM = new DataSet();
        private DataSet _dsBASEADDIFIELD = new DataSet();
        private DataSet _dsBASEADDIFIELDITEM = new DataSet();
        private DataSet _dsHeader = new DataSet();
        private DataSet _dsFooter = new DataSet();
        private DataSet _dsDCITEMFIELDS = new DataSet();
        private DataSet _dsDCHEADRFIELDS = new DataSet();
        private DataSet _dsSTFIELDS = new DataSet();
        private DataSet _dsetview = new DataSet();
        private DataSet _dsetview1 = new DataSet();
        private DataSet _dsAccountPosting = new DataSet();

        public DataSet DsAccountPosting
        {
            get { return _dsAccountPosting; }
            set { _dsAccountPosting = value; }
        }

        private Hashtable _HTCUSTOMEFIELDS = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
        private Hashtable _HTMAIN = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
        private Hashtable _HTITEM = new Hashtable(StringComparer.InvariantCultureIgnoreCase);        
        private Hashtable _HTEXTRA = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
        private Hashtable _HTACDET = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
        private Hashtable _hashacItem = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
        private Hashtable _htaccountAmountdet = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
        private Hashtable _hashAccountAlloc = new Hashtable(StringComparer.InvariantCultureIgnoreCase);

        public Hashtable HT_ALLOC
        {
            get { return _hashAccountAlloc; }
            set { _hashAccountAlloc = value; }
        }        

        public Hashtable HtaccountAmountdet
        {
            get { return _htaccountAmountdet; }
            set { _htaccountAmountdet = value; }
        }

        public Hashtable HashacItem
        {
            get { return _hashacItem; }
            set { _hashacItem = value; }
        }

        public Hashtable HT_ACDET
        {
            get { return _HTACDET; }
            set { _HTACDET = value; }
        }

        public Hashtable HTEXTRA
        {
            get { return _HTEXTRA; }
            set { _HTEXTRA = value; }
        }
        private Hashtable _htitem_details = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
        private Hashtable _htextra_details = new Hashtable(StringComparer.InvariantCultureIgnoreCase);

        public Hashtable Htextra_details
        {
            get { return _htextra_details; }
            set { _htextra_details = value; }
        }
        private Hashtable _HTITEM_VALUE = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
        private Hashtable _HTDCFIELDS = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
        private Hashtable _HTMAINREF = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
        private Hashtable _HTITEMREF = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
        private Hashtable _hashitemref = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
        Hashtable _htPur_Ref = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
        private Hashtable _htModuleList = new Hashtable(StringComparer.InvariantCultureIgnoreCase);

        public Hashtable HtModuleList
        {
            get { return _htModuleList; }
            set { _htModuleList = value; }
        }

        private DataSet _dsNavigation = new DataSet();
        private DataSet _dsTab = new DataSet();
        private DataSet _dsTabDet = new DataSet();
        private DataSet _dsDateTime = new DataSet();
        private DataSet _dsetFileUpload = new DataSet();

        public DataSet DsDateTime
        {
            get { return _dsDateTime; }
            set { _dsDateTime = value; }
        }
        public DataSet DsetFileUpload
        {
            get { return _dsetFileUpload; }
            set { _dsetFileUpload = value; }
        }

        private Company _objCompany;
        private BLHT _HASHTABLES;
        private BL_CONTROL_SET _objControlSet;

        public BL_CONTROL_SET ObjControlSet
        {
            get { return _objControlSet; }
            set { _objControlSet = value; }
        }


        public BL_MAIN_FIELDS ObjLoginUser
        {
            get { return _objLoginUser; }
            set { _objLoginUser = value; }
        }
        public string Approve_tbl_nm
        {
            get { return _approve_tbl_nm; }
            set { _approve_tbl_nm = value; }
        }
        public bool BlnStopItemEnter
        {
            get { return _blnStopItemEnter; }
            set { _blnStopItemEnter = value; }
        }
        public string Tran_no_nm
        {
            get { return _tran_no_nm; }
            set { _tran_no_nm = value; }
        }
        public string Active_frm
        {
            get { return _active_frm; }
            set { _active_frm = value; }
        }
        public bool IsTaxRound
        {
            get { return _isTaxRound; }
            set { _isTaxRound = value; }
        }

        public bool IsTaxApp
        {
            get { return _isTaxApp; }
            set { _isTaxApp = value; }
        }

        public bool IsTransCopy
        {
            get { return _isTransCopy; }
            set { _isTransCopy = value; }
        }

        public bool IsApprove1
        {
            get { return _isApprove; }
            set { _isApprove = value; }
        }

        public bool IsApprove
        {
            get { return IsApprove1; }
            set { IsApprove1 = value; }
        }

        public bool IsdispPL
        {
            get { return _isdispPL; }
            set { _isdispPL = value; }
        }

        public bool IsDCApp
        {
            get { return _isDCApp; }
            set { _isDCApp = value; }
        }

        public bool Ac_pt_info
        {
            get { return _ac_pt_info; }
            set { _ac_pt_info = value; }
        }

        public bool Round_asses_amt
        {
            get { return _round_asses_amt; }
            set { _round_asses_amt = value; }
        }

        public bool Round_groamt
        {
            get { return _round_groamt; }
            set { _round_groamt = value; }
        }

        public string Ac_grp
        {
            get { return _ac_grp; }
            set { _ac_grp = value; }
        }

        public string Ref_tran_fld
        {
            get { return _ref_tran_fld; }
            set { _ref_tran_fld = value; }
        }

        public string Seachfield
        {
            get { return _seachfield; }
            set { _seachfield = value; }
        }
        public BLHT HASHTABLES
        {
            get { return _HASHTABLES; }
            set { _HASHTABLES = value; }
        }

        public Company ObjCompany
        {
            get { return _objCompany; }
            set { _objCompany = value; }
        }

        public string Curr_date_time
        {
            get { return _curr_date_time; }
            set { _curr_date_time = value; }
        }

        public bool Curr_date
        {
            get { return _curr_date; }
            set { _curr_date = value; }
        }

        public string Code
        {
            get { return _code; }
            set { _code = value; }
        }
        public string Tran_nm
        {
            get { return _Tran_nm; }
            set { _Tran_nm = value; }
        }
        public string Ac_pop_sel
        {
            get { return _Ac_pop_sel; }
            set { _Ac_pop_sel = value; }
        }
        public string Tran_color
        {
            get { return _Tran_color; }
            set { _Tran_color = value; }
        }
        public bool Validity
        {
            get { return _validity; }
            set { _validity = value; }
        }
        public bool Prnt_saving
        {
            get { return _prnt_saving; }
            set { _prnt_saving = value; }
        }
        public bool Activate_acc
        {
            get { return _activate_acc; }
            set { _activate_acc = value; }
        }
        public bool Due_dt
        {
            get { return _due_dt; }
            set { _due_dt = value; }
        }
        public bool Ac_narr
        {
            get { return _ac_narr; }
            set { _ac_narr = value; }
        }
        public bool Pt_narr
        {
            get { return _Pt_narr; }
            set { _Pt_narr = value; }
        }
        public bool Tran_narr
        {
            get { return _Tran_narr; }
            set { _Tran_narr = value; }
        }
        public string Tran_no_wid
        {
            get { return _Tran_no_wid; }
            set { _Tran_no_wid = value; }
        }
        public string Def_Narr
        {
            get { return _Def_Narr; }
            set { _Def_Narr = value; }
        }
        public bool Block
        {
            get { return _block; }
            set { _block = value; }
        }

        public bool Auto_tran_no
        {
            get { return _auto_tran_no; }
            set { _auto_tran_no = value; }
        }
        public string Defrep_nm
        {
            get { return _defrep_nm; }
            set { _defrep_nm = value; }
        }
        public string Ref_type
        {
            get { return _ref_type; }
            set { _ref_type = value; }
        }
        public string Ref_tbl_nm
        {
            get { return _ref_tbl_nm; }
            set { _ref_tbl_nm = value; }
        }
        public string Tbl_catalog
        {
            get { return _tbl_catalog; }
            set { _tbl_catalog = value; }
        }
        public string Primary_id
        {
            get { return _primary_id; }
            set { _primary_id = value; }
        }
        public string Item_tbl_nm
        {
            get { return _item_tbl_nm; }
            set { _item_tbl_nm = value; }
        }
        public string Main_tbl_nm
        {
            get { return _main_tbl_nm; }
            set { _main_tbl_nm = value; }
        }
        public string Alloc_tbl_nm
        {
            get { return _alloc_tbl_nm; }
            set { _alloc_tbl_nm = value; }
        }

        public string Ac_tbl_nm
        {
            get { return _ac_tbl_nm; }
            set { _ac_tbl_nm = value; }
        }

        public string Tran_type
        {
            get { return tran_type; }
            set { tran_type = value; }
        }
        public string Tran_id
        {
            get { return _tran_id; }
            set { _tran_id = value; }
        }
        public string TRAN_CD
        {
            get { return _tran_cd; }
            set { _tran_cd = value; }
        }
        public string Tran_mode
        {
            get { return _tran_mode; }
            set { _tran_mode = value; }
        }
        public string Behavier_cd
        {
            get { return _behavier_cd; }
            set { _behavier_cd = value; }
        }
        public string Ref_behaiver_cd
        {
            get { return _ref_behaiver_cd; }
            set { _ref_behaiver_cd = value; }
        }
        public string Extra_tbl_nm
        {
            get { return _extra_tbl_nm; }
            set { _extra_tbl_nm = value; }
        }

        public bool Print_once
        {
            get { return _print_once; }
            set { _print_once = value; }
        }
        public bool Round_stax
        {
            get { return _round_stax; }
            set { _round_stax = value; }
        }
        public bool PT_stax
        {
            get { return _PT_stax; }
            set { _PT_stax = value; }
        }
        public bool T_stax
        {
            get { return _T_stax; }
            set { _T_stax = value; }
        }
        public string Copies_nm
        {
            get { return _copies_nm; }
            set { _copies_nm = value; }
        }
        public string Due_dt_on
        {
            get { return _due_dt_on; }
            set { _due_dt_on = value; }
        }
        public string CompId
        {
            get { return _CompId; }
            set { _CompId = value; }
        }
        public string Stk_effect
        {
            get { return _stk_effect; }
            set { _stk_effect = value; }
        }
        public string Disp_locate
        {
            get { return _disp_locate; }
            set { _disp_locate = value; }
        }
        public string Pt_pop_sel
        {
            get { return _Pt_pop_sel; }
            set { _Pt_pop_sel = value; }
        }
        public string Bck_entry
        {
            get { return _bck_entry; }
            set { _bck_entry = value; }
        }
        public string Pt_type_avail
        {
            get { return _Pt_type_avail; }
            set { _Pt_type_avail = value; }
        }

        public string Tran_no_depd
        {
            get { return _Tran_no_depd; }
            set { _Tran_no_depd = value; }
        }
        public bool Cons
        {
            get { return _cons; }
            set { _cons = value; }
        }
        public bool Edit_tran_no
        {
            get { return _edit_tran_no; }
            set { _edit_tran_no = value; }
        }
        public Hashtable Hashitemref
        {
            get { return _hashitemref; }
            set { _hashitemref = value; }
        }
        public DataSet dsNavigation
        {
            get { return _dsNavigation; }
            set { _dsNavigation = value; }
        }
        public DataSet dsBASEFIELDMAIN
        {
            get { return _dsBASEFIELDMAIN; }
            set { _dsBASEFIELDMAIN = value; }
        }
        public DataSet dsBASEFIELDITEM
        {
            get { return _dsBASEFIELDITEM; }
            set { _dsBASEFIELDITEM = value; }
        }
        public DataSet dsBASEADDIFIELD
        {
            get { return _dsBASEADDIFIELD; }
            set { _dsBASEADDIFIELD = value; }
        }
        public DataSet dsBASEADDIFIELDITEM
        {
            get { return _dsBASEADDIFIELDITEM; }
            set { _dsBASEADDIFIELDITEM = value; }
        }
        public DataSet dsHeader
        {
            get { return _dsHeader; }
            set { _dsHeader = value; }
        }
        public DataSet dsFooter
        {
            get { return _dsFooter; }
            set { _dsFooter = value; }
        }
        public DataSet dsDCITEMFIELDS
        {
            get { return _dsDCITEMFIELDS; }
            set { _dsDCITEMFIELDS = value; }
        }
        public DataSet dsDCHEADRFIELDS
        {
            get { return _dsDCHEADRFIELDS; }
            set { _dsDCHEADRFIELDS = value; }
        }
        public DataSet dsSTFIELDS
        {
            get { return _dsSTFIELDS; }
            set { _dsSTFIELDS = value; }
        }
        public DataSet dsetview
        {
            get { return _dsetview; }
            set { _dsetview = value; }
        }
        public DataSet dsetview1
        {
            get { return _dsetview1; }
            set { _dsetview1 = value; }
        }
        public Hashtable HTCUSTOMEFIELDS
        {
            get { return _HTCUSTOMEFIELDS; }
            set { _HTCUSTOMEFIELDS = value; }
        }
        public Hashtable HTMAIN
        {
            get { return _HTMAIN; }
            set { _HTMAIN = value; }
        }
        public Hashtable HTITEM
        {
            get { return _HTITEM; }
            set { _HTITEM = value; }
        }
        // private Hashtable HTITEM_details = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
        public Hashtable htitem_details
        {
            get { return _htitem_details; }
            set { _htitem_details = value; }
        }

        public Hashtable HTITEM_VALUE
        {
            get { return _HTITEM_VALUE; }
            set { _HTITEM_VALUE = value; }
        }
        public Hashtable HTDCFIELDS
        {
            get { return _HTDCFIELDS; }
            set { _HTDCFIELDS = value; }
        }
        public Hashtable HTMAINREF
        {
            get { return _HTMAINREF; }
            set { _HTMAINREF = value; }
        }
        public Hashtable HTITEMREF
        {
            get { return _HTITEMREF; }
            set { _HTITEMREF = value; }
        }
        public DataSet dsTab
        {
            get { return _dsTab; }
            set { _dsTab = value; }
        }
        public DataSet dsTabDet
        {
            get { return _dsTabDet; }
            set { _dsTabDet = value; }
        }
        public Hashtable HtPur_Ref
        {
            get { return _htPur_Ref; }
            set { _htPur_Ref = value; }
        }
        public string Tran_mode_type
        {
            get { return _tran_mode_type; }
            set { _tran_mode_type = value; }
        }
    }
}
