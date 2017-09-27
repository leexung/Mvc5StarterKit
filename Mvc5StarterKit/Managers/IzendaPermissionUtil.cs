using System;
using System.Collections.Generic;
using Izenda.BI.Framework.Models.Permissions;
using Izenda.BI.Framework.Models.Permissions.Dashboards;
using Izenda.BI.Framework.Models.Permissions.DataSetup;
using Izenda.BI.Framework.Models.Permissions.Emailing;
using Izenda.BI.Framework.Models.Permissions.Exporting;
using Izenda.BI.Framework.Models.Permissions.Reports;
using Izenda.BI.Framework.Models.Permissions.RoleSetup;
using Izenda.BI.Framework.Models.Permissions.Scheduling;
using Izenda.BI.Framework.Models.Permissions.Systemwide;
using Izenda.BI.Framework.Models.Permissions.TenantSetup;
using Izenda.BI.Framework.Models.Permissions.UserSetup;
using Permissions = Izenda.BI.Framework.Models.Permissions.RoleSetup.Permissions;

namespace Mvc5StarterKit.Managers
{
    public static class IzendaPermissionUtil
    {
        public static List<string> FullTenantModules = new List<string>
        {
            "Alerting",
            "Form",
            "Dashboard",
            "Report Templates",
            "Scheduling",
            "Exporting",
            "Report Designer",
            "Charting",
            "Maps"
        };
        public static Permission FullAccess
        {
            get
            {
                return fullAccess.Value;
            }
        }

