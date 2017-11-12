﻿using System;
using System.Runtime.InteropServices;
using System.Text;
using IniParser;

namespace iMANTRA_iniL
{
    public class Ini
    {
        public string path;
        IniParser.FileIniDataParser parser = new FileIniDataParser();
        IniData parsedData;

        public Ini()
        {
            path = AppDomain.CurrentDomain.BaseDirectory + "Setting.ini";
        }

        public string GetSectionDetails(string section_nm, string delimeter)
        {
            parsedData = parser.LoadFile(this.path);
            string strCon = "";
            foreach (SectionData section in parsedData.Sections)
            {
                if (section.SectionName == section_nm)
                {
                    foreach (KeyData key in section.Keys)
                    {
                        strCon += key.KeyName + "=" + key.Value + delimeter;
                    }
                    break;
                }
            }
            return strCon;
        }
        public string GetKeyFieldValue(string section_nm, string strKey_nm)
        {
            parsedData = parser.LoadFile(this.path);
            return parsedData[section_nm][strKey_nm];
        }
        public void SetKeyFieldValue(string section_nm, string strKey_nm, string strKeyValue)
        {
            parsedData = parser.LoadFile(this.path);
            parsedData[section_nm][strKey_nm] = strKeyValue;
            parser.SaveFile(path, parsedData);
        }
    }
}