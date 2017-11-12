using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace iMANTRA_BL
{
    public class BL_MAIN_FIELDS
    {
        private string _curUser, _fin_yr, _comp_nm, _login_type, _no_of_users, _user_theme, _local_ip, _comp_type, _modules, _system_nm, _system_id, _motherboardId, _macId, _license_type, _license_dt, _priority_nm;
        private string _role_nm;

        public string Role_nm
        {
            get { return _role_nm; }
            set { _role_nm = value; }
        }

        public string Priority_nm
        {
            get { return _priority_nm; }
            set { _priority_nm = value; }
        }

        public string Comp_type
        {
            get { return _comp_type; }
            set { _comp_type = value; }
        }

        public string Modules
        {
            get { return _modules; }
            set { _modules = value; }
        }

        public string System_nm
        {
            get { return _system_nm; }
            set { _system_nm = value; }
        }

        public string System_id
        {
            get { return _system_id; }
            set { _system_id = value; }
        }

        public string MotherboardId
        {
            get { return _motherboardId; }
            set { _motherboardId = value; }
        }

        public string MacId
        {
            get { return _macId; }
            set { _macId = value; }
        }

        public string License_type
        {
            get { return _license_type; }
            set { _license_type = value; }
        }

        public string License_dt
        {
            get { return _license_dt; }
            set { _license_dt = value; }
        }

        public string Local_ip
        {
            get { return _local_ip; }
            set { _local_ip = value; }
        }

        public string User_theme
        {
            get { return _user_theme; }
            set { _user_theme = value; }
        }

        public string Login_type
        {
            get { return _login_type; }
            set { _login_type = value; }
        }

        public string Comp_nm
        {
            get { return _comp_nm; }
            set { _comp_nm = value; }
        }

        public string Fin_yr
        {
            get { return _fin_yr; }
            set { _fin_yr = value; }
        }

        public string No_of_users
        {
            get { return _no_of_users; }
            set { _no_of_users = value; }
        }


        private Hashtable _HASHTOOL = new Hashtable(StringComparer.InvariantCultureIgnoreCase);

        public string CurUser
        {
            get { return _curUser; }
            set { _curUser = value; }
        }
        public Hashtable HASHTOOL
        {
            get { return _HASHTOOL; }
            set { _HASHTOOL = value; }
        }

    }
}