        #region Properties
        /// <summary>
        /// The full access permission
        /// </summary>
        private static Lazy<Permission> fullAccess = new Lazy<Permission>(() => new Permission
        {
            SystemAdmin = false,
            FullReportAndDashboardAccess = true,
            SystemConfiguration = new Izenda.BI.Framework.Models.Permissions.SystemConfiguration.SystemConfiguration
            {
                ScheduledInstances = new Izenda.BI.Framework.Models.Permissions.SystemConfiguration.ScheduledInstances
                {
                    Value = true,
                    TenantAccess = 1
                },
                TenantAccess = 1
            },
            DataSetup = new DataSetup
            {
                DataModel = new DataModel
                {
                    Value = true,
                    CustomView = new CustomView
                    {
                        Create = true,
                        Delete = true,
                        Edit = true,
                        TenantAccess = 1
                    },
                    TenantAccess = 1
                },
                AdvancedSettings = new AdvancedSettings
                {
                    Category = true,
                    Others = true,
                    TenantAccess = 1
                },
                TenantAccess = 1
            },
            UserSetup = new UserSetup
            {
                Actions = new Izenda.BI.Framework.Models.Permissions.UserSetup.Actions
                {
                    ConfigureSecurityOptions = true,
                    Create = true,
                    Del = true,
                    Edit = true,
                    TenantAccess = 1
                },
                UserRoleAssociation = new UserRoleAssociation
                {
                    Value = true,
                    TenantAccess = 1
                },
                TenantAccess = 1
            },
            TenantSetup = new TenantSetup
            {
                Actions = new Izenda.BI.Framework.Models.Permissions.RoleSetup.Actions
                {
                    Create = true,
                    Del = true,
                    Edit = true,
                    TenantAccess = 1
                },
                Permissions = new Izenda.BI.Framework.Models.Permissions.TenantSetup.Permissions
                {
                    Value = true,
                    TenantAccess = 1
                },
                TenantAccess = 1
            },
            RoleSetup = new RoleSetup
            {
                Actions = new Izenda.BI.Framework.Models.Permissions.RoleSetup.Actions
                {
                    Create = true,
                    Del = true,
                    Edit = true,
                    TenantAccess = 1
                },
                DataModelAccess = new DataModelAccess
                {
                    Value = true,
                    TenantAccess = 1
                },
                GrantRoleWithFullReportAndDashboardAccess = new GrantRoleWithFullReportAndDashboardAccess
                {
                    Value = true,
                    TenantAccess = 1
                },
                Permissions = new Permissions
                {
                    Value = true,
                    TenantAccess = 1
                },
                TenantAccess = 1
            },
            Reports = new Reports
            {
                CanCreateNewReport = new CanCreateNewReport
                {
                    Value = true,
                    TenantAccess = 1
                },
                DataSources = new DataSources
                {
                    SimpleDataSources = false,
                    AdvancedDataSources = true,
                    TenantAccess = 1
                },
                ReportPartTypes = new ReportPartTypes
                {
                    Chart = true,
                    Form = true,
                    Gauge = true,
                    Map = true,
                    TenantAccess = 1
                },
                ReportCategoriesSubcategories = new ReportCategoriesSubcategories
                {
                    CanCreateNewCategory = new Izenda.BI.Framework.Models.Permissions.Reports.CanCreateNewCategory
                    {
                        Value = true,
                        TenantAccess = 1
                    }
                },
                FieldProperties = new FieldProperties
                {
                    CustomURL = true,
                    EmbeddedJavaScript = true,
                    Subreport = true,
                    TenantAccess = 1
                },
                FilterProperties = new FilterProperties
                {
                    FilterLogic = true,
                    CrossFiltering = true,
                    TenantAccess = 1
                },
                Actions = new Izenda.BI.Framework.Models.Permissions.Reports.Actions
                {
                    ConfigureAccessRights = true,
                    Del = true,
                    Email = true,
                    Exporting = true,
                    OverwriteExistingReport = true,
                    Print = true,
                    RegisterForAlerts = true,
                    Schedule = true,
                    Subscribe = true,
                    UnarchiveReportVersions = true,
                    ViewReportHistory = true,
                    TenantAccess = 1
                },
                TenantAccess = 1
            },
            Dashboards = new Dashboards
            {
                CanCreateNewDashboard = new CanCreateNewDashboard
                {
                    Value = true,
                    TenantAccess = 1
                },
                DashboardCategoriesSubcategories = new DashboardCategoriesSubcategories
                {
                    CanCreateNewCategory = new Izenda.BI.Framework.Models.Permissions.Dashboards.CanCreateNewCategory
                    {
                        Value = true,
                        TenantAccess = 1
                    }
                },
                Actions = new Izenda.BI.Framework.Models.Permissions.Dashboards.Actions
                {
                    ConfigureAccessRights = true,
                    Del = true,
                    Email = true,
                    OverwriteExistingDashboard = true,
                    Print = true,
                    Schedule = true,
                    Subscribe = true,
                    TenantAccess = 1
                },
                TenantAccess = 1
            },
            Scheduling = new Scheduling
            {
                SchedulingScope = new SchedulingScope
                {
                    ExternalUsers = true,
                    SystemUsers = true,
                    TenantAccess = 1
                },
                TenantAccess = 1
            },
            Emailing = new Emailing
            {
                AttachmentType = new AttachmentType
                {
                    Csv = true,
                    Excel = true,
                    Json = true,
                    Pdf = true,
                    Word = true,
                    Xml = true,
                    TenantAccess = 1
                },
                DeliveryMethod = new DeliveryMethod
                {
                    Attachment = true,
                    EmbeddedHTML = true,
                    Link = true,
                    TenantAccess = 1
                }
            },
            Exporting = new Exporting
            {
                ExportingFormat = new ExportingFormat
                {
                    Csv = true,
                    Excel = true,
                    Xml = true,
                    Json = true,
                    Pdf = true,
                    QueryExecution = true,
                    Word = true,
                    TenantAccess = 1
                },
                TenantAccess = 1
            },
            Systemwide = new Systemwide
            {
                CanSeeSystemMessages = new CanSeeSystemMessages
                {
                    Value = true,
                    TenantAccess = 1
                },
                TenantAccess = 1
            }
        });

        #endregion
    }
}