using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using iMANTRA_BL;
using System.Collections;
using CUSTOM_iMANTRA_BL;

namespace CUSTOM_iMANTRA
{
    public class btn_event
    {
        private BL_BASEFIELD objBLFD = new BL_BASEFIELD();
        private BLHT objhashtable = new BLHT();

        private BL_Nessary_Fields bL_RELATED_FIELDS = new BL_Nessary_Fields();
        private Hashtable _hashRowItem = new Hashtable(StringComparer.InvariantCultureIgnoreCase);

        public Hashtable HashRowItem
        {
            get { return _hashRowItem; }
            set { _hashRowItem = value; }
        }

        private bool _blnHeaderOrItem;
        private string _btn_nm;

        public string Btn_nm
        {
            get { return _btn_nm; }
            set { _btn_nm = value; }
        }

        public bool BlnHeaderOrItem
        {
            get { return _blnHeaderOrItem; }
            set { _blnHeaderOrItem = value; }
        }

        public BL_Nessary_Fields BL_FIELDS
        {
            get { return bL_RELATED_FIELDS; }
            set { bL_RELATED_FIELDS = value; }
        }
        public BL_BASEFIELD ACTIVE_BL
        {
            get { return objBLFD; }
            set { objBLFD = value; }
        }

        public bool Button_Click_Event()
        {
            if (ACTIVE_BL.Code == "LR")
            {
                if (_btn_nm == "OTHER_DET" && !_blnHeaderOrItem)
                {
                    frmAdjustIssuetoReceipt objAdjustIssuetoReceipt = new frmAdjustIssuetoReceipt();
                    objAdjustIssuetoReceipt.ACTIVE_BL = objBLFD;
                    objAdjustIssuetoReceipt.HashRowList = _hashRowItem;
                    objAdjustIssuetoReceipt.ShowDialog();
                }
            }
            if (ACTIVE_BL.Code == "DE")
            {
                if (_btn_nm == "OTHER_DET" && _blnHeaderOrItem)
                {
                    frmDebitEntry objDebitEntry = new frmDebitEntry();
                    objDebitEntry.ACTIVE_BL = objBLFD;
                    objDebitEntry.ShowDialog();
                }
            }
            if (ACTIVE_BL.Code == "WO")
            {
                if (_btn_nm == "BOM_BTN" && !_blnHeaderOrItem)
                {
                    frmBOM_with_WorkOrder objBOM_with_WorkOrder = new frmBOM_with_WorkOrder();
                    objBOM_with_WorkOrder.ACTIVE_BL = objBLFD;
                    objBOM_with_WorkOrder.HashItemList = _hashRowItem;
                    objBOM_with_WorkOrder.ShowDialog();
                }
            }
            if (ACTIVE_BL.Code == "CE")
            {
                if (_btn_nm == "OTHER_DET" && _blnHeaderOrItem)
                {
                    frmip_wo objip_wo = new frmip_wo();
                    objip_wo.ACTIVE_BL = objBLFD;
                    objip_wo.ShowDialog();
                    BL_FIELDS.Bind_type = ACTIVE_BL.Tran_mode != "view_mode" ? "GRID" : "";

                }
            }
            if (ACTIVE_BL.Code == "PP")
            {
                if (_btn_nm == "OTHER_DET" && _blnHeaderOrItem)
                {
                    frmpp_wo objip_wo = new frmpp_wo();
                    objip_wo.ACTIVE_BL = objBLFD;
                    objip_wo.ShowDialog();
                    BL_FIELDS.Bind_type = ACTIVE_BL.Tran_mode != "view_mode" ? "GRID" : "";

                }
            }
            if (ACTIVE_BL.Code == "PD")
            {
                if (_btn_nm == "OTHER_DET" && _blnHeaderOrItem)
                {
                    frmop_wo objop_wo = new frmop_wo();
                    objop_wo.ACTIVE_BL = objBLFD;
                    objop_wo.ShowDialog();
                    BL_FIELDS.Bind_type = ACTIVE_BL.Tran_mode != "view_mode" ? "GRID" : "";
                }
            }
            if (ACTIVE_BL.Code == "OB")
            {
                if (_btn_nm == "OTHER_DET" && _blnHeaderOrItem)
                {
                    frmOpeningDuties objOpeningDuties = new frmOpeningDuties();
                    objOpeningDuties.ACTIVE_BL = objBLFD;
                    objOpeningDuties.ShowDialog();
                }
            }
            if (ACTIVE_BL.Code == "SE" || ACTIVE_BL.Code == "EI")
            {
                if (_btn_nm == "EXPORT_BTN")
                {
                    if (objBLFD.HTMAIN["TRAN_SR"].ToString() == "EXPORT")
                    {
                        if (objBLFD.HTMAIN["RULE"].ToString() == "CT-1" || objBLFD.HTMAIN["RULE"].ToString() == "CT-3")
                        {
                            frmExportDetailsInSales objExportDetailsInSales = new frmExportDetailsInSales();
                            objExportDetailsInSales.ACTIVE_BL = objBLFD;
                            objExportDetailsInSales.ShowDialog();
                        }
                    }
                }
            }
            if (ACTIVE_BL.IsApprove && ACTIVE_BL.IsReqAuthority)
            {
                if (_btn_nm == "APPROVAL_DET")
                {
                    frmApproval objfrmApprove = new frmApproval();
                    objfrmApprove.ACTIVE_BL = objBLFD;
                    objfrmApprove.ShowDialog();
                }
            }
            if (ACTIVE_BL.Code == "ST")
            {
                if (_btn_nm == "VALID_TRAN_BTN")
                {
                    frmListOfMenus objListOfMenus = new frmListOfMenus();
                    objListOfMenus.ObjBFD = objBLFD;
                    objListOfMenus.Validity_fld_nm = "valid_tran";
                    objListOfMenus.ShowDialog();
                }
            }
            if (ACTIVE_BL.IsSchedule || ACTIVE_BL.Code == "PH" || ACTIVE_BL.Code == "PB" || ACTIVE_BL.Code == "DS")
            {
                if (_btn_nm == "BTN_SCHEDULE" && !_blnHeaderOrItem)
                {
                    frmDispatchSchedule objfrmDispatchSchedule = new frmDispatchSchedule();
                    objfrmDispatchSchedule.ACTIVE_BL = objBLFD;
                    objfrmDispatchSchedule.HashItemList = _hashRowItem;
                    objfrmDispatchSchedule.ShowDialog();
                }
            }
            if (objBLFD.IsFileAttach && _btn_nm == "FILE_UPLOAD_BTN")
            {
                frmFileUpload objFileUpload = new frmFileUpload(_blnHeaderOrItem);
                objFileUpload.ACTIVE_BL = objBLFD;
                objFileUpload.HashItemList = _hashRowItem;
                objFileUpload.ShowDialog();
            }
            if (ACTIVE_BL.IsDeSchedule)
            {
                if (_btn_nm == "BTN_DE_SCHEDULE" && !_blnHeaderOrItem)
                {
                    frmDeallocateSchedule objfrmDeallocateSchedule = new frmDeallocateSchedule();
                    objfrmDeallocateSchedule.ACTIVE_BL = objBLFD;
                    objfrmDeallocateSchedule.HashItemList = _hashRowItem;
                    objfrmDeallocateSchedule.ShowDialog();
                }
            }
            //if (ACTIVE_BL.Ac_post)
            //{
            //    frmAccountAlloc objfrmAccountAlloc = new frmAccountAlloc();
            //    objfrmAccountAlloc.ACTIVE_BL = objBLFD;
            //    objfrmAccountAlloc.HashRowList = _hashRowItem;
            //    objfrmAccountAlloc.ShowDialog();
            //}
            return true;
        }
    }
}
