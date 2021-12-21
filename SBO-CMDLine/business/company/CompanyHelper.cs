using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using SAPbobsCOM;
using SAPbouiCOM;
using Company = SAPbobsCOM.Company;

namespace SBO_CMDLine.business.company
{
    public class CompanyHelper
    {
        /// <summary>
        /// Default Connection String
        /// </summary>
        private const string CS = "0030002C0030002C00530041005000420044005F00440061007400650076002C0050004C006F006D0056004900490056";

        /// <summary>
        /// Application to use UI API
        /// </summary>
        private static Application _sboApplication;

        /// <summary>
        /// Access to DI API
        /// </summary>
        private static Company _company;

        /// <summary>
        /// Connect to current SBO application
        /// </summary>
        public static void ConnectUI()
        {
            // *******************************************************************
            // // Use an SboGuiApi object to establish connection
            // // with the SAP Business One application and return an
            // // initialized application object
            // *******************************************************************
            try
            {
                SboGuiApi sboGuiApi = null;
                sboGuiApi = new SboGuiApi();

                // connect to a running SBO Application
                sboGuiApi.Connect(CS);

                // get an initialized application object
                _sboApplication = sboGuiApi.GetApplication();
                _company = (Company) _sboApplication.Company.GetDICompany();
            }
            catch (Exception e)
            {
                throw new Exception($"Error connecting company: {e.Message}");
            }
        }

        /// <summary>
        /// Get list of companies
        /// </summary>
        /// <returns></returns>
        public static List<string> GetCompanyList(bool verbose = false)
        {
            string verboseFormat = "{0}\n\tName: {1}\n\tVersion: {2}\n\tLocalization: {3}";

            List<string> result = new List<string>();

            if (_company != null)
            {
                var oRecordSet = _company.GetCompanyList();
                oRecordSet.MoveFirst();

                if (verbose)
                {
                    while (oRecordSet.EoF != true)
                    {
                        string str = String.Format(verboseFormat,
                            oRecordSet.Fields.Item(0).Value,
                            oRecordSet.Fields.Item(1).Value,
                            oRecordSet.Fields.Item(2).Value,
                            oRecordSet.Fields.Item(4).Value);
                        result.Add(str);
                        oRecordSet.MoveNext();
                    }
                }
                else
                {
                    while (oRecordSet.EoF != true)
                    {
                        result.Add(oRecordSet.Fields.Item(0).Value.ToString());
                        oRecordSet.MoveNext();
                    }
                }


                Marshal.ReleaseComObject(oRecordSet);
            }

            return result;
        }

        /// <summary>
        /// Connect to DI company using credential file.
        /// </summary>
        /// <param name="connectionFile">XML file containing credentials.</param>
        public static void ConnectDI(string connectionFile)
        {
            if (String.IsNullOrEmpty(connectionFile) || !File.Exists(connectionFile))
                throw new Exception($"Error reading credential file: {connectionFile}");

            Credential cred = Credential.GetInstance(connectionFile);
            ConnectDI(cred.ServerName,
                (BoDataServerTypes) cred.DbType,
                cred.CompanyDbName,
                cred.LicenseServerAndPort,
                cred.UserName,
                cred.Password
            );
        }

        /// <summary>
        /// Connect to DI company.
        /// </summary>
        /// <param name="server"></param>
        /// <param name="dbType"></param>
        /// <param name="companyDbName"></param>
        /// <param name="licenseServerAndPort"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        public static void ConnectDI(string server, BoDataServerTypes dbType, string companyDbName,
            string licenseServerAndPort, string username,
            string password)
        {
            try
            {
                // Set connection properties
                _company = new Company();
                _company.Server = server;
                _company.DbServerType = dbType;
                _company.CompanyDB = companyDbName;
                _company.LicenseServer = licenseServerAndPort;
                _company.SLDServer = licenseServerAndPort;
                _company.UserName = username;
                _company.Password = password;

                int lRetCode = _company.Connect();

                if (lRetCode != 0)
                    Console.WriteLine($"Connection error: {_company.GetLastErrorDescription()}");
            }
            catch (Exception e)
            {
                throw new Exception($"Error connecting DI company: {e.Message}");
            }
        }

        public static Company GetDICompany()
        {
            return _company;
        }

        public static SAPbouiCOM.Company GetCompany()
        {
            return _sboApplication.Company;
        }

        public static Application GetApplication()
        {
            return _sboApplication;
        }

        public static string GetUsername()
        {
            if (GetCompany() != null)
                return GetCompany().UserName;

            return _company.UserName;
        }

        public static void Disconnect()
        {
            _company.Disconnect();
        }
    }
}