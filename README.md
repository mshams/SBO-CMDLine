# SBO-CMDLine

SBO-CMDLine is a handy tool to connect to Sap Business One and run commands via cmd-line.

It makes it easier to run batch commands for SBO administrator and developers.

Usage Examples:

SBO-CMDLine.exe /h
SBO Command Line Helper v1.0.0.0
By M.Shams 2021

Options:
  -f=VALUE                   Connection filename for DI connection mode.
                               VALUE: <FILEPATH>
  -m=VALUE                   Connection mode.
                               VALUE: UI, DI
  -c                         Company information tools.
  -n                         Working with UI menus.
  -r                         Report management tools.
  -h, -?                     show help

SBO-CMDLine.exe -m=UI -c --verbose --list
SBODemoTR
        Name: OEC Bilgisayarları
        Version: 930210
        Localization: Turkey
SBODemoUS
        Name: OEC Computers
        Version: 930210
        Localization: United States Of America/Puerto Rico
  
SBO-CMDLine.exe -m=UI -n --verbose --list=43520
[+]UID: 43520           Name: &Modules
   [+]UID: 3328         Name: &Administration
   [+]UID: 1536         Name: &Financials
   [+]UID: 43679        Name: &CRM
   [+]UID: 2560         Name: &Opportunities
   [+]UID: 2048         Name: &Sales - A/R
   [+]UID: 2304         Name: &Purchasing - A/P
   [+]UID: 43535        Name: &Business Partners
   [+]UID: 43537        Name: Ba&nking
   [+]UID: 3072         Name: &Inventory
   [+]UID: 13312        Name: &Resources
   [+]UID: 4352         Name: Pro&duction
   [+]UID: 43543        Name: &MRP
   [+]UID: 3584         Name: S&ervice
   [+]UID: 43544        Name: &Human Resources
   [+]UID: 48896        Name: Pro&ject Management
   [+]UID: 43545        Name: Repor&ts
   
SBO-CMDLine.exe -m=UI -n --verbose --find=name:"Financial Reports"
UID: 43531      Name: Financia&l Reports

SBO-CMDLine.exe -m=UI -n --verbose --find=id:43550
UID: 43550      Name: &Financial

SBO-CMDLine.exe -m=UI -n --verbose --find=name:"Query"
UID: 45111      Name: BP Bank Accounts Query
UID: 45113      Name: House Bank Accounts Query
UID: 45110      Name: BP Bank Accounts Query
UID: 45112      Name: House Bank Accounts Query
UID: 4865       Name: &Query Manager...
UID: 4102       Name: Q&uery Generator
UID: 4103       Name: Qu&ery Wizard
UID: 4868       Name: Que&ry Print Layout...
UID: 5121       Name: BP Bank Accounts Query
UID: 5130       Name: House Bank Accounts Query

SBO-CMDLine.exe -m=UI -r
Install/Uninstall reports.
Options:
      --file[=VALUE]         Report file to do action on.
                               VALUE: <FILEPATH>
      --report[=VALUE]       Report name to install.
                               VALUE: <NAME>
      --type[=VALUE]         Type name to install.
                               VALUE: <NAME>
      --code[=VALUE]         Type code to install.
                               VALUE: <NAME>
      --form[=VALUE]         Form name to install.
                               VALUE: <NAME>
      --addon[=VALUE]        Addon name to install.
                               VALUE: <NAME>
      --menu[=VALUE]         Menu ID to install report.
                               VALUE: <NAME>
      --install              Install report.
      --uninstall            Uninstall report.
      --list                 List installed reports.
      --find=VALUE           Find report by type or name.
                               VALUE: type:<TYPECODE>, name:<SUBSTRING>
							   
SBO-CMDLine.exe -m=UI -r --file="AdvancedTrialBalance-v1.rpt" --report="Advanced Trial Balance" --install --menu=9728
SBO-CMDLine.exe -m=UI -r --code=A005 --uninstall
SBO-CMDLine.exe -m=UI -r --type=A007 --uninstall

SBO-CMDLine.exe -m=UI -r --find=type:RCRI
Type: RCRI  Code: RCRI0005  Category: rlcCrystal  Name: Annual Sales Analysis (by Quarter)
Type: RCRI  Code: RCRI0002  Category: rlcCrystal  Name: Bank Reconciliation Report
Type: RCRI  Code: RCRI0006  Category: rlcCrystal  Name: Business Assessment Report
Type: RCRI  Code: RCRI0007  Category: rlcCrystal  Name: Customer Open Document List
Type: RCRI  Code: RCRI0008  Category: rlcCrystal  Name: Customer Open Document List_HANA
Type: RCRI  Code: RCRI0009  Category: rlcCrystal  Name: Inventory Turnover Analysis
Type: RCRI  Code: RCRI0010  Category: rlcCrystal  Name: Inventory Turnover Analysis_HANA
Type: RCRI  Code: RCRI0001  Category: rlcCrystal  Name: Inventory Valuation Method Report
Type: RCRI  Code: RCRI0011  Category: rlcCrystal  Name: Monthly Customer Status Report
Type: RCRI  Code: RCRI0012  Category: rlcCrystal  Name: Monthly Customer Status Report - HANA
Type: RCRI  Code: RCRI0003  Category: rlcCrystal  Name: Payment Orders Report by Business Partner
Type: RCRI  Code: RCRI0004  Category: rlcCrystal  Name: Payment Orders Report by Payment Run

SBO-CMDLine.exe -m=UI -r --find=name:Payroll
Type: A001  Code: A0010001  Category: rlcCrystal  Name: Payroll Document
Type: A004  Code: A0040001  Category: rlcCrystal  Name: Payroll Documents