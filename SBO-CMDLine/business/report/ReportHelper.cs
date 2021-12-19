using System;
using System.IO;
using System.Runtime.InteropServices;
using SAPbobsCOM;
using SBO_CMDLine.business.company;
using SBO_CMDLine.system;

namespace SBO_CMDLine.business.report
{
    public class ReportHelper
    {
        public static bool RemoveReport(string typeName, string formType, string addonName)
        {
            bool result = false;

            ReportTypesService rptTypeService = (ReportTypesService) CompanyHelper.GetDICompany().GetCompanyService()
                .GetBusinessService(ServiceTypes.ReportTypesService);

            ReportLayoutsService layoutService = (ReportLayoutsService) CompanyHelper.GetDICompany().GetCompanyService()
                .GetBusinessService(ServiceTypes.ReportLayoutsService);

            ReportParams rParams = (ReportParams) layoutService
                .GetDataInterface(ReportLayoutsServiceDataInterfaces.rlsdiReportParams);


            try
            {
                ReportType newType = (ReportType) rptTypeService.GetDataInterface(ReportTypesServiceDataInterfaces.rtsReportType);

                newType.TypeName = typeName;
                newType.AddonName = addonName;
                newType.AddonFormType = formType;

                ReportTypesParams rtParams = rptTypeService.GetReportTypeList();

                for (var i = 0; i < rtParams.Count; i++)
                {
                    ReportTypeParams rtItem = rtParams.Item(i);

                    if (rtItem.AddonName.Equals(addonName) && rtItem.TypeName.Equals(typeName) && rtItem.AddonFormType.Equals(formType))
                    {
                        ReportType rType = rptTypeService.GetReportType(rtItem);
                        rType.DefaultReportLayout = "";
                        rptTypeService.UpdateReportType(rType);

                        //Remove layouts before deletion
                        rParams.ReportCode = rtItem.TypeCode;

                        ReportLayoutsParams rlParams = layoutService.GetReportLayoutList(rParams);
                        for (var j = 0; j < rlParams.Count; j++)
                        {
                            ReportLayoutParams rlItem = rlParams.Item(j);

                            if (string.IsNullOrEmpty(rtItem.MenuID))
                            {
                                layoutService.DeleteReportLayout(rlItem);
                            }
                            else
                            {
                                layoutService.DeleteReportLayoutAndMenu(rlItem);
                            }
                        }

                        rptTypeService.DeleteReportType(rType);
                        result = true;
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception($"Error removing report: {e.Message} {e.InnerException?.Message}");
            }
            finally
            {
                if (rptTypeService != null)
                    Marshal.ReleaseComObject(rptTypeService);

                if (layoutService != null)
                    Marshal.ReleaseComObject(layoutService);

                if (rParams != null)
                    Marshal.ReleaseComObject(rParams);
            }

            return result;
        }

        public static bool ImportReport(string filePath, string reportName, string typeName, string formType, string addonName, string menuId = null)
        {
            ReportTypesService typeService = (ReportTypesService) CompanyHelper.GetDICompany().GetCompanyService()
                .GetBusinessService(ServiceTypes.ReportTypesService);

            ReportLayoutsService layoutService = (ReportLayoutsService) CompanyHelper.GetDICompany().GetCompanyService()
                .GetBusinessService(ServiceTypes.ReportLayoutsService);

            BlobParams oBlobParams = (BlobParams) CompanyHelper.GetDICompany().GetCompanyService()
                .GetDataInterface(CompanyServiceDataInterfaces.csdiBlobParams);

            Blob oBlob = (Blob) CompanyHelper.GetDICompany().GetCompanyService().GetDataInterface(CompanyServiceDataInterfaces.csdiBlob);

            bool result = false;

            try
            {
                ReportType newType = (ReportType) typeService.GetDataInterface(ReportTypesServiceDataInterfaces.rtsReportType);

                newType.TypeName = typeName;
                newType.AddonName = addonName;
                newType.AddonFormType = formType;
                newType.MenuID = menuId;

                ReportTypeParams newTypeParam = typeService.AddReportType(newType);
                ReportLayout layout = (ReportLayout) layoutService.GetDataInterface(ReportLayoutsServiceDataInterfaces.rlsdiReportLayout);

                layout.Author = CompanyHelper.GetUsername();
                layout.Category = ReportLayoutCategoryEnum.rlcCrystal;
                layout.Name = reportName;
                layout.TypeCode = newTypeParam.TypeCode;

                ReportLayoutParams newReportParam;
                if (string.IsNullOrEmpty(menuId))
                {
                    newReportParam = layoutService.AddReportLayout(layout);
                }
                else
                {
                    newReportParam = layoutService.AddReportLayoutToMenu(layout, menuId);
                }

                newType = typeService.GetReportType(newTypeParam);
                newType.DefaultReportLayout = newReportParam.LayoutCode;
                typeService.UpdateReportType(newType);

                oBlobParams.Table = "RDOC";
                oBlobParams.Field = "Template";
                BlobTableKeySegment oKeySegment = oBlobParams.BlobTableKeySegments.Add();
                oKeySegment.Name = "DocCode";
                oKeySegment.Value = newReportParam.LayoutCode;
                string rptFilePath = filePath;

                FileStream oFile = new FileStream(rptFilePath, FileMode.Open);
                int fileSize = (int) oFile.Length;
                byte[] buf = new byte[fileSize];
                oFile.Read(buf, 0, fileSize);
                oFile.Dispose();

                oBlob.Content = Convert.ToBase64String(buf, 0, fileSize);
                CompanyHelper.GetDICompany().GetCompanyService().SetBlob(oBlobParams, oBlob);

                result = true;
            }
            catch (Exception e)
            {
                throw new Exception($"Error importing report: {e.Message} {e.InnerException?.Message}");
            }
            finally
            {
                if (typeService != null)
                    Marshal.ReleaseComObject(typeService);

                if (layoutService != null)
                    Marshal.ReleaseComObject(layoutService);

                if (oBlobParams != null)
                    Marshal.ReleaseComObject(oBlobParams);

                if (oBlob != null)
                    Marshal.ReleaseComObject(oBlob);
            }

            return result;
        }
    }
}