// ---------------------------------------------------------------------- 
// <copyright file="Permission.cs" company="Izenda">
//  Copyright (c) 2015 Izenda, Inc.                          
//  ALL RIGHTS RESERVED                
//                                                                         
//  The entire contents of this file is protected by U.S. and      
//  International Copyright Laws. Unauthorized reproduction,        
//  reverse-engineering, and distribution of all or any portion of  
//  the code contained in this file is strictly prohibited and may  
//  result in severe civil and criminal penalties and will be      
//  prosecuted to the maximum extent possible under the law.        
//                                                                  
//  RESTRICTIONS                                                    
//                                                                  
//  THIS SOURCE CODE AND ALL RESULTING INTERMEDIATE FILES          
//  ARE CONFIDENTIAL AND PROPRIETARY TRADE                          
//  SECRETS OF IZENDA INC. THE REGISTERED DEVELOPER IS  
//  LICENSED TO DISTRIBUTE THE PRODUCT AND ALL ACCOMPANYING .NET    
//  CONTROLS AS PART OF AN EXECUTABLE PROGRAM ONLY.                
//                                                                  
//  THE SOURCE CODE CONTAINED WITHIN THIS FILE AND ALL RELATED      
//  FILES OR ANY PORTION OF ITS CONTENTS SHALL AT NO TIME BE        
//  COPIED, TRANSFERRED, SOLD, DISTRIBUTED, OR OTHERWISE MADE      
//  AVAILABLE TO OTHER INDIVIDUALS WITHOUT EXPRESS WRITTEN CONSENT  
//  AND PERMISSION FROM IZENDA INC.                      
//                                                                  
//  CONSULT THE END USER LICENSE AGREEMENT(EULA FOR INFORMATION ON  
//  ADDITIONAL RESTRICTIONS.
// </copyright> 
// ----------------------------------------------------------------------


namespace Mvc5StarterKit.IzendaBoundary.Models.Permissions
{
    /// <summary>
    /// The permission model
    /// </summary>
    public class Permission
    {
        /// <summary>
        /// Gets or sets a value indicating whether user has system admin permission.
        /// </summary>
        public bool SystemAdmin { get; set; }

        /// <summary>
        /// Full report and dashboard access
        /// </summary>
        public bool FullReportAndDashboardAccess { get; set; }

        /// <summary>
        /// The system configuration
        /// </summary>
        public SystemConfiguration.SystemConfiguration SystemConfiguration { get; set; }

        /// <summary>
        /// The data setup
        /// </summary>
        public DataSetup.DataSetup DataSetup { get; set; }

        /// <summary>
        /// The user setup
        /// </summary>
        public UserSetup.UserSetup UserSetup { get; set; }

        /// <summary>
        /// The role setup
        /// </summary>
        public RoleSetup.RoleSetup RoleSetup { get; set; }

        /// <summary>
        /// The reports
        /// </summary>
        public Reports.Reports Reports { get; set; }

        /// <summary>
        /// The tenant
        /// </summary>
        public TenantSetup.TenantSetup TenantSetup { get; set; }

        /// <summary>
        /// The dashboards
        /// </summary>
        public Dashboards.Dashboards Dashboards { get; set; }

        /// <summary>
        /// The access
        /// </summary>
        public Access.Access Access { get; set; }

        /// <summary>
        /// The scheduling
        /// </summary>
        public Scheduling.Scheduling Scheduling { get; set; }

        /// <summary>
        /// The emailing
        /// </summary>
        public Emailing.Emailing Emailing { get; set; }

        /// <summary>
        /// Exporting
        /// </summary>
        public Exporting.Exporting Exporting { get; set; }

        /// <summary>
        /// System wide
        /// </summary>
        public Systemwide.Systemwide Systemwide { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public Permission()
        {
            SystemConfiguration = new SystemConfiguration.SystemConfiguration();
            DataSetup = new DataSetup.DataSetup();
            TenantSetup = new TenantSetup.TenantSetup();
            UserSetup = new UserSetup.UserSetup();
            RoleSetup = new RoleSetup.RoleSetup();
            Reports = new Reports.Reports();
            Dashboards = new Dashboards.Dashboards();
            Access = new Access.Access();
            Scheduling = new Scheduling.Scheduling();
            Emailing = new Emailing.Emailing();
            Exporting = new Exporting.Exporting();
            Systemwide = new Systemwide.Systemwide();
        }
    }
}
