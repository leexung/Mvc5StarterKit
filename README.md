# Izenda Mvc5Starterkit

## Overview
The MVC5 Starterkit showcases how to embed Izenda in a MVC5 application.

 :warning: The MVC Kit is designed for demonstration purposes and should not be used as an “as-is” fully-integrated solution. You can use the kit for reference or a baseline but ensure that security and customization meet the standards of your company.
 
 Download the v1.24.4 (https://downloads.izenda.com/v1.24.4/) of the API and EmbeddedUI and copy the following:

All files & folders in API\bin -> Mvc5StarterKit\IzendaReferences

API\Content -> Mvc5StarterKit\IzendaResources
API\EmailTemplates -> Mvc5StarterKit\IzendaResources
API\Export -> Mvc5StarterKit\IzendaResources

EmbeddedUI -> Mvc5StarterKit\Scripts\izenda

Open SSMS
- Create a new database named Retail
- Run script located at Mvc5StarterKit\SQLScript\RetailDbScript.sql

Run Application and login by System Admin Account below:
- Tenant : System
- Username: IzendaAdmin@system.com
- Password: Izenda@123

After start up and logging in as Admin Go to Settings on top Nav
- Add Izenda License Key and Token (leave Izenda Configuration Database Connection string set this is using an MDF file included in this kit)
- Go to Data Setup > Connection String  add the connection string to the Retail database created above for system and all tenants and move tables/views to visible
   More Info here:<a href="https://www.izenda.com/docs/ui/doc_connection_string.html?highlight=connection%20string" /> How To Set Up A Connection String</a>
   
- In Data Setup > Advanced Settings > Security Set the Tenant Field to [CustomerID] (please ensure you use the brackets) for each Tenant / CustomerID in the Retail Database are the tenants  (DELDG/NATWR/RETCL) and this will filter data
   based on the current user's TenantID
   More Info here: <a href = "https://www.izenda.com/docs/ui/doc_advanced_settings.html?highlight=set%20tenant%20field#update-settings-in-security-tenant-group"/> Updating settings in Performance, Security/Additive Fields and Others group </a> 

- For each Role in the tenant set datamodel access (what tables / views each role can access)
  More Info here: <a href = "https://www.izenda.com/wiki7/ui/doc_role_setup.html?highlight=role%20setup" /> How To Set Up A Role</a>

For each Tenant the following users / roles are configured all use the same password: Izenda@123

Tenant: DELDG <br />
User: employee@deldg.com        Role: employee <br />
User: manager@deldg.com         Role: manager <br />
User: vp@deldg.com              Role: VP <br />

Tenant: NATWR <br />
User: employee@natwr.com        Role: employee <br />
User: manager@natwr.com         Role: manager <br />
User: VP@natwr.com              Role: VP <br />

Tenant: RETCL <br />
User: employee@retcl.com         		Role: employee <br />
User: manager@retcl.com    								Role: manager <br />
User: vp@retcl.com         								Role: VP <br />

When registering a new user in this sample all users are hardcoded to the manager role here:
Mvc5StarterKit\Controllers\AccountController.cs (Line 177)	

Please review the following file:
mvc5starterkit\mvc5starterkit\izendaboundary\customadhocreport.cs
This is where you can find samples for:
Hidden Filters
Filter Dropdown Overrides
See more information here: https://www.izenda.com/wiki7/index.php?title=Ref:IAdHocExtension


The CSS can be configured per tenant and an example is provided see below:
This is configured here ~\mvc5starterkit\Mvc5StarterKit\Views\Shared\_Layout.cshtml
And folder structures are located here ~\mvc5starterkit\Mvc5StarterKit\Content

